using Sporty.Helper;
using System.Net;
using System.Net.Mail;

namespace App.PR.Helper
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var Clint = new SmtpClient("smtp.gmail.com", 587);
                Clint.EnableSsl=true;
                Clint.Credentials = new NetworkCredential("mohamed20ali003@gmail.com", "qhnkuolvwknvpasz");
                Clint.Send("mohamed20ali003@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (Exception ex) 
            { return false; }
            
        }
    }
}
