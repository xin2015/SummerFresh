using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Business
{
    public class CustomException : Exception
    {
        public CustomException(string message)
            : base(message)
        {

        }
    }

    public class PageNotFoundException : CustomException
    {
        public PageNotFoundException()
            : base("您访问的资源不存在或已被删除！")
        { }
    }

    public class UnAuthorizedException : CustomException
    {
        public UnAuthorizedException()
            : base("您没有访问当前资源的权限！")
        { }
    }
}
