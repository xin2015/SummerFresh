using System;

namespace SummerFresh.Security.Permission
{
    [Serializable]
    public class UIPermission : IPermission
    {
        public string Operation { get; set; }

        public string Name { get; set; }

        public string Parent { get; set; }

        public string Url { get; set; }

        public string ElementId { get; set; }

        public string ElementBehaviour { get; set; }
    }
}