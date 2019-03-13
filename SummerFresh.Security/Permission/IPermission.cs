using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Security.Principal;

namespace SummerFresh.Security.Permission
{
    public interface IPermission
    {
        /// <summary>
        /// 操作的唯一标识
        /// </summary>
        string Operation { get; }

        /// <summary>
        /// 权限的显示名称
        /// </summary>
        string Name { get; }
    }
}