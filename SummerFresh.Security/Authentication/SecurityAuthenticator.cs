using System;
using System.Security.Cryptography;
using System.Text;
using SummerFresh.Security.Principal;
using SummerFresh.Security.Store;
using SummerFresh.Util;

namespace SummerFresh.Security.Authentication
{
    /// <summary>
    /// 以缓存与非缓存两种方式获取认证信息
    /// </summary>
    public class SecurityAuthenticator : ISecurityAuthenticator
    {
        private static readonly ILog log = LogManager.GetCurrentClassLogger();

        private readonly Object lockObj = new object();

        protected ISecurityStore _store;
        protected HashAlgorithm _hashAlgorithm = MD5.Create();
        protected String _encryptionSalt;

        public virtual ISecurityStore Store
        {
            get { return _store ?? SecurityFactory.Store; }
            set { _store = value; }
        }

        public virtual HashAlgorithm HashAlgorithm
        {
            get { return _hashAlgorithm; }
            set { _hashAlgorithm = value; }
        }

        public virtual String EncryptionSalt {
            get { return String.IsNullOrEmpty(_encryptionSalt) ? "" : "{" + _encryptionSalt + "}"; } 
            set { _encryptionSalt = value; }
        }

        public virtual bool Authenticate(string username, string password)
        {
            IUser user = Store.GetUserLoginInfo(username);

            if (null == user)
            {
                log.Info("User Not Found , LoginId='{0}'",username);
                return false;
            }
            else
            {
                return EncryptPassword(password).Equals(user.Password);
            }
        }

        public virtual string EncryptPassword(string password)
        {
            return _hashAlgorithm == null ? password : Hash(password,_hashAlgorithm);
        }

        protected virtual string Hash(string text,HashAlgorithm algorithm)
        {
            //没同步时，压力测试下，并发会出错System.ObjectDisposedException: Safe handle has been closed
            lock(lockObj)
            {
                return HashToString(algorithm.ComputeHash(Encoding.UTF8.GetBytes(text + EncryptionSalt)));
            }            
        }

        protected virtual string HashToString(byte[] hashed)
        {
            return Convert.ToBase64String(hashed);
        }
    }
}