using SummerFresh.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
using System.Web.Mvc;
namespace SummerFresh.Controls
{
    /// <summary>
    /// 计算列
    /// </summary>
    [DisplayName("计算列")]
    public class ComputeColumn : TableColumn, SummerFresh.Business.IComponent, ICloneable, IChildren
    {

        public override string Render()
        {
            if (ComputeNames.IsNullOrWhiteSpace())
            {
                throw new CustomException("计算列的计算范围为空，请在 ComputeNames 中填写参与计算的列名");
            }
            TagBuilder result = new TagBuilder("td");
            string[] colIndexArr = ComputeNames.Split(',');
            result.InnerHtml = ComputeResult(colIndexArr); ;
            return result.ToString();
        }

        protected string ComputeResult(string[] colKeyArr)
        {
            if (colKeyArr == null || colKeyArr.Length < 1)
                throw new ArgumentNullException("colKeyArr", "参与计算的列名为空");

            string result = this.EmptyValue;

            double[] computeNumberArr = new double[colKeyArr.Length];
            for (int i = 0; i < colKeyArr.Length; i++)
            {
                computeNumberArr[i] = Convert.ToDouble(RowData[colKeyArr[i]]);
            }
            double comRes = double.NaN;
            try
            {
                string computeClassName = "Compute" + TableColumnType.ToString();
                Type computeType = TypeHelper.GetAllImpl<IMathCompute>().Where(c => c.Name == computeClassName).FirstOrDefault();
                if (computeType == null)
                    throw new NotSupportedException("");
                IMathCompute compute = Activator.CreateInstance(computeType) as IMathCompute;

                comRes = compute.Compute(computeNumberArr);
            }
            catch (Exception ex)
            {
                comRes = double.NaN;
            }
            if (!double.IsNaN(comRes))
            {
                string roundClassName = "Round" + RoundType.ToString();
                Type roundType = TypeHelper.GetAllImpl<IMathCompute>().Where(c => c.Name == roundClassName).FirstOrDefault();
                if (roundType == null)
                    throw new NotSupportedException("");
                IMathRound round = Activator.CreateInstance(roundType) as IMathRound;

                comRes = round.Round(comRes, this.ReservedBits);
                result = comRes.ToString();
            }
            return result;
        }

        

        

        public ComputeColumn()
        {
            EmptyValue = "—";
            //CustomComuteJsMethod = "";
            TableColumnType = TableColumnType.Sum;
            ComputeCellLocateType = ComputeCellLocateType.Column;
            ColIndexOffset = 0;
            ReservedBits = 0;
        }

        [DisplayName("参与计算的列名")]
        public string ComputeNames { get; set; }

        //[DisplayName("自定义统计")]
        //public string CustomComuteJsMethod { get; set; }

        [DisplayName("空值显示内容")]
        public string EmptyValue { get; set; }

        [DisplayName("表格列统计类型")]
        public TableColumnType TableColumnType { get; set; }

        [DisplayName("统计元素所在位置")]
        public ComputeCellLocateType ComputeCellLocateType { get; set; }

        [DisplayName("保留位数")]
        public int ReservedBits { get; set; }

        [DisplayName("修约类型")]
        public RoundType RoundType { get; set; }

        [FormField(Editable = false)]
        public int ColIndexOffset { get; set; }
    }

}
