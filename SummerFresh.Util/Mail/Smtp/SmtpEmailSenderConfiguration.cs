
using SummerFresh.Basic;
namespace SummerFresh.Util.Mail
{
    public class SmtpEmailSenderConfiguration 
    {
        public string Host
        {
            get { return SysConfig.MailHost; }
        }

        public int Port
        {
            get { return SysConfig.MailPort; }
        }

        public string UserName
        {
            get { return SysConfig.MailUserName; }
        }

        public string Password
        {
            get { return SysConfig.MailPassword; }
        }

        public string Domain
        {
            get { return ""; }
        }

        public bool EnableSsl
        {
            get { return true; }
        }

        public bool UseDefaultCredentials
        {
            get { return false; }
        }

        public string DefaultFromAddress
        {
            get { return this.UserName; }
        }

        public string DefaultFromDisplayName
        {
            get { return "0124"; }
        }
    }
}