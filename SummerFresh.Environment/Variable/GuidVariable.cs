using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Basic;
namespace SummerFresh.Environment.Variable
{
    /// <summary>
    /// Guid类型的环境变量
    /// </summary>
    class GuidVariable : BaseVariable
    {
        public GuidVariable()
        {

        }

        public GuidVariable(String name)
        {
            this.Name = name;
        }

        public override object Evaluate()
        {
            return Guid.NewGuid();
        }

        public override Scope Scope
        {
            get
            {
                return Scope.None;
            }
        }
    }

    public class DateTimeVariable : BaseVariable
    {
        public DateTimeVariable()
        {
            
        }

        private DateTime? _dt;
        public DateTimeVariable(DateTime dt)
        {
            _dt = dt;
        }

        public override object Evaluate()
        {
            if (_dt.HasValue)
            {
                return _dt.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public override Scope Scope
        {
            get
            {
                return Scope.None;
            }
        }
    }

    public class DateTimeEnvironment : IEnvironmentProvider
    {

        public string Prefix
        {
            get;
            set;
        }

        public bool Configure(IEnvironmentContainer container, Config.ProviderElement providerElement)
        {
            return true;
        }

        public IEnvironmentVariable GetVariable(string name)
        {
            if (!name.IsNullOrEmpty())
            {
                var days = name.ConverTo<int>();
                var dt = DateTime.Now.AddDays(days);
                return new DateTimeVariable(dt);
            }
            return new DateTimeVariable();
        }
    }
}
