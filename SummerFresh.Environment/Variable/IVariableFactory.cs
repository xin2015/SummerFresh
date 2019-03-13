using SummerFresh.Environment.Config;

namespace SummerFresh.Environment.Variable
{
    public interface IVariableFactory
    {
        IEnvironmentVariable CreateVariable(VariableElement variable);
    }
}