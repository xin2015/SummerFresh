using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public interface IMathCompute
    {
        //MathComputeType MathComputeType { get; }

        double Compute(params double[] args);
    }

    public interface IMathRound
    {
        //MathRoundType MathRoundType { get; }

        double Round(double val, int length);
    }
}
