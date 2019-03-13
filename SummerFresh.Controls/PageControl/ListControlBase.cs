using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using SummerFresh.Basic;
using System.Threading;
namespace SummerFresh.Controls
{
    public abstract class NoPagerListControlBase : PageControlBase, IListDataSourceControl
    {
        public override object Clone()
        {
            var result = base.Clone() as NoPagerListControlBase;
            if (result.DataSource != null)
            {
                result.DataSource.Parameter = null;
                //result.DataSource.SortExpression = "";
            }
            return result;
        }

        /// <summary>
        /// 表格数据源
        /// </summary>
        [DisplayName("表格数据源")]
        public IListDataSource DataSource
        {
            get;
            set;
        }

        private void InitDataSourceParameter()
        {
            if (DataSource != null)
            {
                IDictionary<string, object> param = null;
                if (DataSource.Parameter != null)
                {
                    param = DataSource.Parameter is Dictionary<string, object> ? (DataSource.Parameter as Dictionary<string, object>) : DataSource.Parameter.ToDictionary();
                }
                else
                {
                    param = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                }
                foreach (var key in HttpContext.Current.Request.QueryString.AllKeys)
                {
                    if (!HttpContext.Current.Request.QueryString[key].IsNullOrEmpty())
                    {
                        param[key] = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString[key]);
                    }
                }
                foreach (var key in HttpContext.Current.Request.Form.AllKeys)
                {
                    if (!HttpContext.Current.Request.Form[key].IsNullOrEmpty())
                    {
                        param[key] = HttpUtility.UrlDecode(HttpContext.Current.Request.Form[key]);
                    }
                }
                DataSource.Parameter = param;
            }
        }

        protected virtual IList<IDictionary<string, object>> GetDataInner()
        {
            if (DataSource == null)
            {
                throw new CustomException("NoPagerListControlBase组件需要指定DataSource");
            }
            return DataSource.GetList();
        }

        public virtual IList<IDictionary<string, object>> GetData()
        {
            InitDataSourceParameter();
            var entities = GetDataInner();
            if(entities.IsNullOrEmpty())
            {
                return entities;
            }
            if (DataSource is ListDataSourceBase)
            {
                var tempDs = DataSource as ListDataSourceBase;
                var filters = tempDs.DataFilters;
                if(!filters.IsNullOrEmpty())
                {
                    foreach(var filter in filters.OrderBy(o=>o.Rank))
                    {
                        entities = filter.Conveter(entities);
                    }
                }
                var converters = tempDs.ColumnConverters;
                if (!converters.IsNullOrEmpty())
                {
                    foreach (var entity in entities)
                    {
                        foreach (var converter in converters.OrderBy(o => o.Rank))
                        {
                            foreach (var columnName in converter.ColumnName.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                if (!entity.ContainsKey(columnName)) continue;
                                converter.RowData = entity;
                                switch (converter.ConverterType)
                                {
                                    case ColumnConverterType.Append:
                                        entity[converter.AppendColumnName] = converter.Converter(entity[columnName]);
                                        break;
                                    case ColumnConverterType.Replace:
                                        entity[columnName] = converter.Converter(entity[columnName]);
                                        break;
                                    case ColumnConverterType.Rename:
                                        entity[converter.AppendColumnName] = entity[columnName];
                                        entity.Remove(columnName);
                                        break;
                                    case ColumnConverterType.Remove:
                                        entity.Remove(columnName);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return entities;
        }
    }

    public abstract class ListControlBase : NoPagerListControlBase
    {

        /// <summary>
        /// 是否翻页
        /// </summary>
        [DisplayName("是否翻页")]
        public bool AllowPaging { get; set; }

        /// <summary>
        /// 翻页显示位置
        /// </summary>
        [DisplayName("翻页显示位置")]
        public PagerPosition PagerPosition { get; set; }

        /// <summary>
        /// 翻页配置
        /// </summary>
        [DisplayName("翻页配置")]
        public Pager Pager { get; set; }



        protected override IList<IDictionary<string, object>> GetDataInner()
        {
            if (DataSource == null)
            {
                throw new CustomException("ListControlBase组件需要指定DataSource");
            }
            int recordCount = 0;
            IList<IDictionary<string, object>> entities = null;
            if (AllowPaging)
            {
                if (!HttpContext.Current.Request["pageIndex"].IsNullOrEmpty())
                {
                    Pager.CurrentPageIndex = HttpContext.Current.Request["pageIndex"].ConverTo<int>();
                }
                else
                {
                    Pager.CurrentPageIndex = 1;
                }
                if (!HttpContext.Current.Request["pageSize"].IsNullOrEmpty())
                {
                    Pager.PageSize = HttpContext.Current.Request["pageSize"].ConverTo<int>();
                }
                if (!HttpContext.Current.Request["sortExpression"].IsNullOrEmpty())
                {
                    DataSource.SortExpression = HttpContext.Current.Request["sortExpression"];
                }
                entities = DataSource.GetList(Pager.CurrentPageIndex, Pager.PageSize, out recordCount);
                Pager.RecordCount = recordCount;
            }
            else
            {
                entities = DataSource.GetList();
            }
            return entities;
        }
    }
}
