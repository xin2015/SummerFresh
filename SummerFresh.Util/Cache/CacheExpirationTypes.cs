using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SummerFresh.Util.Cache
{
    /// <summary>
    /// Cache 类型值，时间长度
    /// </summary>
    public enum CacheExpirationTypes
    {
        /// <summary>
        /// 86400秒，24小时
        /// </summary>
        Invariable,
        /// <summary>
        /// 28800秒，8小时
        /// </summary>
        Stable,
        /// <summary>
        /// 7200秒，2小时
        /// </summary>
        RelativelyStable,
        /// <summary>
        /// 3600秒 1小时
        /// </summary>
        HourStable,
        /// <summary>
        /// 600秒
        /// </summary>
        UsualSingleObject,
        /// <summary>
        /// 300秒
        /// </summary>
        UsualObjectCollection,
        /// <summary>
        /// 1800秒
        /// </summary>
        SingleObject,
        /// <summary>
        /// 180秒
        /// </summary>
        ObjectCollection

    }
}
