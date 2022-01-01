using BackEnd.Core.Services.Interfaces;
using System.Net.Mail;

namespace BackEnd.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "info@dr-payamabolhassani.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("mail.dr-payamabolhassani.com");

            mail.From = new MailAddress(defaultEmail, "وبسایت دکتر پیام ابوالحسنی");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 25;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "P@yam4257613");

            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }
    }
}

