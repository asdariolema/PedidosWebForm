using System;
using System.Web.Mail;
//using Chr.data.Log;
using System.Configuration;


namespace Chr.General.mail
{
	/// <summary>
	/// Envío de emails utilizando un servidor SMTP.
	/// </summary>
	public class ChrEmail
	{
			
		static string wEmailFrom = ConfigurationManager.AppSettings["emailFrom"].ToString();
		static string wEmailAdmin = ConfigurationManager.AppSettings["emailAdmin"].ToString();
	
		public ChrEmail()
		{		
		}

		/// <summary>
		/// Envía un email utilizando un servidor SMTP. 
		/// El nombre o IP del servidor debe estar configurado en el archivo de configuración, bajo la clave "SmtpServer"
		/// </summary>
		/// <param name="pTo">Email del destinatario</param>
		/// <param name="pSubject">Asunto del mensaje</param>
		/// <param name="pBody">Cuerpo del mensaje.</param>
		/// <param name="pHTMLformat">Formato con que se envía el email.
		/// Si es true, el email se enviará con formato HTML</param>
		/// <param name="pPrioridadAlta">Indicar si el email tiene prioridad alta</param>
		public static void sendMail(string pTo, string pSubject, string pBody, bool pHTMLformat, bool pPrioridadAlta)
		{
			if(wEmailAdmin.Length > 0)
			{
				try
				{
					MailMessage eMail = new MailMessage();
                    eMail.From = wEmailFrom;
					eMail.To = pTo;
					//				eMail.Bcc = wEmailBcc;
					eMail.Subject = pSubject;
			
					if(pHTMLformat)
					{
						eMail.BodyFormat = MailFormat.Html;
						pBody = string.Format("<html><body>{0}</body></html>", pBody);
					}
					else
					{
						eMail.BodyFormat = MailFormat.Text;
					}

					if(pPrioridadAlta)
						eMail.Priority = MailPriority.High;
					else
						eMail.Priority = MailPriority.Normal;


					eMail.Body = pBody;
			
					SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SmtpServer"].ToString();
					SmtpMail.Send(eMail);
				}
				catch(Exception e)
				{
					//ChrEventLog.LogEvent(e, ChrEventLog.LogEventType.Error);
					throw new Exception(string.Format(@"No se puede enviar el mail de log de eventos.
										[{0}] Intente remover la clave 'emailAdmin' de la configuración.", e.Message));
				}
			}
		}
	}
}
