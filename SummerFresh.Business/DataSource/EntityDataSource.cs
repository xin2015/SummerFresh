using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Util;
using SummerFresh.Data.Attributes;
using System.Web.Mvc;
using SummerFresh.Data.Sql;
using System.ComponentModel;
using System.Web;
using SummerFresh.Basic.FastReflection;
using System.Threading;
using System.Web.Script.Serialization;
namespace SummerFresh.Business
{
    /// <summary>
    /// 实体数据源
    /// </summary>
    [DisplayName("实体数据源")]
    public class EntityDataSource : ListDataSourceBase, ICascadeDataSource, IKeyValueDataSource
    {
        private string entityTypeFullName;
        private Type _entityType;

        private Type EntityType
        {
            get
            {
                return _entityType;
            }
            set
            {
                _entityType = value;
                GetField();
            }
        }

        [DisplayName("实体")]
        [TypeDataSource(typeof(CustomEntity))]
        [FormField(ControlType = ControlType.DropDownList)]
        public string EntityTypeFullName
        {
            get
            {
                return entityTypeFullName;
            }
            set
            {
                entityTypeFullName = value;
                EntityType = TypeHelper.GetType(value);

            }
        }

        private string ValueFieldName { get; set; }

        private string PKFieldName { get; set; }

        private string TitleFieldName { get; set; }

        private string ParentFieldName { get; set; }

        private string TableName { get; set; }

        public EntityDataSource()
        {

        }

        public EntityDataSource(Type entityType)
        {
            EntityType = entityType;
        }

        private void GetField()
        {
            var tableAttr = EntityType.GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null)
            {
                TableName = tableAttr.Name;
            }
            var pis = EntityType.GetProperties();
            foreach (var p in pis)
            {
                var attr2 = p.GetCustomAttribute<ValueFieldAttribute>(true);
                if (attr2 != null)
                {
                    ValueFieldName = p.Name;
                }
                var attr = p.GetCustomAttribute<PrimaryKeyAttribute>(true);
                if (attr != null)
                {
                    PKFieldName = p.Name;
                }
                var attr1 = p.GetCustomAttribute<TitleFieldAttribute>(true);
                if (attr1 != null)
                {
                    TitleFieldName = p.Name;
                }
                var attr3 = p.GetCustomAttribute<ParentFieldAttribute>(true);
                if (attr3 != null)
                {
                    ParentFieldName = p.Name;
                }
                if (ValueFieldName.IsNullOrEmpty())
                {
                    ValueFieldName = PKFieldName;
                }
                var attr4 = p.GetCustomAttribute<DefaultSortFieldAttribute>(true);
                if (attr4 != null)
                {
                    string orderByTemplete = ", " + Dao.Get().Provider.NameFormat + " {1}";
                    string desc = "DESC";
                    if (attr4.OrderByType == OrderByType.ASC)
                    {
                        desc = "ASC";
                    }
                    SortExpression += orderByTemplete.FormatTo(p.Name, desc);
                }
            }
            if (!SortExpression.IsNullOrEmpty())
            {
                SortExpression = SortExpression.Substring(1);
            }
        }

        
        private DataTableDataSource _tableSource;

        [ScriptIgnore]
        public DataTableDataSource TableSource
        {
            get
            {
                return _tableSource ?? (_tableSource = new DataTableDataSource()
                {
                    TableName = TableName,
                    DataTextField = TitleFieldName,
                    DataValueField = ValueFieldName,
                    ID = ID,
                    Parameter = Parameter,
                    ParentField = ParentFieldName,
                    SortExpression = SortExpression,
                    EnableCache = true
                });
            }
        }

        public IList<SelectListItem> SelectItems()
        {
            return TableSource.SelectItems();
        }

        public object Get(string id)
        {
            var result = TableSource.GetAll();
            var temp = result.FirstOrDefault(o => o[ValueFieldName].ToString().Equals(id));
            if (temp == null)
            {
                temp = result.FirstOrDefault(o => o[PKFieldName].ToString().Equals(id));
            }
            if (temp != null)
            {
                return temp.ToEntity(EntityType);
            }
            return null;
        }

        public override IList<IDictionary<string, object>> GetList()
        {
            IDictionary<string, object> param = null;
            if (Parameter != null)
            {
                param = Parameter is Dictionary<string, object> ? (Parameter as Dictionary<string, object>) : Parameter.ToDictionary();
            }
            else
            {
                param = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            }
            
            string sqlId = EntityType.FullName + ".Select";
            IList<IDictionary<string, object>> result = null;
            if (DaoFactory.GetSqlSource().Find(sqlId) == null)
            {
                result = TableSource.GetAll();
                var ft = FastType.Get(EntityType);
                foreach (var p in ft.Getters)
                {
                    var attr = p.Info.GetCustomAttribute<SearchFieldAttribute>(true);
                    if (attr != null)
                    {
                        if (!param.Keys.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        string key = p.Name;
                        if (p.Type == typeof(System.String))
                        {
                            //like
                            result = result.Where(o => o[p.Name].ToString().IndexOf(param[key].ToString(), StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
                        }
                        else if (p.Type == typeof(DateTime))
                        {
                            //between
                            DateTime dt = DateTime.MinValue;
                            if (DateTime.TryParse(param[key].ToString(), out dt))
                            {
                                result = result.Where(o => o[p.Name].ConverTo<DateTime>().ToString("yyyy-MM-dd").Equals(dt.ToString("yyyy-MM-dd"))).ToList();
                            }
                        }
                        else
                        {
                            // equal
                            result = result.Where(o => o[p.Name].Equals(param[key])).ToList();
                        }
                    }
                }
            }
            else
            {
                result = Dao.Get().QueryDictionaries(sqlId, Parameter);
            }
            return result;
        }

        public IList<TreeNode> SelectRootItems(TreeNode parent = null)
        {
            var sqlId = string.Empty;
            var attr = EntityType.GetCustomAttribute<TreeAttribute>(true);
            if (attr != null)
            {
                sqlId = attr.SqlId;
            }
            else
            {
                sqlId = EntityType.FullName + ".Tree";
                if (DaoFactory.GetSqlSource().Find(sqlId) == null)
                {
                    return TableSource.SelectRootItems();
                }
            }
            return Dao.Get().QueryEntities<TreeNode>(sqlId, parent);
        }

        public override object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var result = SplitConvert(columnName, columnValue, rowData);
            if (result != null)
            {
                return result;
            }
            var returnValue = TableSource.Converter(columnName, columnValue, rowData);
            if (returnValue != null)
            {
                var pI = EntityType.GetProperty(TitleFieldName);
                var attr = pI.GetCustomAttribute<DataSourceAttribute>(true);
                if (attr != null)
                {
                    var ds = attr.GetDataSource();
                    if (ds is IFieldConverter)
                    {
                        return returnValue = (ds as IFieldConverter).Converter(columnName, returnValue, rowData);
                    }
                }
                else
                {
                    return returnValue.ToString();
                }
            }
            return "";
        }
    }
}
