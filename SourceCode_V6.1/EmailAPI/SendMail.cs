using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;
using Logger;

namespace EmailAPI
{
    public class SendMail
    {
        public class RecepientData
        {
            string recepientEmail;
            string recepientName;
            string subject;
            string messageContent;

            public string MessageContent
            {
                get { return messageContent; }
                set { messageContent = value; }
            }
            public string Subject
            {
                get { return subject; }
                set { subject = value; }
            }
            public string RecepientName
            {
                get { return recepientName; }
                set { recepientName = value; }
            }
            public string RecepientEmail
            {
                get { return recepientEmail; }
                set { recepientEmail = value; }
            }
        }
        public enum MailType
        {
            TextEmail,
            AttachmentEmail,
            HtmlEmail,
            HtmlWithAttachmentEmail
        };
        public bool SendMessage(MailType mailType, RecepientData data, params string[] attachments)
        {
            bool success = false;
            try
            {
                switch (mailType)
                {
                    case MailType.HtmlWithAttachmentEmail:
                    case MailType.HtmlEmail:
                    case MailType.AttachmentEmail:
                        success = SendEmail(data, attachments);
                        break;
                    case MailType.TextEmail: success = SendEmail(data);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
            }
            return success;
        }
        private bool SendEmail(RecepientData data,params string[] attachments)
        {
            string[] recepientEmails = null;
            StringBuilder address = null;
            try
            {
                //Set up SMTP client
                SmtpClient client = new SmtpClient();
                client.Host = Configuration.Hostname;
                client.Port = int.Parse(Configuration.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(Configuration.UserName, Configuration.Password);
                client.EnableSsl = true;
                //Set up the email message
                MailMessage message = new MailMessage();
                recepientEmails = data.RecepientEmail.Split(';');
                address = new StringBuilder();
                foreach(string email in recepientEmails)
                {
                    address.Append(email);
                    address.Append(",");
                }
                address.Remove(address.Length-1,1);
                message.To.Add(address.ToString().Trim());
                message.From = new MailAddress(Configuration.UserName);
                message.Subject = data.Subject;
                message.IsBodyHtml = true; //HTML email

                string htmlBody = FormatEmail.FormatMessageBody(data.RecepientName, data.MessageContent);
                htmlBody = htmlBody.Replace("[email]", "["+data.RecepientEmail+"]");
                htmlBody = htmlBody.Replace("[CLIENTS.COMPANY_NAME]", "[SOURCE CODE]");
                htmlBody = htmlBody.Replace("[CLIENTS.ADDRESS]", "");
                htmlBody = htmlBody.Replace("[CLIENTS.PHONE]", "+91-8105708706");
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString
                    (htmlBody, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Text.Html);

                // Create a LinkedResource object for each embedded image
                string cwd = System.IO.Directory.GetCurrentDirectory();
                
                if (cwd.EndsWith("\\bin\\Debug"))
                {
                    cwd = cwd.Replace("\\bin\\Debug", "");
                }
                LinkedResource logo = new LinkedResource(cwd + "\\images\\logo.png");
                logo.ContentId = "logo";
                LinkedResource id1 = new LinkedResource(cwd + "\\images\\6@2x.png");
                id1.ContentId = "id1";
                LinkedResource pics = new LinkedResource(cwd + "\\images\\pics.png");
                pics.ContentId = "pics";
                LinkedResource yes = new LinkedResource(cwd + "\\images\\yes.gif");
                yes.ContentId = "yes";
                LinkedResource no = new LinkedResource(cwd + "\\images\\no.gif");
                no.ContentId = "no";
                LinkedResource facebook = new LinkedResource(cwd + "\\images\\facebook.png");
                facebook.ContentId = "facebook";
                LinkedResource twitter = new LinkedResource(cwd + "\\images\\twitter.png");
                twitter.ContentId = "twitter";
                LinkedResource pinterest = new LinkedResource(cwd + "\\images\\pinterest.png");
                pinterest.ContentId = "pinterest";
                
                avHtml.LinkedResources.Add(logo);
                avHtml.LinkedResources.Add(id1);
                avHtml.LinkedResources.Add(pics);
                avHtml.LinkedResources.Add(yes);
                avHtml.LinkedResources.Add(no);
                avHtml.LinkedResources.Add(facebook);
                avHtml.LinkedResources.Add(twitter);
                avHtml.LinkedResources.Add(pinterest);
                //message.Body = "";
                message.AlternateViews.Add(avHtml);
                //For Attachment
                if (attachments.Length > 0)
                {
                    foreach (string file in attachments)
                    {
                        if (System.IO.File.Exists(file))
                            message.Attachments.Add(new Attachment(file));
                    }
                }
                //Attempt to send the email
                try
                {
                    client.SendAsync(message, message.Subject);
                }
                catch (Exception ex)
                {
                    ErrorLog.ErrorRoutine(ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
                return false;
            }
            return true;
        }
        private bool SendEmail(RecepientData data)
        {
            try
            {
                //Set up SMTP client
                SmtpClient client = new SmtpClient();
                client.Host = Configuration.Hostname;
                client.Port = int.Parse(Configuration.Port);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential(Configuration.UserName, Configuration.Password);
                client.EnableSsl = true;
                //Set up the email message
                MailMessage message = new MailMessage();
                message.To.Add(data.RecepientEmail);
                message.From = new MailAddress(Configuration.UserName);
                message.Subject = data.Subject;
                message.IsBodyHtml = false;
                message.Body = data.MessageContent;
                //Attempt to send the email
                try
                {
                    client.SendAsync(message, message.Subject);
                }
                catch (Exception ex)
                {
                    ErrorLog.ErrorRoutine(ex);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.ErrorRoutine(ex);
                return false;
            }
            return true;
        }
    }
}
