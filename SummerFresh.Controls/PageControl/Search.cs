using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web;
using SummerFresh.Environment;
using System.ComponentModel;
using System.Web.Script.Serialization;
namespace SummerFresh.Controls
{
    /// <summary>
    /// 搜索组件
    /// </summary>
    [DisplayName("搜索组件")]
    public class Search : PageControlBase, ITargetId, IAutoGenerate
    {
        public Search()
        {
            Fields = new List<FormControlBase>();
            CssClass = "search pt10 pl10";
            FormCssClass = "form-inline";
            FormLayoutType = SummerFresh.Controls.FormLayoutType.Vertical;
            Buttons = new List<Button>();
        }

        /// <summary>
        /// 目标控件
        /// </summary>
        [DisplayName("目标控件")]
        public string TargetId { get; set; }

        public void SetTarget(IList<IControl> components)
        {
            if (!Fields.IsNullOrEmpty())
            {
                var formData = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                string key = string.Empty;
                var request = HttpContext.Current.Request;
                foreach (var f in Fields)
                {
                    if (f is TimeRangePicker)
                    {
                        //日期范围控件特殊处理
                        var dt = f as TimeRangePicker;
                        if (!dt.DefaultStartValue.IsNullOrEmpty())
                        {
                            formData.Add(NamingCenter.GetTimeRangeDatePickerStartId(dt.Name), DateTime.Parse(Env.Parse(dt.DefaultStartValue)).ToString(dt.DateTimeFormat));
                        }
                        if (!dt.DefaultEndValue.IsNullOrEmpty())
                        {
                            formData.Add(NamingCenter.GetTimeRangeDatePickerEndId(dt.Name), DateTime.Parse(Env.Parse(dt.DefaultEndValue)).ToString(dt.DateTimeFormat));
                        }
                    }
                    else if (f is DatePicker)
                    {
                        if (!f.Value.IsNullOrEmpty())
                        {
                            key = f.Name.IsNullOrEmpty() ? f.ID : f.Name;
                            var d = f as DatePicker;
                            formData.Add(key, DateTime.Parse(Env.Parse(f.Value)).ToString(d.DateTimeFormat));
                        }
                    }
                    else
                    {
                        if (!f.Value.IsNullOrEmpty())
                        {
                            key = f.Name.IsNullOrEmpty() ? f.ID : f.Name;
                            if (!request.Form.AllKeys.Contains(key) && !request.QueryString.AllKeys.Contains(key))
                            {
                                formData.Add(key, Env.Parse(f.Value));
                            }
                        }
                    }
                }
                foreach (var c in components)
                {
                    if (c is IListDataSourceControl)
                    {
                        var ds = (c as IListDataSourceControl).DataSource;
                        if (ds != null)
                        {
                            if (ds.Parameter == null)
                            {
                                ds.Parameter = formData;
                            }
                            else
                            {
                                var dict = ds.Parameter as Dictionary<string, object>;
                                foreach (var f in formData.Keys)
                                {
                                    dict[f] = formData[f];
                                }
                            }
                        }
                    }
                }
            }
        }

        public FormLayoutType FormLayoutType { get; set; }

        /// <summary>
        /// 搜索字段
        /// </summary>
        [DisplayName("搜索字段")]
        public IList<FormControlBase> Fields { get; set; }

        /// <summary>
        /// 搜索按钮
        /// </summary>
        [DisplayName("搜索按钮")]
        public IList<Button> Buttons { get; set; }

        public string SearchFormTemplate { get; set; }

        public override string Render()
        {
            if (Fields.Count(o => o.Visiable) == 0)
            {
                return string.Empty;
            }
            if (FormLayoutType == SummerFresh.Controls.FormLayoutType.Horizontal)
            {
                CssClass = "search";
                FormCssClass = "form-list";
            }
            else
            {
                if(CssClass.Equals("search", StringComparison.OrdinalIgnoreCase))
                {
                    CssClass = "search pt10 pl10";
                }
            }
            return base.Render();
        }

        public override void AddChildren(string property, object component)
        {
            if (property.Equals("Fields"))
            {
                this.Fields.Add(component as FormControlBase);
            }
            if (property.Equals("Buttons"))
            {
                Buttons.Add(component as Button);
            }
            base.AddChildren(property, component);
        }

        public override string RenderContent()
        {
            if (Fields.Count == 0)
            {
                throw new CustomException("Fields不能为空");
            }
            StringBuilder result = new StringBuilder();
            SearchFormTemplate = TemplateGenerator.GetSearchFormTemplate();
            string searchFieldTemplate = TemplateGenerator.GetFormFieldTemplate();
            string buttonTemplate = string.Empty;
            if (!Buttons.IsNullOrEmpty())
            {
                buttonTemplate = "<div class=\"search-control\"><div class=\"form-group\">{0}</div></div>";
                StringBuilder buttonResult = new StringBuilder();
                foreach (var button in Buttons.OrderBy(o => o.Rank))
                {
                    buttonResult.AppendLine(button.Render());
                }
                buttonTemplate = buttonTemplate.FormatTo(buttonResult.ToString());
            }
            foreach (var field in Fields.OrderBy(o => o.Rank))
            {
                if (field is HiddenField) continue;
                if (field.ContainerTemplate.IsNullOrEmpty())
                {
                    field.ContainerTemplate = searchFieldTemplate;
                }
                if (field.Name.IsNullOrEmpty())
                {
                    field.Name = field.ID;
                }
                if (!HttpContext.Current.Request.QueryString[field.ID].IsNullOrEmpty())
                {
                    //如果URL参数中含有与当前查询控件ID一致的键值，则以URL参数的值赋初始值，并且改当前查询控件为禁用状态
                    field.Value = HttpContext.Current.Request.QueryString[field.ID];
                }
                else
                {
                    field.Enable = true;
                    if (field.Value != null)
                    {
                        field.Value = Env.Parse(field.Value.ToString());
                    }
                }
                result.AppendLine(field.Render());
            }
            return SearchFormTemplate.FormatTo(FormCssClass, result.ToString(), buttonTemplate);
        }

        public string FormCssClass
        {
            get;
            set;
        }

        public override object Clone()
        {
            var result = base.Clone() as Search;
            result.Buttons = new List<Button>();
            foreach (var button in Buttons)
            {
                result.Buttons.Add(button.Clone() as Button);
            }
            result.Fields = new List<FormControlBase>();
            foreach (var field in Fields)
            {
                result.Fields.Add(field.Clone() as FormControlBase);
            }
            return result;
        }

        public IControl Generate(IList<Business.Entity.DataFieldEntity> fields, string moduleName)
        {
            ID = "{0}Search".FormatTo(moduleName);
            TargetId = "{0}Table".FormatTo(moduleName);
            this.Rank = 1;
            foreach (var field in fields.Where(o => o.FieldEditable).OrderBy(o => o.Rank))
            {
                var f = ControlDefaultSetting.GetFormControl(field);
                f.ID = field.FieldName;
                f.Name = field.FieldName;
                f.Label = field.FieldComment;
                f.Rank = field.Rank;
                f.Enable = true;
                f.Visiable = true;
                this.Fields.Add(f);
            }
            foreach (var btn in ControlDefaultSetting.GetDefaultSearchButton())
            {
                this.Buttons.Add(btn);
            }
            return this;
        }
    }

    public enum FormLayoutType
    {
        Vertical,
        Horizontal
    }
}
