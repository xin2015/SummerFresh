using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class DefaultSortFieldAttribute:Attribute
    {
        public OrderByType OrderByType { get; set; }
        public DefaultSortFieldAttribute(OrderByType orderbyType)
        {
            OrderByType = orderbyType;
        }
    }

    public enum OrderByType
    {
        ASC,
        DESC    
    }
}
