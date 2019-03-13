using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Business.Entity;
using SummerFresh.Util;

namespace SummerFresh.Business
{
    public class QCResultDataSource:IListDataSource
    {
        public IList<IDictionary<string, object>> GetList()
        {
            IList<IDictionary<string, object>> qcRes = Data.Dao.Get().QueryDictionaries("EXEC [P_QCPreviewCity] '$StationCode$',1",Parameter);
            //var groupQC=qcRes.GroupBy(c=>c["PositionName"]).ToDictionary<object,IList<IDictionary<string, object>>>();
            IList<StationEntity> stationRes = Data.Dao.Get().QueryEntities<StationEntity>( SQLGenerator.GetSelectAllCommand(typeof(StationEntity)));  //new CustomEntityDataSource(typeof(StationEntity)).GetList();
            Dictionary<object,IDictionary<string, object>> result = new  Dictionary<object,IDictionary<string,object>>();

            object key;
            string PositionName = "站点";
            string Count = "异常项统计";
            string Task = "异常项目(各项目最近一次导常情况)";
            foreach (IDictionary<string, object> qcItem in qcRes)
            {
                key=qcItem["PositionName"];
                if (!result.ContainsKey(key))
                {
                    result[key] = new Dictionary<string, object>();
                    result[key][PositionName]=key;
                    result[key][Count]=0;
                    result[key][Task]="";
                }
                result[key][Count] = Convert.ToInt32(result[key][Count]) + 1;
                result[key][Task] = string.Format("{0} {1}", result[key][Task], qcItem[Task]).Trim();
            }
            foreach (StationEntity stationEnt in stationRes)
            {
                key = stationEnt.PositionName;
                if (!result.ContainsKey(key))
                {
                    result[key] = new Dictionary<string, object>();
                    result[key][PositionName] = key;
                    result[key][Count] = 0;
                    result[key][Task] = "";
                }
            }
            foreach (var item in result)
            {
                item.Value[Count] = string.Format("{0}个异常项目", item.Value[Count]);
            }
            return result.Values.ToList();
            //return res;
        }

        public IList<IDictionary<string, object>> GetList(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = -1;
            return null;
        }

        public string ID
        {
            get;
            set;
        }

        public object Parameter
        {
            get;
            set;
        }

        public string SortExpression
        {
            get;
            set;
        }

        public object Converter(string columnName, object columnValue, IDictionary<string, object> rowData)
        {
            throw new NotImplementedException();
        }
    }
}
