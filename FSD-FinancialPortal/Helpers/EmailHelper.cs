using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace FSD_FinancialPortal.Helpers
{
    public class EmailMessage
    {
        public string CustomSource { get; set; }
        public string SourceName { get; set; }
        public string SourceId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string DestinationEmail { get; set; }

        public string Subject { get; set; }

        [AllowHtml]
        public string Body { get; set; }
    }

    public class EmailHelper
    {
        public static async Task SendInvite(EmailMessage message)
        {
            //Steps for composing the CustomSource 
            message.CustomSource = message.SourceName + WebConfigurationManager.AppSettings["emailsource"];

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

                    await smtp.SendMailAsync(message.CustomSource, message.DestinationEmail, message.Subject, message.Body);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(0);
                }
        }
    }
}