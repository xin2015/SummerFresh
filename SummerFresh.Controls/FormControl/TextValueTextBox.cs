using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
using System.Web;
using SummerFresh.Business;
namespace SummerFresh.Controls
{

    /// <summary>
    /// KV文本框
    /// </summary>
    [DisplayName("KV文本框")]
    public class TextValueTextBox : FormControlBase
    {
        public TextValueTextBox()
        {
            DialogWidth = 700;
            DialogHeight = 400;
        }

        protected override string TagName
        {
            get { return "div"; }
        }
        public string TextID { get; set; }

        [DisplayName("选择器名称")]
        public string SelectType { get; set; }

        [DisplayName("是否多选")]
        public bool IsMulitle { get; set; }

        public ShowType ShowType { get; set; }

        public int DialogWidth { get; set; }

        public int DialogHeight { get; set; }

        public override string Render()
        {
            if (Visiable)
            {
                AddAttributes();
                var texts = new List<string>();
                if (TextID.IsNullOrEmpty())
                {
                    TextID = "{0}_Text".FormatTo(ID);
                }
                if(!Value.IsNullOrEmpty() && !SelectType.IsNullOrEmpty())
                {
                    IFieldConverter listDs = null;
                    if (SelectType.StartsWith("DD"))
                    {
                        listDs = new DictionaryDataSource() { DictionaryCode = SelectType.Substring(2) };
                    }
                    else
                    {
                        var page = PageBuilder.BuildPage(SelectType, HttpContext.Current.Request);
                        if (page != null && page.Controls.Count > 0)
                        {

                            page.Controls.ForEach((o) =>
                            {
                                if (o is IListDataSourceControl)
                                {
                                    listDs = (o as IListDataSourceControl).DataSource as IFieldConverter;
                                    return;
                                }
                                else if (o is ICascadeDataSourceControl)
                                {
                                    listDs = (o as ICascadeDataSourceControl).DataSource as IFieldConverter;
                                    return;
                                }
                            });
                        }
                    }
                    if (listDs != null)
                    {
                        if (IsMulitle)
                        {
                            foreach (var v in Value.Split(','))
                            {
                                texts.Add(listDs.Converter(ID, v, null).ToString());
                            }
                        }
                        else
                        {
                            texts.Add(listDs.Converter(ID, Value, null).ToString());
                        }
                    }
                }
                string text = string.Empty;
                if(texts.Count>0)
                {
                    text = string.Join(",", texts.ToArray());
                }
                var textBox = new TextBox() { ID = TextID, Name = TextID, Value = text, AttributeString = "TVBox:true" };
                if (!Attributes.IsNullOrEmpty())
                {
                    foreach (var attr in Attributes)
                    {
                        textBox.Attributes[attr.Key] = attr.Value;
                    }
                }
                textBox.Attributes["readonly"] = "readonly";
                textBox.Attributes["onclick"] = "summerFresh.showSelect('{0}','{1}','{2}','{3}',{4},{5},{6})".FormatTo(SelectType, ShowType.ToString(), ID, TextID, IsMulitle.ToString().ToLower(),DialogHeight,DialogWidth);
                var hidden = new HiddenField() { ID = ID, Name = ID, Value = Value, Validator = Validator };
                string result = hidden.Render() + textBox.Render();
                return ContainerTemplate.FormatTo(ID, Label, result, Description);
            }
            return string.Empty;
        }
    }

    public enum ShowType
    {
        SlideDown,
        Modal
    }
}
