using System;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using SummerFresh.Util;

namespace SummerFresh.Environment
{
    public class SessionProvider : ISessionProvider
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private static readonly string UserSessionKeyFormat = typeof (SessionProvider).FullName + "${0}";

        public bool IsValid
        {
            get { return null != HttpContext.Current && null != HttpContext.Current.Session; }
        }

        public SessionProvider()
        {

        }

        public object this[string name]
        {
            get
            {
                return SessionSate[name];
            }
            set
            {
                SessionSate[name] = value;
            }
        }

        public bool Remove(string name)
        {
            return SessionSate.Remove(name);
        }

        public void Clear()
        {
            ClearSessionState();
        }

        protected ISessionState SessionSate
        {
            get
            {
                return GetSessionState();
            }
        }

        protected virtual void ClearSessionState()
        {
            CheckSessionValid();

            string name = HttpContext.Current.User.Identity.Name;
            string key  = string.Format(UserSessionKeyFormat, name);

            ISessionState state = HttpContext.Current.Session[key] as ISessionState;

            if (null != state)
            {
                HttpContext.Current.Session.Remove(key);    

                state.Dispose();
            }
        }

        protected virtual ISessionState GetSessionState()
        {
            CheckSessionValid();

            HttpContext context = HttpContext.Current;
            IIdentity identity = context.User.Identity;
            
            string key = string.Format(UserSessionKeyFormat, identity.Name);
                    
            ISessionState state = context.Session[key] as ISessionState;

            if (null == state)
            {
                //TODO : 这里只需实现对同一会话的并发线程进行排它锁定即可，目前还没有好的机制实现
                //由于下面的语句速度很快，而且大量并发创建新的session state的情况很少会出现，所以暂时
                //实现对所有线程进行排它锁定
                lock (this)
                {
                    if ((state = context.Session[key] as ISessionState) == null)
                    {
                        log.Debug("Create Session State For User : {0}", identity.Name);

                        state = new SessionState();
                        context.Session[key] = state;
                    }
                }
            }

            return state;
        }

        protected virtual void CheckSessionValid()
        {
            if (null == HttpContext.Current.Session)
            {
                throw new InvalidOperationException("Asp.Net Session Object is Null,Make sure 'EnableSessionState' is true in your .aspx page");
            }
        }
    }
}