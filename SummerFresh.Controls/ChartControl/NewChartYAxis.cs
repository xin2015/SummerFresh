using SummerFresh.Basic.FastReflection;
using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Collections;
using System.Web.Script.Serialization;
using System.Web;

namespace SummerFresh.Controls
{
    public class NewChartYAxis : SummerFresh.Business.IComponent, IChildren,ISerializeable
    {
        public NewChartYAxis()
        { 
            YAxisExtMembers = new List<NewChartOptionMember>();
            (this as IChildren).Controls = new List<IControl>();
            try
            {
                if (HttpContext.Current.Request.QueryString.AllKeys.Contains("targetId"))
                {
                    this.ID = HttpContext.Current.Request.QueryString["targetId"] + "YAxis";
                }
            }
            catch(Exception ex)
            {
                this.ID = "YAxis";
            }
        }

        IList<IControl> IChildren.Controls
        {
            get;
            set;
        }

        public void AddChildren(string property, object component)
        {
            if (component is NewChartOptionMember)
            {
                YAxisExtMembers.Add(component as NewChartOptionMember);
            }
        }

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

        string SummerFresh.Business.IComponent.Render()
        {
            return string.Empty;
        }


        public string ID
        {
            get;
            set;
        }
        public int Rank
        {
            get;
            set;
        }

        [DisplayName("标签颜色")]
        public string Labels_Style_Color { get; set; }

        [DisplayName("标题")]
        public string Title_Text { get; set; }

        [DisplayName("标题颜色")]
        public string Title_Style_Color { get; set; }

        [DisplayName("网格宽度")]
        public string GridLineWidth { get; set; }

        [DisplayName("放置右方")]
        public bool Opposite { get; set; }

        [DisplayName("坐标轴颜色")]
        public string LineColor { get; set; }

        [DisplayName("网格颜色")]
        public string GridColor { get; set; }

        [DisplayName("最大值")]
        public string Max { get; set; }

        [DisplayName("最小值")]
        public string Min { get; set; }

        [DisplayName("扩展属性")]
        public IList<NewChartOptionMember> YAxisExtMembers { get; set; }

        public Dictionary<string, object> GetJson()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            FastProperty[] propArr = FastType.Get(this.GetType()).Getters;
            foreach (var propInfo in propArr)
            {
                if (propInfo.Info.GetCustomAttribute<DisplayNameAttribute>(true) == null ||
                   propInfo.Type.IsGenericType) continue;
                SetPropValue(result, propInfo.Name.ToLower().Split('_').ToList(), propInfo.GetValue(this));
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (NewChartOptionMember item in YAxisExtMembers)
            {
                SetPropValue(result, item.MemberName.Split('.').ToList(), serializer.Deserialize<object>(item.MemberJson));
            }

            return result;
        }

        private void SetPropValue(Dictionary<string, object> parentInstance, List<string> propLst, object value)
        {
            if (value == null || parentInstance == null || propLst == null) return;
            if (value is string && (value as string).IsNullOrWhiteSpace()) return;
            for (int i = 0; i < propLst.Count; i++)
            {
                if (i == propLst.Count - 1)
                    parentInstance[propLst[i]] = value;//.GetValue(this);
                else
                {
                    if (!parentInstance.ContainsKey(propLst[i]))
                        parentInstance[propLst[i]] = new Dictionary<string, object>();
                    parentInstance = parentInstance[propLst[i]] as Dictionary<string, object>;
                }
            }
        }

        public Dictionary<string, object> Serialize()
        {
            return GetJson();
        }
    }
}
