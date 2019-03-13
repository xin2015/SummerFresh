﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace SummerFresh.Business
{
   public abstract class ColumnConverterBase:IColumnConverter
    {
       public string ColumnName
       {
           get;
           set;
       }

       public string AppendColumnName
       {
           get;
           set;
       }

       public int Rank
       {
           get;
           set;
       }

       public ColumnConverterType ConverterType
       {
           get;
           set;
       }

       [ScriptIgnore]
       [FormField(Editable=false)]
       public IDictionary<string, object> RowData
       {
           get;
           set;
       }

       [ScriptIgnore]
       protected virtual IFieldConverter FieldConverter { get { return null; } }

       public virtual object Converter(object value)
       {
           return FieldConverter.Converter(ColumnName, value, RowData);
       }

       public string ID
       {
           get;
           set;
       }
    }
}
