﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.ComponentModel;
using SummerFresh.Business;
namespace SummerFresh.Controls
{
    /// <summary>
    /// 按钮
    /// </summary>
    [DisplayName("按钮")]
    public class Button : FormControlBase, IScriptComponent
    {
        public Button()
        {
            ID = "btn";
            CssClass = "btn btn-default";
            ButtonType = ButtonType.Button;
            Visiable = true;
        }

        public ButtonType ButtonType
        {
            get;
            set;
        }

        protected override bool SelfClosing
        {
            get
            {
                if (ButtonType == ButtonType.Button)
                {
                    return false;
                }
                return true;
            }
        }

        protected override string TagName
        {
            get
            {
                return "input";
            }
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        [DisplayName("单击事件")]
        public string OnClick { get; set; }

        internal override void AddAttributes()
        {
            if (Value.IsNullOrEmpty())
            {
                Value = Label;
            }
            Attributes["type"] = ButtonType.ToString().ToLower();
            Attributes["value"] = Value;
            if (!OnClick.IsNullOrEmpty())
            {
                Attributes["onclick"] = OnClick;
            }
            base.AddAttributes();
        }

        public string PageStartUpScript
        {
            get
            {
                return string.Empty;
            }
        }

        public string PageScriptBlock
        {
            get
            {
                if (Visiable && Enable && !OnClick.IsNullOrEmpty())
                {
                    return "function {0}_Click(){{{1}}}".FormatTo(ID, OnClick);
                }
                return string.Empty;
            }
        }
    }

    public enum ButtonType
    {
        Button,
        Submit,
        Reset
    }
}
