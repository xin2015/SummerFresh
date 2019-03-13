using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Business;
using SummerFresh.Business.Entity;
using SummerFresh.Controls;
using SummerFresh.Basic;

namespace SummerFresh.MVC.Controllers
{
    public class ChartController : BaseController
    {
        //
        // GET: /Chart/

        private static Dictionary<string, EntityDataSource> dicDatasource = new Dictionary<string, EntityDataSource>();

        public ActionResult Index()
        {
            //var chartConfig = ConfigHelper.GetChartConfig(typeof(PermissionEntity));
            //return View(chartConfig);

            return View();
        }

        public ActionResult Chart(string chartName )
        {
            return View(chartName,null);
        }


        public JsonResult CreateChartOption()
        {

            string fieldMapping = Request.QueryString["fieldmapping"];//字段映射
            string othersetting = Request.QueryString["othersetting"];//其他json设置
            string sqlids = Request.QueryString["sqlids"];//数据源id;分Entity 和 sqlid:......

            if (fieldMapping.IsNullOrWhiteSpace())
            {
                throw new Exception("缺少字段映射配置");
            }

            if (sqlids.IsNullOrWhiteSpace())
            {
                throw new Exception("缺少数据源信息");
            }

            List<string> datasourceName = sqlids.Split(',').ToList();

            IList<IList<IDictionary<string, object>>> data = new List<IList<IDictionary<string, object>>>();
            foreach (string item in datasourceName)
            {
                if (item.StartsWith("sqlid:"))
                {
                    data.Add(Data.Dao.Get().QueryDictionaries(item.Replace("sqlid:", "")));
                }
                else if (dicDatasource.ContainsKey(item))
                {
                    data.Add(dicDatasource[item].GetList());
                }
                else
                {
                    dicDatasource[item] = new EntityDataSource(GetEntityType(item));
                    data.Add(dicDatasource[item].GetList());
                }
            }

            List<Dictionary<string, string>> fieldMapLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(fieldMapping); ;//反序列化
            Dictionary<string, object> otherSettingDic = othersetting.IsNullOrWhiteSpace() ? null : othersetting.Deserialize<Dictionary<string, object>>();
            //Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,object>>(othersetting);//反序列化
            Dictionary<string, object> dicRes = SummerFresh.Controls.Chart.CustomChartInnerV4(fieldMapLst, otherSettingDic, data);
                //CustomChartExtenstion.CustomChartInnerV4(fieldMapLst, otherSettingDic, data);

           //string result = Newtonsoft.Json.JsonConvert.SerializeObject(dicRes);//序列化

           return Json(dicRes,JsonRequestBehavior.AllowGet);
        }
    }
}
