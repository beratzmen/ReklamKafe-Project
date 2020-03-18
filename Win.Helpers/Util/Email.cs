using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Win.Helpers.Util
{
    public class Email
    {
        public static void SendMail(string from, string fromPassword, string to, string subject, string body)
        {
            try
            {
                if (!IsValidEmail(from))
                    return;
                if (!IsValidEmail(to))
                    return;
                var fromAddress = new MailAddress(from);
                var toAddress = new MailAddress(to);
                using (var smtp = new SmtpClient
                {
                    Host = "srvm04.turhost.com",
                    Port = 587,
                    EnableSsl = true,//true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                    {
                        smtp.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
                    return false;
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
