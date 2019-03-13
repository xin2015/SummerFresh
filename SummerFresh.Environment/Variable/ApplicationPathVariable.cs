using System;
using System.Web;

namespace SummerFresh.Environment.Variable
{
    class ApplicationPathVariable : BaseVariable
    {
        public override object Evaluate()
        {
            if (null == HttpContext.Current)
            { return String.Empty; }

            String applicationPath = HttpContext.Current.Request.ApplicationPath;
            return ("/".Equals(applicationPath) || String.IsNullOrEmpty(applicationPath)) ? String.Empty :
            applicationPath;
        }
    }
}
