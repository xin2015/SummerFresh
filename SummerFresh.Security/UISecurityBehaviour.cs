using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Security
{
    [Serializable]
    public class UISecurityBehaviour
    {
        public const string Invisible = "invisible";
        public const string Disabled = "disabled";

        private string _behaviour;

        public string Behaviour
        {
            get { return _behaviour; }
            set
            {
                _behaviour = value;
                IsInvisible = Invisible.Equals(_behaviour, StringComparison.OrdinalIgnoreCase);
                IsDisabled = Disabled.Equals(_behaviour, StringComparison.OrdinalIgnoreCase);
            }
        }

        public bool IsInvisible { get; protected set; }

        public bool IsDisabled { get; protected set; }
    }
}
