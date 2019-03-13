using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web;
using System.Transactions;
using SummerFresh.Data.Attributes;
using SummerFresh.Util;
namespace SummerFresh.Business.Service
{
    public class CustomEntityService
    {
        public Type EntityType { get; set; }

        public HttpRequest Request { get; set; }

        public virtual void CleanTableCache()
        {
            var tableAttr = EntityType.GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null)
            {
                string tableName = tableAttr.Name;
                CacheHelper.Remove(NamingCenter.GetCacheKey(CacheType.DATA_TABLE, tableName));
            }
        }

        public virtual IListDataSource DataSource
        {
            get
            {
                return new EntityDataSource(EntityType);
            }
        }

        public virtual object Get(object id)
        {
            object returnValue = null;
            string key = EntityType.FullName + ".Get";
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                returnValue = new EntityDataSource(EntityType).Get(id.ToString());
            }
            else
            {
                var temp = Dao.Get().QueryDictionary(key, new { id = id });
                if (!temp.IsNullOrEmpty())
                {
                    returnValue = temp.ToEntity(EntityType);
                }
            }
            return returnValue;
        }

        public virtual int Insert(CustomEntity entity)
        {
            EntityType = entity.GetType();
            string key = EntityType.FullName + ".Insert";
            int effectCount = 0;
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                effectCount = Dao.Get().Insert(entity);
            }
            else
            {
                effectCount = Dao.Get().ExecuteNonQuery(key, entity);
            }
            if(effectCount>0)
            {
                CleanTableCache();
            }
            return effectCount;
        }

        public virtual int Update(CustomEntity entity)
        {
            EntityType = entity.GetType();
            string key = EntityType.FullName + ".Update";
            int effectCount = 0;
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                effectCount = Dao.Get().Update(entity);
            }
            else
            {
                effectCount = Dao.Get().ExecuteNonQuery(key, entity);
            }
            if (effectCount > 0)
            {
                CleanTableCache();
            }
            return effectCount;
        }

        public virtual int Delete(object entity)
        {
            string key = EntityType.FullName + ".Delete";
            int effectCount = 0;
            if (DaoFactory.GetSqlSource().Find(key) == null)
            {
                var dao = Dao.Get();
                string providerName = dao.Provider.Name.ToLower();
                var provider = DaoFactory.GetMappingProvider(dao.DbProviderName);
                var tabAttr = EntityType.GetCustomAttribute<TableAttribute>(true);
                string tableName = tabAttr == null ? EntityType.Name : tabAttr.Name;
                key = SQLSyntaxGeneratorFactory.GetGenerator(tableName).GetDeleteSQL();
                var table = SummerFresh.Data.Mapping.TableMapper.ReadTable(dao, provider, tableName, string.Empty);
                var tableMapping = new SummerFresh.Data.Mapping.TableMapping(dao.Provider, provider, null, table);
                //effectCount = Dao.Get().ExecuteNonQuery(key, new { ids = entity });
                effectCount = Dao.Get().ExecuteNonQuery(key, new Dictionary<string, object> { { table.Keys.FirstOrDefault().Name, entity } });
            }
            else
            {
                effectCount = Dao.Get().ExecuteNonQuery(key, new { ids = entity });
            }
            if (effectCount > 0)
            {
                CleanTableCache();
            }
            return effectCount;
        }

        public virtual int BatchDelete(string[] ids)
        {
            using (TransactionScope tran = new TransactionScope())
            {
                string key = EntityType.FullName + ".BatchDelete";
                int effectCount = 0;
                if (DaoFactory.GetSqlSource().Find(key) == null)
                {
                    foreach (var id in ids)
                    {
                        effectCount += Delete(id);
                    }
                }
                else
                {
                    effectCount = Dao.Get().ExecuteNonQuery(key, new { ids = string.Join(",", ids, 0, ids.Length) });
                }
                tran.Complete();
                if (effectCount > 0)
                {
                    CleanTableCache();
                }
                return effectCount;
            }
        }

        public virtual IList<ButtonEntity> AddButtons()
        {
            var buttons = new List<ButtonEntity>();
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnInsert", ButtonName = "新增", CssClass = "btn btn-success add-button", ButtonType = ButtonEntityType.Toolbar });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnEdit", ButtonName = "编辑", CssClass = "btn btn-primary btn-sm", ButtonType = ButtonEntityType.TableRow });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnDelete", ButtonName = "删除", CssClass = "btn btn-danger btn-sm", ButtonType = ButtonEntityType.TableRow });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnBatchDelete", ButtonName = "批量删除", CssClass = "btn btn-danger delete-button-w", ButtonType = ButtonEntityType.Toolbar });
            buttons.Add(new ButtonEntity() { ButtonControlId = "btnExport", ButtonName = "导出EXCEL", CssClass = "btn btn-primary copy-button", OnClick = "exportToExcel();", ButtonType = ButtonEntityType.Toolbar });
            return buttons;
        }
    }
}
