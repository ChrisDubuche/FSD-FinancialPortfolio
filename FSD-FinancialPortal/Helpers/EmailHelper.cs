using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace FSD_FinancialPortal.Helpers
{
    public class EmailHelper
    {
        public static async Task SendInvite(string email, string callbackUrl)
        {
            // Plug in your email service here to send an email.
            var OutlookUsername = WebConfigurationManager.AppSettings["username"];
            var OutlookPassword = WebConfigurationManager.AppSettings["password"];
            var host = WebConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["port"]);

            using (var smtp = new SmtpClient()
            {
                Host = host,
                Port = port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(OutlookUsername, OutlookPassword)
            })

                try
                {
                    var emailFrom = WebConfigurationManager.AppSettings["emailfrom"];

                    await smtp.SendMailAsync(emailFrom, email, "You have been invited to a Household", "You have been invited to join a Household, please accept by clicking <a href=\"" + callbackUrl + "\">here</a>");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
        }
    }
}