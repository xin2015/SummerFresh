using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SummerFresh.Security.Authentication;
using SummerFresh.Security.Authorization;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Store;
using SummerFresh.Util;

namespace SummerFresh.Security
{
    /// <summary>
    /// 对Security项目中涉及到的 授权、认证、用户信息操作、安全机制信息存储（store） 这些操作类提供外放
    /// 调用者毋需关心类的构造过程
    /// </summary>
    public static class SecurityFactory
    {
        private static ISecurityStore         _store;
        private static ISecurityProvider      _provider;
        private static ISecurityAuthorizer    _authorizer;
        private static ISecurityAuthenticator _authenticator;

        static SecurityFactory()
        {
            Initialize();
        }

        public static ISecurityProvider Provider
        {
            get { return _provider;}
        }

        public static ISecurityStore Store
        {
            get { return _store; }
        }

        public static ISecurityAuthenticator Authenticator
        {
            get { return _authenticator; }
        }

        public static ISecurityAuthorizer Authorizer
        {
            get { return _authorizer; }
        }

        private static void Initialize()
        {

            //ISecurityStore)
            if (!ObjectHelper.TryGetObject<ISecurityStore>(out _store))
            {
                _store = new SecurityStore();
            }

            //ISecurityAuthenticator
            if (!ObjectHelper.TryGetObject<ISecurityAuthenticator>(out _authenticator))
            {
                _authenticator = new SecurityAuthenticator();
            }

            //ISecurityAuthorizer
            if (!ObjectHelper.TryGetObject<ISecurityAuthorizer>(out _authorizer))
            {
                _authorizer = new SecurityAuthorizer();
            }
            
            //ISeurityProvider
            if (!ObjectHelper.TryGetObject<ISecurityProvider>(out _provider))
            {
                _provider = new SecurityProvider();
            }
        }
    }
}