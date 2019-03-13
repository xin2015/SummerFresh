using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Business;
using SummerFresh.Controls;
using System.Text;
using SummerFresh.Business.Entity;
namespace SummerFresh.MVC.Controllers
{
    public class SelectorController : BaseController
    {
        public ActionResult DDSelector()
        {
            string dictionaryCode = Request["DictionaryCode"];
            var ckList = new CheckBoxList();
            ckList.ID = "ddList";
            ckList.DataSource = new DictionaryDataSource() { DictionaryCode = dictionaryCode };
            ViewData["Ck"] = ckList;
            return View();
        }
    }
}
