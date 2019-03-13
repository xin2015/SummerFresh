using System;

namespace SummerFresh.Security.Cache
{
    /// <summary>
    /// 用户信息缓存，用于User类中
    /// </summary>
    public interface IUserState : IDisposable
    {
        object this[string name] { get; set; }

        object GetValue(string name, Func<object> value);

        bool TryGetValue(string name, out object value);

        bool Remove(string name);
    }
}
