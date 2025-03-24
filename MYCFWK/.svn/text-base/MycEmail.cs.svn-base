using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Configuration;
//using System.Web.Mail;




namespace Myc.General.mail
{
    public class MycEmail
    {
        public static string mailStatus = String.Empty;
        public static event EventHandler NotifyCaller;
    
        protected static void OnNotifyCaller()
        {
            if (NotifyCaller != null) NotifyCaller(mailStatus, EventArgs.Empty);

        }

        public static bool SendMailMessage(string toAddress, string toName, string msgSubject, string msgBody)
        {
            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"]);
                MailAddress from = new MailAddress(ConfigurationManager.AppSettings["smtpFromAddres"], ConfigurationManager.AppSettings["smtpFromName"]);
                MailAddress to = new MailAddress(toAddress, toName);
                MailMessage message = new MailMessage(from, to);
                message.Subject = msgSubject;
                message.Body = msgBody;
               
                if (ConfigurationManager.AppSettings["smtpAutentification"].ToLower() == "true")
                {
                    string user = ConfigurationManager.AppSettings["smtpUser"];
                    string pass = ConfigurationManager.AppSettings["smtpPassword"];
                    client.Credentials = new System.Net.NetworkCredential(user, pass);
                  
                    client.UseDefaultCredentials = false;
                    client.Timeout = 10000;
                }
                else
                {
                    client.UseDefaultCredentials = true;
                }
                client.Send(message);
  
            }
            catch (System.Net.Mail.SmtpException)
            {
                 throw;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }


        public static void SendMailMessageAsync(string SMTPServer, MailMessage message)
        {
            try
            {
                SmtpClient client = new SmtpClient(SMTPServer);
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                client.SendAsync(message, message.To.ToString());
            }
            catch (System.Net.Mail.SmtpException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool SendMailMessage(string SMTPServer, MailMessage message)
        {
            try
            {
                SmtpClient client = new SmtpClient(SMTPServer);
                client.Send(message);
            }
            catch (System.Net.Mail.SmtpException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }


        public static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;
            if (e.Cancelled)
            {
                mailStatus = token + " Send canceled.";
            }
            if (e.Error != null)
            {
                mailStatus = "Error on " + token + ": " + e.Error.ToString();
            }
            else
            {
                mailStatus = token + " mail sent.";
            }
            OnNotifyCaller();
        }
    }
}

