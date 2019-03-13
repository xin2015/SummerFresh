using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Math
{
    public class ComputeSum : IMathCompute
    {
        public double Compute(params double[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            double result = double.NaN;
            try
            {
                double data = 0;
                if (args.Length > 0)
                {
                    foreach (double item in args)
                    {
                        data += item;
                    }
                    result = data;
                }
            }
            catch (Exception ex)
            {   
                throw;
            }
            return result;
        }
    }

    public class ComputeAvg:IMathCompute
    {

        public double Compute(params double[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            double result = double.NaN;
            try
            {
                double data = 0;
                if (args.Length > 0)
                {
                    foreach (double item in args)
                    {
                        data += item;
                    }
                    result = (data / args.Length);
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return result;
        }
    }

    public class ComputeMax:IMathCompute
    {

        public double Compute(params double[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            double result = double.NaN;
            try
            {
                double data = double.MinValue, cur;
                if (args.Length > 0)
                {
                    foreach (double item in args)
                    {
                        cur = item;
                        if (cur > data)
                            data = cur;
                    }
                    result = data;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return result;
        }
    }

    public class ComputeMin:IMathCompute
    {

        public double Compute(params double[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            double result = double.NaN;
            try
            {
                double data = double.MaxValue, cur;
                if (args.Length > 0)
                {
                    foreach (double item in args)
                    {
                        cur = item;
                        if (cur < data)
                            data = cur;
                    }
                    result = data;
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return result;
        }
    }
}
