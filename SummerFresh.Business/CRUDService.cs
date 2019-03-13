using SummerFresh.Business.Entity;
using SummerFresh.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
using System.Web.Mvc;
namespace SummerFresh.Business
{
    [DisplayName("CRUD数据源")]
    public class CRUDService : ListDataSourceBase, IFormService, IFieldConverter, IKeyValueDataSource, ICascadeDataSource
    {
        [FormField(ControlType = ControlType.DropDownList)]
        [CustomEntityDataSource(typeof(CRUDEntity))]
        public string CRUDName { get; set; }

        private Dao dao { get { return Dao.Get(); } }

        private CRUDEntity _entity;
        private CRUDEntity Entity
        {
            get
            {
                if (!CRUDName.IsNullOrEmpty())
                {
                    _entity = new CRUDEntityService().Get(CRUDName) as CRUDEntity;
                    if (_entity == null)
                    {
                        //throw new CustomException("CRUDName不存在!");
                    }
                }
                return _entity;
            }
        }

        public virtual int Insert(IDictionary<string, object> data)
        {
            if (!data.IsNullOrEmpty())
            {
                if (!data.Keys.Contains(KeyFieldName))
                {
                    data[KeyFieldName] = Guid.NewGuid().ToString();
                }
                int effectCount = dao.ExecuteNonQuery(Entity.InsertSQL, data);
                if (effectCount > 0)
                {
                    BSDContextCache.ClearTableCache(Entity.TableName);
                }
                return effectCount;
            }
            return 0;
        }

        public virtual int Update(string key, IDictionary<string, object> data)
        {
            int effectCount = 0;
            if (!data.IsNullOrEmpty())
            {
                if (!key.IsNullOrEmpty())
                {
                    data[Entity.PKName] = key;
                }
                effectCount = dao.ExecuteNonQuery(Entity.UpdateSQL, data);
                if (effectCount > 0)
                {
                    BSDContextCache.ClearTableCache(Entity.TableName);
                }
            }
            return effectCount;
        }

        public virtual int Delete(string key)
        {
            var parameter = new Dictionary<string, object>();
            if (!key.IsNullOrEmpty())
            {
                parameter[Entity.PKName] = key;
                if (!Entity.ValueFieldName.IsNullOrEmpty() && !Entity.ValueFieldName.Equals(Entity.PKName, StringComparison.CurrentCultureIgnoreCase))
                {
                    parameter[Entity.ValueFieldName] = key;
                }
            }
            int effectCount = dao.ExecuteNonQuery(Entity.DeleteSQL, parameter);
            if (effectCount > 0)
            {
                BSDContextCache.ClearTableCache(Entity.TableName);
            }
            return effectCount;
        }

        public virtual IDictionary<string, object> Get(string key)
        {
            var parameter = new Dictionary<string, object>();
            if (!key.IsNullOrEmpty())
            {
                parameter[Entity.PKName] = key;
                if (!Entity.ValueFieldName.IsNullOrEmpty() && !Entity.ValueFieldName.Equals(Entity.PKName, StringComparison.CurrentCultureIgnoreCase))
                {
                    parameter[Entity.ValueFieldName] = key;
                }
            }
            return dao.QueryDictionary(Entity.GetOneSQL, parameter);
        }

        public override IList<IDictionary<string, object>> GetList()
        {
            if (SortExpression.IsNullOrEmpty())
            {
                SortExpression = Entity.DefaultSortExpression;
            }
            if (SortExpression.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase) < 0)
            {
                SortExpression = " ORDER BY {0}".FormatTo(SortExpression);
            }
            string sql = Entity.SelectSQL;
            if (sql.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase) < 0)
            {
                sql = sql + SortExpression;
            }
            return dao.QueryDictionaries(sql, Parameter);
        }

        public IList<IDictionary<string, object>> GetAll()
        {
            if (SortExpression.IsNullOrEmpty())
            {
                SortExpression = Entity.DefaultSortExpression;
            }
            if (SortExpression.IndexOf("ORDER BY", StringComparison.OrdinalIgnoreCase) < 0)
            {
                SortExpression = " ORDER BY {0}".FormatTo(SortExpression);
            }
            string sql = "SELECT * FROM {0} {1}".FormatTo(Entity.TableName, SortExpression);
            return dao.QueryDictionaries(sql, Parameter);
        }

        public override IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            if (SortExpression.IsNullOrEmpty())
            {
                SortExpression = Entity.DefaultSortExpression;
            }
            return dao.PageQueryDictionariesByPage(Entity.SelectSQL, pageIndex, pageSize, SortExpression, out recordCount, Parameter);
        }

        private IList<IDictionary<string, object>> _items;
        private IList<IDictionary<string, object>> Items
        {
            get
            {
                return _items ?? (_items = GetAll());
            }
        }

        public override object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            var res = SplitConvert(columnName, columnValue, rowData);
            if (res != null)
            {
                return res;
            }
            if (columnValue != null && !columnName.ToString().IsNullOrEmpty() && !Items.IsNullOrEmpty())
            {
                IDictionary<string, object> result = null;
                if (Items[0].ContainsKey(Entity.PKName))
                {
                    result = Items.FirstOrDefault(o => o[Entity.PKName].ToString().Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
                }
                if (result.IsNullOrEmpty() && !Entity.PKName.Equals(Entity.ValueFieldName, StringComparison.OrdinalIgnoreCase) && Items[0].ContainsKey(Entity.ValueFieldName))
                {
                    result = Items.FirstOrDefault(o => o[Entity.ValueFieldName].ToString().Equals(columnValue.ToString(), StringComparison.OrdinalIgnoreCase));
                }
                if (result.IsNullOrEmpty())
                {
                    result = Get(columnValue.ToString());
                }
                if (!result.IsNullOrEmpty())
                {
                    return result[Entity.TitleFieldName];
                }
            }
            return string.Empty;
        }

        public string KeyFieldName
        {
            get
            {
                if (Entity != null)
                {
                    return Entity.PKName;
                }
                return string.Empty;
            }
        }

        public IList<SelectListItem> SelectItems()
        {
            var returnValue = new List<SelectListItem>();
            foreach (var r in Items)
            {
                returnValue.Add(new SelectListItem() { Text = r[Entity.TitleFieldName].ToString(), Value = r[Entity.ValueFieldName].ToString() });
            }
            return returnValue;
        }

        public IList<TreeNode> SelectRootItems(TreeNode parent = null)
        {
            var returnValue = new List<TreeNode>();
            foreach (var r in Items)
            {
                var node = new TreeNode() { name = r[Entity.TitleFieldName].ToString(), id = r[Entity.ValueFieldName].ToString(), pId = r[Entity.ParentFieldName].ToString() };
                node.open = node.pId.IsNullOrEmpty() || node.pId.Equals("root", StringComparison.OrdinalIgnoreCase);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}
