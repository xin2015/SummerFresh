using System.Web;

namespace SummerFresh.Environment
{
    internal class QueryStringParameters : AbstractHttpContextParameters
    {
        private const string Prefix = "QueryString:";
        private const string Prefix1 = "QueryString.";
        private static readonly int PrefixLength = Prefix.Length;

        protected override bool IsSupport(string name)
        {
            return true;
        }

        public override object Evaluate()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            //支持带前缀或者不带
            if (Name.StartsWith(Prefix, System.StringComparison.OrdinalIgnoreCase))
            {
                Name = Name.Remove(0, PrefixLength);
            }
            if (Name.StartsWith(Prefix1, System.StringComparison.OrdinalIgnoreCase))
            {
                Name = Name.Remove(0, Prefix1.Length);
            }
            return HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString[Name]);
        }
    }
}
