using SummerFresh.Business;
using SummerFresh.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SummerFresh.Basic;
using System.Threading;

namespace SummerFresh.Controls
{
    [DisplayName("扩展表单控件")]
    public class NewForm:Form
    {
        public NewForm()
        {
            CssClass = "form-inline form-fixed";
            ColSpan = ColumnCount.SingleColumn;
        }

        [EnumDataSource(typeof(ColumnCount))]
        [DisplayName("单据格式")]
        public ColumnCount ColSpan { get; set; }

        public override string RenderContent()
        {
            //return base.RenderContent();
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("form-fixed-list");

            if (Fields.Count == 0)
            {
                throw new CustomException("Fields不能为空");
            }
            var request = HttpContext.Current.Request;

            if (FormService != null && !FormService.KeyFieldName.IsNullOrEmpty())
            {
                string key = request[FormService.KeyFieldName];

                //当FORM指定的服务类不为空，且当前的请求中带有KEY的URL参数，
                //则尝试去获取表单数据，如果不为空，则为编辑状态
                if (!key.IsNullOrEmpty())
                {
                    var tempData = FormService.Get(key);
                    if (!tempData.IsNullOrEmpty())
                    {
                        FormMode = FormMode.Edit;
                        Data = tempData;
                    }
                }
            }
            IDictionary<string, object> formData = null;
            if (Data != null)
            {
                formData = Data as Dictionary<string, object>;
                if (formData.IsNullOrEmpty())
                {
                    formData = Data.ToDictionary();
                }
            }
            StringBuilder result = new StringBuilder();
            ///隐藏域和按钮属于特殊群体放在表单的指定位置
            StringBuilder hiddenResult = new StringBuilder();
            StringBuilder buttonResult = new StringBuilder();
            if (!Buttons.IsNullOrEmpty())
            {
                foreach (var button in Buttons)
                {
                    buttonResult.AppendLine(button.Render());
                }
            }
            var formModeHidden = new HiddenField() { ID = "FormViewMode", Value = request["FormViewMode"], Name = "FormViewMode" };
            hiddenResult.Append(formModeHidden.Render());
            var formTemplate = TemplateGenerator.GetDefaultFormTemplate();
            var renderQuerystringFinished = false;
            var preDefinedKey = "componentId,randomCode,FormViewMode".Split(',');
            if (AppendQueryString && !renderQuerystringFinished)
            {
                foreach (var r in request.QueryString.AllKeys)
                {
                    if (preDefinedKey.Contains(r, StringComparer.Create(Thread.CurrentThread.CurrentCulture, true)))
                    {
                        continue;
                    }
                    if (Fields.Count(o => o.ID.Equals(r, StringComparison.CurrentCultureIgnoreCase)) == 0)
                    {
                        hiddenResult.AppendLine(new HiddenField() { ID = r, Name = r, Value = request.QueryString[r] }.Render());
                    }
                }
                renderQuerystringFinished = true;
            }

            Dictionary<TagBuilder, List<FormControlBase>> liDic = new Dictionary<TagBuilder, List<FormControlBase>>();
            TagBuilder currli = null;

            foreach (var field in Fields.OrderBy(o => o.Rank))
            {
                if (field.Name.IsNullOrEmpty())
                {
                    field.Name = field.ID;
                }
                if (!formData.IsNullOrEmpty())
                {
                    if (formData.Keys.Contains(field.ID))
                    {
                        field.Value = (formData[field.ID] == null) ? "" : formData[field.ID].ToString();
                    }
                }
                else
                {
                    if (!request.QueryString[field.ID].IsNullOrEmpty())
                    {
                        //如果URL参数中含有与当前控件ID一致的键值，则以URL参数的值赋初始值
                        field.Value = request.QueryString[field.ID];
                    }
                    else
                    {
                        /// 当字段的值存在的时候从环境变量将值格式化
                        if (field.Value != null)
                        {
                            field.Value = Env.Parse(field.Value.ToString());
                        }
                    }
                }
                if (field is HiddenField)
                {
                    hiddenResult.AppendLine(field.Render());
                }
                else
                {
                    if (field.ContainerTemplate.IsNullOrEmpty())
                    {
                        field.ContainerTemplate = TemplateGenerator.GetFormFieldTemplate();
                    }

                    if (field.ColSpan)
                    {
                        currli = null;
                    }
                    if (currli == null || liDic[currli].Count == (int)this.ColSpan)
                    {
                        currli = new TagBuilder("li");
                        liDic[currli] = new List<FormControlBase>();
                    }
                    liDic[currli].Add(field);
                    if (field.ColSpan)
                    {
                        currli = null;
                    }

                }
            }
            foreach (var item in liDic)
            {
                StringBuilder sb = new StringBuilder();
                item.Value.ForEach(c => sb.AppendLine(c.ColSpan ? c.Render().Replace("class=\"form-group\"", "class=\"form-group form-group-block\"") : c.Render()));
                item.Key.InnerHtml = sb.ToString();
                ulTag.InnerHtml += item.Key.ToString();
            }
            return formTemplate.FormatTo(PostUrl, FormCssClass, hiddenResult.ToString(), ulTag.ToString(), buttonResult.ToString());
        }
    }

    public enum ColumnCount : int
    {
        [Description("一行一列")]
        SingleColumn = 1,
        [Description("一行两列")]
        TwoColumns = 2,
        [Description("一行三列")]
        ThirdColumns = 3,
    }
}
