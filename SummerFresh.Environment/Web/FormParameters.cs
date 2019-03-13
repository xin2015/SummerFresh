using System.Web;

namespace SummerFresh.Environment
{
    internal class FormParameters : AbstractHttpContextParameters
    {
        private const string Prefix = "Form:";
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
            if (Name.StartsWith(Prefix))
            {
                Name = Name.Remove(0, PrefixLength);
            }

            return HttpUtility.UrlDecode(HttpContext.Current.Request.Form[Name]);
        }
    }
}
