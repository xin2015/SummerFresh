using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SummerFresh.Basic;

namespace SummerFresh.Business
{
    [DisplayName("通用列传行过滤器")]
    public class CommonColumnRowConveter : IDataFilter
    {

        public int Rank
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string OldNameValueFieldNames { get; set; }

        public string NewNameFieldName { get; set; }

        public string NewValueFieldName { get; set; }

        public FieldHandleType FieldHandleType { get; set; }

        public IList<IDictionary<string, object>> Conveter(IList<IDictionary<string, object>> data)
        {
            if (data.IsNullOrEmpty())
            {
                return data;
            }
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            List<string> oldNameFieldArr = OldNameValueFieldNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            switch (FieldHandleType)
            {
                case FieldHandleType.Contain:
                    {
                        Dictionary<string, object> moduleRow;
                        foreach (IDictionary<string, object> row in data)
                        {
                            moduleRow = new Dictionary<string, object>();
                            foreach (var col in row)
                            {
                                if (!oldNameFieldArr.Contains(col.Key))
                                {
                                    moduleRow[col.Key] = col.Value;
                                }
                            }
                            foreach (var col in row)
                            {
                                if (oldNameFieldArr.Contains(col.Key))
                                {
                                    Dictionary<string, object> newRow = new Dictionary<string, object>(moduleRow);
                                    newRow[NewNameFieldName] = col.Key;
                                    newRow[NewValueFieldName] = col.Value;
                                    result.Add(newRow);
                                }
                            }
                        }
                        break;
                    }
                case FieldHandleType.Except:
                    {
                        Dictionary<string, object> moduleRow;
                        foreach (IDictionary<string, object> row in data)
                        {
                            moduleRow = new Dictionary<string, object>();
                            foreach (var col in row)
                            {
                                if (oldNameFieldArr.Contains(col.Key))
                                {
                                    moduleRow[col.Key] = col.Value;
                                }
                            }
                            foreach (var col in row)
                            {
                                if (!oldNameFieldArr.Contains(col.Key))
                                {
                                    Dictionary<string, object> newRow = new Dictionary<string, object>(moduleRow);
                                    newRow[NewNameFieldName] = col.Key;
                                    newRow[NewValueFieldName] = col.Value;
                                    result.Add(newRow);
                                }
                            }
                        }

                        break;
                    }
                case FieldHandleType.Like:

                    return this.ConvertLikeTypeData(data);

                default:
                    result.AddRange(data);
                    break;
            }

            return result;
        }

        private IList<IDictionary<string, object>> ConvertLikeTypeData(IList<IDictionary<string, object>> data)
        {
            List<string> oldNameLst = this.OldNameValueFieldNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> newNameLst = this.NewNameFieldName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> newValueLst = this.NewValueFieldName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<MatchItemModel> NewColLst = new List<MatchItemModel>();
            for (int i = 0; i < oldNameLst.Count; i++)
            {
                NewColLst.Add(new MatchItemModel(oldNameLst[i]) { NewValueField = newValueLst[i], NewNameField = newNameLst, Index = i });
            }
            NewColLst = NewColLst.OrderBy(c => c.WildcardIndex.Count).ToList();
            for (int i = 0; i < NewColLst.Count - 1; i++)
            {
                if (!NewColLst[i].WildcardIsContain(NewColLst[i + 1]))
                {
                    return data;
                }
            }
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            Dictionary<string, object> normalDatas;
            bool isMatch;
            foreach (IDictionary<string, object> row in data)
            {
                normalDatas = new Dictionary<string, object>();
                foreach (var col in row)
                {
                    isMatch = false;
                    foreach (MatchItemModel item in NewColLst)
                    {
                        if (item.FieldNameIsMatch(col))
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    if (!isMatch)
                    {
                        normalDatas.Add(col.Key, col.Value);
                    }
                }

                MatchItemModel child;
                MatchItemModel parent;
                bool hasParent;
                for (int i = NewColLst.Count - 1; i > 0; i--)
                {
                    child = NewColLst[i];
                    parent = NewColLst[i - 1];
                    for (int j = 0; j < child.Result.Count; j++)
                    {
                        hasParent = false;
                        for (int k = 0; k < parent.Result.Count; k++)
                        {
                            if (parent.Result[k].NewNameLstIsContain(child.Result[j]))
                            {
                                hasParent = true;
                                parent.Result[k].Childs.Add(child.Result[j]);
                                break;
                            }
                        }
                        if (!hasParent)
                        {
                            result.AddRange(GetDataRows(normalDatas, child.Result[j], child));
                        }
                    }
                }
                NewColLst[0].Result.ForEach(c => result.AddRange(GetDataRows(normalDatas, c, NewColLst[0])));

                foreach (var newCol in NewColLst)
                {
                    newCol.Result.Clear();
                }
            }
            return result;
        }

        private List<Dictionary<string, object>> GetDataRows(Dictionary<string, object> normalDatas, MatchResultModel model, MatchItemModel matchItem)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> current = new Dictionary<string, object>();
            foreach (var kvp in normalDatas)
            {
                current.Add(kvp.Key, kvp.Value);
            }
            current[matchItem.NewValueField] = model.Value;

            foreach (var kvp in model.NewNameLst)
            {
                current[matchItem.NewNameField[kvp.Key]] = kvp.Value;
            }
            if (model.Childs.Count > 0)
            {
                model.Childs.ForEach(c => { result.AddRange(GetDataRows(current, c, c.MatchItem)); });
            }
            else
            {
                result.Add(current);
            }
            return result;
        }

        class MatchItemModel
        {
            public MatchItemModel(string oldName)
            {
                this.OrgStatement = oldName;
                WildcardIndex = new List<int>();
                RegexStr = Regex.Replace(OrgStatement, "{\\d+}", "\\w+");
                MatchCollection mc = Regex.Matches(OrgStatement, "{\\d+}");
                foreach (Match m in mc)
                {
                    WildcardIndex.Add(Convert.ToInt32(m.Value.Substring(1, 1)));
                }
                Result = new List<MatchResultModel>();
            }

            public string OrgStatement { get; set; }

            public int Index { get; set; }

            /// <summary>
            /// 通配符索引集合
            /// </summary>
            public List<int> WildcardIndex { get; set; }

            /// <summary>
            /// 生成的正则
            /// </summary>
            public string RegexStr { get; private set; }


            public List<string> NewNameField { get; set; }

            public string NewValueField { get; set; }


            public bool FieldNameIsMatch(KeyValuePair<string, object> kvp)
            {
                bool result = Regex.IsMatch(kvp.Key, RegexStr);

                if (result)
                {
                    MatchResultModel model = new MatchResultModel();
                    model.MatchItem = this;
                    model.Value = kvp.Value;
                    string[] normalStrArr = this.RegexStr.Replace("\\w+", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int lastIndex = 0;
                    for (int i = 0; i < normalStrArr.Length; i++)
                    {
                        model.NewNameLst[WildcardIndex[i]] = kvp.Key.Substring(lastIndex, kvp.Key.IndexOf(normalStrArr[i]));
                        lastIndex = kvp.Key.IndexOf(normalStrArr[i]);
                    }

                    this.Result.Add(model);
                }

                return result;
            }

            public bool WildcardIsContain(MatchItemModel model)
            {
                if (this.WildcardIndex.Count > model.WildcardIndex.Count)
                {
                    return false;
                }
                else
                {
                    foreach (int item in this.WildcardIndex)
                    {
                        if (!model.WildcardIndex.Contains(item))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            public List<MatchResultModel> Result { get; set; }

        }

        class MatchResultModel
        {
            public MatchResultModel()
            {
                NewNameLst = new Dictionary<int, string>();
                Childs = new List<MatchResultModel>();
            }

            public Dictionary<int, string> NewNameLst { get; set; }

            public object Value { get; set; }

            public MatchItemModel MatchItem { get; set; }

            public bool NewNameLstIsContain(MatchResultModel model)
            {
                if (this.NewNameLst.Count > model.NewNameLst.Count)
                {
                    return false;
                }
                else
                {
                    foreach (var kvp in NewNameLst)
                    {
                        if (model.NewNameLst[kvp.Key] != kvp.Value)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            public List<MatchResultModel> Childs { get; set; }
        }
    }

    public enum FieldHandleType
    {
        [Description("包含")]
        Contain,
        [Description("排除")]
        Except,
        [Description("通配")]
        Like,
    }
}
