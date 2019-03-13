namespace SummerFresh.Environment
{
    public interface ISessionProvider
    {
        /// <summary>
        /// 在当前上下文中会话是否有效
        /// </summary>
        /// <returns></returns>
        bool IsValid { get; }

        object this[string name] { get; set; }

        bool Remove(string name);

        void Clear();
    }
}