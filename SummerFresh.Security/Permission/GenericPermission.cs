using System;

namespace SummerFresh.Security.Permission
{
    /// <summary>
    /// 表示一个用户所拥有的权限，实体类
    /// </summary>
    [Serializable]
    public class GenericPermission : IPermission
    {
        /// <summary>
        /// 操作的唯一标识
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 权限的显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级操作的唯一标识
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// 此权限对应的Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 此权限对应的页面元素ID
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// 权限对应的安全规则内容
        /// </summary>
        public string Rule { get; set; }

        /// <summary>
        /// 安全规则的优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 表示作用在受控对象上的UI行为（如只读，隐藏等）
        /// </summary>
        public string Behaviour { get; set; }
    }
}