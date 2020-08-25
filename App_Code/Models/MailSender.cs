using System;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
namespace NCC.Models
{
    public class MailSender
    {
        public bool SendMail(string to, string html, string subject, string MailSenderDisplayName, string mailSenderUserName, string mailSenderPass, string mailHost, int portNumber, bool enableSsl)
        {
            bool retVal;
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                msg.From = new MailAddress(mailSenderUserName, MailSenderDisplayName);
                msg.To.Add(to);
                msg.Subject = subject;
                msg.Body = html;

                msg.IsBodyHtml = true;
                smtpClient.Host = mailHost;
                smtpClient.Credentials = new System.Net.NetworkCredential(mailSenderUserName, mailSenderPass);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.Port = portNumber;

                smtpClient.Send(msg);
                retVal = true;

            }
            catch (Exception ee)
            {
                throw ee;
            }

            return retVal;
        }
        public bool sendMailWithAttachment(string to, string html, string subject, string fileName, System.IO.Stream stream, string MailSenderDisplayName, string mailSenderUserName, string mailSenderPass, string mailHost, int portNumber, bool enableSsl)
        {
            bool retVal;
            try
            {
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                msg.From = new System.Net.Mail.MailAddress(mailSenderUserName, MailSenderDisplayName);
                msg.To.Add(to);
                msg.Subject = subject;
                msg.Body = html;
                msg.Attachments.Add(new Attachment(stream, fileName));
                msg.IsBodyHtml = true;
                smtpClient.Host = mailHost;
                smtpClient.Credentials = new System.Net.NetworkCredential(mailSenderUserName, mailSenderPass);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.Port = portNumber;

                smtpClient.Send(msg);
                retVal = true;

            }
            catch (Exception ee)
            {
                throw ee;
            }

            return retVal;
        }


    }
}