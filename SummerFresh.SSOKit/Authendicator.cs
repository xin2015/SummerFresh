using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace SummerFresh.SSOKit
{
    public static class Authendicator
    {
        public const string TOKEN_NAME = "token";

        public const string RETURN_URL = "return_url";

        public const string APP_KEY = "app_key";

        public const string SSO_URL = "sso_url";

        static Authendicator()
        {
            AppKey = ConfigurationManager.AppSettings[APP_KEY];
            SSOAddress = ConfigurationManager.AppSettings[SSO_URL];
        }

        public static string ReturnUrl { get; set; }

        public static string UserName { get; private set; }

        public static string SSOAddress { get; private set; }

        public static string AppKey { get; private set; }

        public static bool Authendicate()
        {
            if (string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(SSOAddress))
            {
                throw new Exception(string.Format("请在AppSettings配置节中增加Key为【{0}】及Key为【{1}】的配置项", APP_KEY, SSO_URL));
            }
            var request = HttpContext.Current.Request;
            if (request.Params.AllKeys.Contains(TOKEN_NAME))
            {
                string token = request.Params[TOKEN_NAME];
                string result = string.Empty;
                if (Cryptogram.Decrypt(token, AppKey, out result))
                {
                    string[] resultArr = result.Split(';');
                    string appKey = resultArr[0];
                    string userName = resultArr[1];
                    string expiretime = resultArr[2];
                    if (appKey.Equals(AppKey, StringComparison.OrdinalIgnoreCase)
                        && DateTime.Parse(expiretime) > DateTime.Now)
                    {
                        UserName = userName;
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GetSSOUrl()
        {
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                ReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            }
            string url = SSOAddress;
            if (!SSOAddress.StartsWith("http://"))
            {
                url = string.Format("http://{0}", SSOAddress);
            }
            url = string.Format("{0}?{1}={2}&{3}={4}", url, RETURN_URL, ReturnUrl, APP_KEY, AppKey);
            return url;
        }

        public static string GetSSOLogOutUrl()
        {
            string url = SSOAddress;
            if (!SSOAddress.StartsWith("http://"))
            {
                url = string.Format("http://{0}", SSOAddress);
            }
            if(!url.EndsWith("/"))
            {
                url = url + "/";
            }
            return SSOAddress + "Home/LogOut";
        }

        public static string GetToken(string appKey, string userName)
        {
            string expiretime = GetExpireTime();
            string originalString = string.Format("{0};{1};{2}", appKey, userName, expiretime);
            return Cryptogram.Encrypt(originalString, appKey);
        }

        public static string GetExpireTime()
        {
            return DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
