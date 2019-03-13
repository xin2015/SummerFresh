using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business.Math
{
    public class RoundFiveIn : IMathRound
    {
        public double Round(double val, int length)
        {
            double result = double.NaN;
            try
            {
                double dec = val;
                int intVal = Convert.ToInt32(dec * System.Math.Pow(10, length + 1));
                if (intVal % 10 >= 5)
                {
                    intVal += 10;
                }
                intVal = intVal / 10 * 10;
                dec = intVal * System.Math.Pow(10, -length - 1);
                result = dec;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }

    public class RoundSixIn : IMathRound
    {

        public double Round(double val, int length)
        {
            double result = double.NaN;
            try
            {
                result = System.Math.Round(val, length);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }

    public class RoundAddOne:IMathRound
    {

        public double Round(double val, int length)
        {
            double result = double.NaN;
            try
            {
                double dec = val;
                int intVal = Convert.ToInt32(dec);
                if (intVal < dec)
                    dec = intVal + 1;
                else
                    dec = intVal;
                result = dec;
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }

    public class RoundGetInteger : IMathRound
    {
        public double Round(double val, int length)
        {
            double result = double.NaN;
            try
            {
                double dec = val;
                dec = Convert.ToInt32(dec);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }

}
