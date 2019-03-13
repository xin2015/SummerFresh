using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SummerFresh.Controls
{
    public class NewChartOptionMember : SummerFresh.Business.IComponent,ISerializeable
    {
        public NewChartOptionMember()
        {
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("targetId"))
                {
                    this.ID = HttpContext.Current.Request.QueryString["targetId"] + "ExtProp";
                }
            }
            catch (Exception ex)
            {
                this.ID = "extProp";
            }
        }

        #region 显式接口实现
        string SummerFresh.Business.IComponent.CssClass
        {
            get;
            set;
        }

        bool SummerFresh.Business.IComponent.Visiable
        {

            get;
            set;
        }


        int SummerFresh.Business.ISortableControl.Rank
        {

            get;
            set;
        }

        #endregion
        public string Render()
        {
            throw new NotImplementedException(); 
        }

        public string ID
        {
            get;
            set;
        }

        [DisplayName("成员名称")]
        public string MemberName { get; set; }

        [FormField(ControlType = ControlType.TextArea)]
        [DisplayName("成员定义内容")]
        public string MemberJson { get; set; }

        public Dictionary<string, object> Serialize()
        {
            JavaScriptSerializer jsSerializer=new  JavaScriptSerializer();
            return jsSerializer.Deserialize<object>(this.MemberJson)  as Dictionary<string,object>;
        }
    }
}
