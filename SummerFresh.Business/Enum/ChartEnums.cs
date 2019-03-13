using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{

    /// <summary>
    /// 图表字段类型枚举
    /// </summary>
    public enum ChartFieldTypeEnum
    {
        Data,
        Series,
        Category,
        SeriesStack,
        YAxis,
    }

    public enum AxisType
    {
        XAxis,
        YAxis,
    }


    public enum ChartTheme
    {
        DarkBlue,
        DarkGreen,
        DarkUnica,
        Gray,
        GridLight,
        Grid,
        SandSignika,
        Skies,
        Default,
    }


    public enum ChartType
    {
        /// <summary>
        /// 
        /// </summary>
        Line,
        Spline,
        Area,
        AreaSpline,
        Column,
        Bar,
        Pie,
        Scatter,
    }

    public enum ChartItemLocation : byte
    {
        Left = 1,
        Center = 2,
        Right = 4,
        Top = 8,
        Bottom = 16,
        Middle = 32,
    }
}
