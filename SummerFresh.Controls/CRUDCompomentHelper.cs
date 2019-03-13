using SummerFresh.Business;
using System;
using SummerFresh.Basic;
using SummerFresh.Data;
using SummerFresh.Business.Entity;
using System.Web.Script.Serialization;
using System.Web;
using SummerFresh.Basic.FastReflection;
using System.Collections;
namespace SummerFresh.Controls
{
    public class CRUDComponentHelper
    {
        public static void RecSave(string pageId, string parentId, IControl instance, string targetId)
        {
            var componentId = SaveComponent(instance, null, pageId, parentId, targetId);
            var pis = FastType.Get(instance.GetType()).Setters;
            foreach (var p in pis)
            {
                if (p.Type.GetInterface(typeof(IControl).FullName, true) != null)
                {
                    var newInstance = p.GetValue(instance) as IControl;
                    if (newInstance != null)
                    {
                        RecSave(pageId, componentId, newInstance, p.Name);
                    }
                }
                if (p.Type.IsGenericType && p.Type.GetInterface(typeof(IEnumerable).FullName, true) != null && !p.Name.Equals("Controls", StringComparison.CurrentCultureIgnoreCase))
                {
                    var lists = p.GetValue(instance) as IEnumerable;
                    if (lists != null)
                    {
                        foreach (var list in lists)
                        {
                            RecSave(pageId, componentId, list as IControl, p.Name);
                        }
                    }
                }
            }
        }
        public static string SaveComponent(IControl entity, string componentId, string pageId, string parentId, string targetId)
        {
            if (componentId.IsNullOrEmpty())
            {
                if (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.ComponentEntity.Validate", new { id = entity.ID, pId = parentId, tId = targetId }) > 0)
                {
                    throw new CustomException("控件ID不能重复！");
                }
                if (Dao.Get().QueryScalar<int>("SummerFresh.Business.Entity.ComponentEntity.Validate", new { id = entity.ID, pageId = pageId }) > 0)
                {
                    throw new CustomException("控件ID不能重复！");
                }
            }
            var componentEntity = new ComponentEntity();
            componentEntity.ComponentName = entity.ID;
            componentEntity.ComponentType = entity.GetType().FullName;
            componentEntity.PageId = pageId;
            componentEntity.ParentId = parentId;
            componentEntity.TargetId = targetId;
            if (entity is ISortableControl)
            {
                componentEntity.Rank = (entity as ISortableControl).Rank;
            }
            if (entity is PageControlBase)
            {
                componentEntity.ComponentDataType = "Component";
            }
            else
            {
                componentEntity.ComponentDataType = "Control";
            }
            if ((entity is IFieldConverter) || (entity is IColumnConverter))
            {
                componentEntity.ComponentDataType = "DataSource";
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new ComponentConverter() });
            componentEntity.ComponentXML = serializer.Serialize(entity);
            int effectCount = 0;
            var service = EntityComponentHelper.GetEntityService(typeof(ComponentEntity));
            if (componentId.IsNullOrEmpty())
            {
                componentEntity.ComponentId = Guid.NewGuid().ToString();
                effectCount = service.Insert(componentEntity);
            }
            else
            {
                componentEntity.ComponentId = componentId;
                effectCount = service.Update(componentEntity);
            }
            return componentEntity.ComponentId;
        }

        public static IControl GetComponent(string componentId, HttpRequest request)
        {
            var componentEntity = (new EntityDataSource(typeof(ComponentEntity))).Get(componentId) as ComponentEntity;
            if (componentEntity == null)
            {
                string key = NamingCenter.GetCacheKey(CacheType.ENTITY_CONFIG, componentId);
                var component = EntityComponentHelper.GetComponent(key);
                if (component == null)
                {
                    string widget = request["widget"];
                    var type = TypeHelper.GetType(componentId.Substring(0, componentId.LastIndexOf('_')).Replace('_', '.'));
                    switch (widget)
                    {
                        case "Table":
                            component = EntityComponentHelper.GetTableComponent(type);
                            break;
                        case "Form":
                            component = EntityComponentHelper.GetFormComponent(type);
                            break;
                        default:
                            break;
                    }
                    return component;
                }
                else
                {
                    return ((component is PageControlBase) ? (component as PageControlBase).Clone() : component) as IControl;
                }
            }
            else
            {
                var page =PageBuilder.BuildPage(componentEntity.PageId, request);
                page.Prepare();
                foreach (var c in page.Controls)
                {
                    var result = RecGet(c, componentId);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        public static IControl RecGet(IControl c, string componentId)
        {
            if (c is IAttributeAccessor)
            {
                if ((c as IAttributeAccessor).Attributes["componentId"] == componentId)
                {
                    return c;
                }
            }
            if (c is IChildren)
            {
                foreach (var cc in (c as IChildren).Controls)
                {
                    var result = RecGet(cc, componentId);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null;
        }
    }
}
