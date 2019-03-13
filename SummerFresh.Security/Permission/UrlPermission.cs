using System;

namespace SummerFresh.Security.Permission
{
    /// <summary>
    /// 表示一个Url访问权限
    /// </summary>
    [Serializable]
    public class UrlPermission : IPermission
    {
        public UrlPermission()
        {
            
        }

        public UrlPermission(string operation,string name,string url)
        {
            Operation = operation;
            Name = name;
            Url = url;
        }

        public string Operation { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        private string _virtualPath = string.Empty;
        private string _queryString = string.Empty;

        public string VirtualPath
        {
            get
            {
                if (string.IsNullOrEmpty(_virtualPath))
                {
                    ParseUrl();
                }

                return _virtualPath;                    
            }
        }

        public string QueryString
        {
            get
            {
                if (string.IsNullOrEmpty(_virtualPath))
                {
                    ParseUrl();
                }

                return _queryString;         
            }
        }

        private void ParseUrl()
        {
            if (string.IsNullOrEmpty(Url))
            {
                return;                
            }
            int index = Url.IndexOf("?");

            if (index > 0)
            {
                _virtualPath = Url.Substring(0, index);
                _queryString = Url.Substring(index + 1);
            }
            else
            {
                _virtualPath = Url;
                _queryString = string.Empty;
            }
        }        
    }
}