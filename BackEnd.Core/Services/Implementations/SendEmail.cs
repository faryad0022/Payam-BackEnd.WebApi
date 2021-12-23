using BackEnd.Core.Services.Interfaces;
using System.Net.Mail;

namespace BackEnd.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "dr.p.abolhassani@gmail.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(defaultEmail, "وبسایت دکتر پیام ابوالحسنی");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "Olomfonon1379");

            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}

