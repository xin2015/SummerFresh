using SummerFresh.Business.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class EntityServiceAttribute:Attribute
    {
        private Type ServiceType { get; set; }
        public EntityServiceAttribute(Type type)
        {
            ServiceType = type;
        }
        public CustomEntityService Service()
        {
            var instance = Activator.CreateInstance(ServiceType) as CustomEntityService;
            return instance;
        }
    }
}
