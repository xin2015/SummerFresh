using SummerFresh.Basic.FastReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using SummerFresh.Business.Entity;
using SummerFresh.Data.Attributes;
using SummerFresh.Business;
using System.ComponentModel;
using SummerFresh.Business.Service;
using System.Web;
using SummerFresh.Util;
namespace SummerFresh.Controls
{
    public static class EntityComponentHelper
    {
        public const string DefalutEntityAssembly = "SummerFresh.Business";
        public const string DefaultEntityNameTemplate = "SummerFresh.Business.Entity.{0}Entity";
        public const string DefaultEntityServiceTemplate = "SummerFresh.Business.Service.{0}EntityService";

        private static IDictionary<string, SummerFresh.Business.IComponent> ComponentCache = new Dictionary<string, SummerFresh.Business.IComponent>();

        private static object lockKey = new object();
        public static SummerFresh.Business.IComponent GetComponent(string key)
        {
            var component = CacheHelper.GetValue(key);
            if (component != null)
            {
                return component as SummerFresh.Business.IComponent;
            }
            return null;
        }

        public static Tab GetTabComponent(object instance, string parentId, string pageId)
        {
            var entityType = instance.GetType();
            var pis = FastType.Get(entityType).Setters;
            var tab = new Tab();
            tab.TabItems.Add(new TabItem()
            {
                ID = "baseInfo",
                TabName = entityType.GetDisplayName(),
                Visiable = true
            });
            var id = (instance as IControl).ID;
            foreach (var fp in pis)
            {
                var p = fp.Info;
                if (p.GetCustomAttribute<FormFieldAttribute>(true) != null)
                {
                    continue;
                }
                if (p.PropertyType.IsGenericType)
                {
                    if (p.PropertyType == typeof(IDictionary<string, object>))
                    {
                        continue;
                    }
                    var typeArgument = p.PropertyType.GetGenericArguments()[0];

                    var newTab = new TabItem()
                    {
                        ID = p.Name,
                        TabName = p.GetDisplayName(),
                        Visiable = true
                    };
                    newTab.Content.Add(new IFrame() { Src = "/Entity/List/Component?parentId={0}&targetId={1}&baseType={2}&pageId={3}".FormatTo(parentId, p.Name, typeArgument.FullName, pageId) });
                    tab.TabItems.Add(newTab);
                }
                else if (p.PropertyType.GetInterface(typeof(IControl).FullName, true) != null)
                {
                    var newItem = new TabItem()
                    {
                        ID = p.Name,
                        TabName = p.GetDisplayName(),
                        Visiable = true
                    };
                    newItem.Content.Add(new IFrame() { Src = "/PageDesigner/ComponentList?pageId={0}&typeName={1}&baseType={2}&targetId={3}&parentId={4}&parentName={5}".FormatTo(pageId, p.PropertyType.FullName, p.PropertyType.FullName, p.Name, parentId, id) });
                    tab.TabItems.Add(newItem);
                }
                else
                {
                    continue;
                }
            }
            return tab;
        }

        public static Form GetFormComponent(Type entityType)
        {
            string formId = NamingCenter.GetEntityFormId(entityType);
            string key = NamingCenter.GetCacheKey(CacheType.ENTITY_CONFIG, formId);
            var result = CacheHelper.GetFromCache<Form>(key, () =>
            {
                var returnValue = new Form();
                returnValue.ID = formId;
                var pis = FastType.Get(entityType).Setters;
                int rank = 0;
                foreach (var fp in pis)
                {
                    var p = fp.Info;
                    if (!(p.PropertyType == typeof(string)) && !p.PropertyType.IsValueType)
                    {
                        continue;
                    }
                    object defaultValue = null;
                    string label = string.Empty;
                    FormControlBase fieldComponent = null;
                    var keyAttr = p.GetCustomAttribute<PrimaryKeyAttribute>(true);
                    if (keyAttr != null)
                    {
                        fieldComponent = new HiddenField();
                        defaultValue = "$GUID$";
                    }
                    var formAttr = p.GetCustomAttribute<FormFieldAttribute>(true);
                    if (formAttr != null)
                    {
                        if (!formAttr.Editable)
                        {
                            continue;
                        }
                        label = formAttr.FormDisplayName;
                        defaultValue = formAttr.DefaultValue;
                        if (formAttr.ControlType != ControlType.None)
                        {
                            switch (formAttr.ControlType)
                            {
                                case ControlType.DateTimeRange:
                                case ControlType.DatePicker:
                                    fieldComponent = new DatePicker();
                                    break;
                                case ControlType.DropDownList:
                                    fieldComponent = new DropDownList();
                                    break;
                                case ControlType.Password:
                                    fieldComponent = new TextBox() { TextMode = TextMode.Password };
                                    break;
                                case ControlType.CheckBox:
                                    fieldComponent = new CheckBox();
                                    break;
                                case ControlType.CheckBoxList:
                                    fieldComponent = new CheckBoxList();
                                    break;
                                case ControlType.RadioButton:
                                    fieldComponent = new RadioButton();
                                    break;
                                case ControlType.RadioButtonList:
                                    fieldComponent = new RadioButtonList();
                                    break;
                                case ControlType.TextArea:
                                    fieldComponent = new TextArea();
                                    break;
                                case ControlType.TextEditor:
                                    fieldComponent = new TextEditor();
                                    break;
                                case ControlType.TextValueTextBox:
                                    fieldComponent = new TextValueTextBox();
                                    break;
                            }
                        }
                        if (!formAttr.ExtendField.IsNullOrEmpty())
                        {
                            string[] attrArray = formAttr.ExtendField.Split(new char[] { ',', '|',';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var attr in attrArray)
                            {
                                string[] kvp = attr.Split(':');
                                FastType ft = new FastType(fieldComponent.GetType());
                                FastProperty setter = ft.GetSetter(kvp[0]);
                                setter.SetValue(fieldComponent, kvp[1].ConventToType(setter.Type));
                            }
                        }
                    }
                    if (fieldComponent == null)
                    {
                        fieldComponent = TypeControlMapping.GetTypeControl(p.PropertyType);
                    }
                    
                    var formAttr1 = p.GetCustomAttribute<DataSourceAttribute>(true);
                    if (formAttr1 != null)
                    {
                        if (fieldComponent is IKeyValueDataSourceControl)
                        {
                            (fieldComponent as IKeyValueDataSourceControl).DataSource = formAttr1.GetDataSource() as IKeyValueDataSource;
                        }
                    }
                    if (p.PropertyType.IsEnum)
                    {
                        fieldComponent = new DropDownList() { DataSource = new EnumDataSource() { EnumType = p.PropertyType, EnumValueType = EnumValueType.Code } };
                    }
                    if (p.PropertyType == typeof(bool))
                    {
                        fieldComponent = new DropDownList() { DataSource = new DictionaryDataSource() { DictionaryCode = "YesOrNo" } };
                    }
                    fieldComponent.Description = p.GetDescription();
                    if (label.IsNullOrEmpty())
                    {
                        label = p.GetDisplayName();
                    }
                    var attr1 = p.GetCustomAttribute<DefaultValueAttribute>(true);
                    if (attr1 != null)
                    {
                        defaultValue = attr1.Value;
                    }
                    if (fieldComponent == null)
                    {
                        fieldComponent = TypeControlMapping.GetTypeControl(p.PropertyType);
                    }
                    var attr2 = p.GetCustomAttribute<ValidatorAttribute>(true);
                    if (attr2 != null)
                    {
                        fieldComponent.Validator = attr2.ValidateString;
                    }
                    var attr3 = p.GetCustomAttribute<TabItemAttribute>(true);
                    if (attr3 != null)
                    {
                        fieldComponent.Attributes["tabItemTag"] = attr3.TabItemName.IsNullOrEmpty() ? p.GetDisplayName() : attr3.TabItemName;
                    }
                    fieldComponent.Label = label;
                    fieldComponent.ID = p.Name;

                    fieldComponent.Rank = (++rank);
                    fieldComponent.Value = defaultValue == null ? "" : defaultValue.ToString();
                    returnValue.Fields.Add(fieldComponent);
                }
                foreach (var btn in ControlDefaultSetting.GetDefaultFormButton())
                {
                    returnValue.Buttons.Add(btn);
                }
                returnValue.Fields.Add(new HiddenField() { ID = CustomEntityModelBinder.EntityFullNameHiddenName, Value = entityType.FullName + "," + entityType.Assembly.FullName });
                return returnValue;
            });
            return result.Clone() as Form;
        }

        public static Toolbar GetToolbarComponent(Type entityType)
        {
            string id = NamingCenter.GetEntityToolbarId(entityType);
            string key = NamingCenter.GetCacheKey(CacheType.ENTITY_CONFIG, id);
            var result = CacheHelper.GetFromCache<Toolbar>(key, () =>
            {
                var service = GetEntityService(entityType);
                var returnValue = new Toolbar();
                returnValue.ID = id;
                var buttons = service.AddButtons();
                if (!buttons.IsNullOrEmpty())
                {
                    foreach (var b in buttons.Where(o => o.ButtonType == ButtonEntityType.Toolbar))
                    {
                        returnValue.Buttons.Add(new Button()
                        {
                            ID = b.ButtonControlId,
                            CssClass = b.CssClass,
                            OnClick = b.OnClick,
                            Value = b.ButtonName
                        });
                    }
                }
                return returnValue;
            });
            return result.Clone() as Toolbar;
        }

        public static Table GetTableComponent(Type entityType)
        {
            string tableId = NamingCenter.GetEntityTableId(entityType);
            string key = NamingCenter.GetCacheKey(CacheType.ENTITY_CONFIG, tableId);
            var result = CacheHelper.GetFromCache<Table>(key, () =>
            {
                var returnValue = new Table() { ShowCheckBox = true, ShowIndex = true, AutoHeight = true };
                string entityName = entityType.Name.Replace("Entity", "");
                returnValue.InsertUrl = "/Entity/Insert/" + entityName;
                returnValue.EditUrl = "/Entity/Edit/" + entityName;
                returnValue.DeleteUrl = "/Entity/Delete/" + entityName;
                var pis = entityType.GetProperties();
                var tableAttr = entityType.GetCustomAttribute<TableAttribute>(true);
                if (tableAttr != null)
                {
                    returnValue.ShowCheckBox = tableAttr.ShowCheckbox;
                    returnValue.ShowIndex = tableAttr.ShowIndex;
                    returnValue.AutoGenerateColum = tableAttr.AutoGenerateColum;
                    returnValue.AllowPaging = tableAttr.AllowPaging;
                }
                var displayAttr = entityType.GetCustomAttribute<DisplayNameAttribute>(true);
                if (displayAttr != null)
                {
                    returnValue.Attributes["fileName"] = displayAttr.DisplayName;
                }
                var piis = FastType.Get(entityType).Getters;
                string sortExpression = string.Empty;

                returnValue.ID = tableId;
                int rank = 10;
                foreach (var p in pis)
                {
                    var column = new TableColumn();
                    var attr2 = p.GetCustomAttribute<PrimaryKeyAttribute>(true);
                    if (attr2 != null)
                    {
                        column.IsKey = true;
                        returnValue.KeyFieldName = p.Name;
                    }
                    var attr6 = p.GetCustomAttribute<DefaultSortFieldAttribute>(true);
                    if (attr6 != null)
                    {
                        string orderByTemplete = ", " + Data.Dao.Get().Provider.NameFormat + " {1}";
                        string tempSortExpression = orderByTemplete.FormatTo(p.Name, attr6.OrderByType.ToString());
                        sortExpression += tempSortExpression;
                    }
                    var attr3 = p.GetCustomAttribute<TableFieldAttribute>(true);
                    if (attr3 != null)
                    {
                        if (!attr3.IsShow)
                        {
                            continue;
                        }
                        column.ColumnWidth = attr3.ColumnWidth;
                        column.TextAlign = attr3.TextAlign;
                        column.Sortable = attr3.Sortable;
                        column.DataFormatString = attr3.DataFormatString;
                        column.DataFormatFields = attr3.DataFormatFields;
                        column.ShowLength = attr3.ShowLength;
                        column.DefaultValue = attr3.DefaultValue;
                        column.ReferenceField = attr3.ReferenceField;
                        column.HtmlEncode = attr3.HtmlEncode;
                    }
                    column.ID = "column" + p.Name;
                    column.FieldName = p.Name;
                    column.Rank = rank;
                    rank += 10;
                    column.ColumnName = p.GetDisplayName();
                    var attr4 = p.GetCustomAttribute<TableColumnConverter>(true);
                    if (attr4 != null)
                    {
                        column.ColumnConverter = attr4.GetDataSource();
                    }
                    var attr7 = p.GetCustomAttribute<DataSourceAttribute>(true);
                    if (attr7 != null)
                    {
                        var ds = attr7.GetDataSource();
                        if (ds is IFieldConverter)
                        {
                            column.ColumnConverter = (ds as IFieldConverter);
                        }
                    }
                    if (p.PropertyType == typeof(bool))
                    {
                        column.ColumnConverter = new DictionaryDataSource() { DictionaryCode = "YesOrNo" } as IFieldConverter;
                    }
                    var attr5 = p.GetCustomAttribute<CategoryAttribute>(true);
                    if (attr5 != null)
                    {
                        var name = attr5.Category;
                        var Component = returnValue.Columns.FirstOrDefault(o => o.ColumnName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                        if (Component != null)
                        {
                            column.Rank = Component.Rank + (Component.Children.Count + 2);
                            Component.Children.Add(column);
                        }
                        else
                        {
                            Component = new TableColumn();
                            Component.ColumnName = name;
                            Component.Rank = column.Rank - 1;
                            Component.Children.Add(column);
                            returnValue.Columns.Add(Component);
                        }

                    }
                    else
                    {
                        returnValue.Columns.Add(column);
                    }
                }
                if (sortExpression.IsNullOrEmpty())
                {
                    sortExpression = Data.Dao.Get().Provider.NameFormat.FormatTo(returnValue.KeyFieldName);
                }
                else
                {
                    sortExpression = sortExpression.Substring(1);
                }
                var entityService = GetEntityService(entityType);
                var button = entityService.AddButtons();
                if (!button.IsNullOrEmpty())
                {
                    returnValue.Buttons = new List<TableButton>();
                    foreach (var b in button.Where(o => o.ButtonType == ButtonEntityType.TableRow))
                    {
                        returnValue.Buttons.Add(new TableButton()
                        {
                            ID = b.ButtonControlId,
                            CssClass = b.CssClass,
                            DataFields = b.DataFields,
                            OnClick = b.OnClick,
                            Name = b.ButtonName,
                            TableButtonType = TableButtonType.TableRow,
                            Visiable = true,
                            Rank = b.ButtonSortNumber
                        });
                    }
                }
                returnValue.DataSource = GetEntityService(entityType).DataSource;
                returnValue.DataSource.SortExpression = sortExpression;
                return returnValue;
            });
            return result.Clone() as Table;
        }

        public static Search GetSearchComponent(Type entityType)
        {

            string searchId = NamingCenter.GetEntitySearchId(entityType);
            string key = NamingCenter.GetCacheKey(CacheType.ENTITY_CONFIG, searchId);
            var result = CacheHelper.GetFromCache<Search>(key, () =>
            {
                var returnValue = new Search();
                var pis = entityType.GetProperties();
                int rank = 0;
                returnValue.ID = searchId;
                object defaultValue = null;
                foreach (var p in pis)
                {
                    var searchAttr = p.GetCustomAttribute<SearchFieldAttribute>(true);
                    if (searchAttr == null || searchAttr.IsSearchControl == false)
                    {
                        continue;
                    }

                    string label = string.Empty;
                    FormControlBase fieldComponent = null;
                    var formAttr = p.GetCustomAttribute<FormFieldAttribute>(true);
                    if (formAttr != null)
                    {
                        label = formAttr.FormDisplayName;
                        defaultValue = formAttr.DefaultValue;
                        if (formAttr.ControlType != ControlType.None)
                        {
                            switch (formAttr.ControlType)
                            {
                                case ControlType.DatePicker:
                                    fieldComponent = new DatePicker();
                                    break;
                                case ControlType.DropDownList:
                                    fieldComponent = new DropDownList();
                                    break;
                                case ControlType.Password:
                                    fieldComponent = new TextBox() { TextMode = TextMode.Password };
                                    break;
                                case ControlType.CheckBox:
                                    fieldComponent = new CheckBox();
                                    break;
                                case ControlType.CheckBoxList:
                                    fieldComponent = new CheckBoxList();
                                    break;
                                case ControlType.RadioButton:
                                    fieldComponent = new RadioButton();
                                    break;
                                case ControlType.RadioButtonList:
                                    fieldComponent = new RadioButtonList();
                                    break;
                                case ControlType.TextArea:
                                    fieldComponent = new TextArea();
                                    break;
                                case ControlType.TextEditor:
                                    fieldComponent = new TextEditor();
                                    break;
                                case ControlType.DateTimeRange:
                                    fieldComponent = new TimeRangePicker();
                                    break;
                                case ControlType.CheckLabelList:
                                    fieldComponent = new CheckLabelList();
                                    break;
                            }
                        }
                    }
                    if (fieldComponent == null)
                    {
                        fieldComponent = TypeControlMapping.GetTypeControl(p.PropertyType);
                    }
                    var formAttr1 = p.GetCustomAttribute<DataSourceAttribute>(true);
                    if (formAttr1 != null)
                    {
                        if (fieldComponent is IKeyValueDataSourceControl)
                        {
                            (fieldComponent as IKeyValueDataSourceControl).DataSource = formAttr1.GetDataSource() as IKeyValueDataSource;
                        }
                    }
                    fieldComponent.Description = p.GetDescription();
                    if (p.PropertyType.IsEnum)
                    {
                        fieldComponent = new DropDownList() { DataSource = new EnumDataSource() { EnumType = p.PropertyType, EnumValueType = EnumValueType.Code } };
                    }
                    if (p.PropertyType == typeof(bool))
                    {
                        fieldComponent = new DropDownList() { DataSource = new DictionaryDataSource() { DictionaryCode = "YesOrNo" } };
                    }
                    if (label.IsNullOrEmpty())
                    {
                        label = p.GetDisplayName();
                    }
                    fieldComponent.Label = label;
                    fieldComponent.ID = p.Name;
                    fieldComponent.Rank = (++rank);
                    fieldComponent.Value = defaultValue == null ? "" : defaultValue.ToString();
                    returnValue.Fields.Add(fieldComponent);
                }
                foreach (var btn in ControlDefaultSetting.GetDefaultSearchButton())
                {
                    returnValue.Buttons.Add(btn);
                }
                return returnValue;
            });
            return result.Clone() as Search;
        }

        private static IDictionary<Type, CustomEntityService> TypeServiceMapping = new Dictionary<Type, CustomEntityService>();
        private static object syncRoot = new object();
        public static void RegisterEntityService(Type entityType, CustomEntityService service)
        {
            lock (syncRoot)
            {
                TypeServiceMapping[entityType] = service;
            }
        }

        public static CustomEntityService GetEntityService(Type entityType)
        {
            if (TypeServiceMapping.Keys.Contains(entityType))
            {
                var service = TypeServiceMapping[entityType];
                service.EntityType = entityType;
                return service;
            }
            var attr = entityType.GetCustomAttribute<EntityServiceAttribute>(true);
            if (attr != null)
            {
                var instance = attr.Service();
                instance.EntityType = entityType;
                return instance;
            }
            string entityName = entityType.Name.Replace("Entity", "");
            var type = entityType.Assembly.GetType(DefaultEntityServiceTemplate.FormatTo(entityName));
            if (type != null)
            {
                var instance = Activator.CreateInstance(type) as CustomEntityService;
                if (instance == null)
                {
                    throw new Exception("类型{0}必须继承自CustomEntityService类".FormatTo(DefaultEntityServiceTemplate.FormatTo(entityName)));
                }
                instance.EntityType = entityType;
                instance.Request = HttpContext.Current.Request;
                return instance;
            }
            return new CustomEntityService() { EntityType = entityType };
        }
    }
}
