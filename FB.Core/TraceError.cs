using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FB.Core
{
    public class TraceError
    {
        public static void ReportError(System.Exception exception, string ReportToEmail, string ApplicationTitle = "", String Message = "", String Filepath = "")
        {
            try
            {
                string[] attachFile = new string[] { };
                string subject;
                string mailBody;                             
                if ((exception != null))
                {
                    string ErrorDesc = "<H3>Error Occured</h3><hr>";
                    ErrorDesc += "<b>Date Time:</b><br>" + DateTime.Now.ToString() + "<br><br>";
                    ErrorDesc += "<b>Error Code: </b><br>" + exception.GetHashCode().ToString() + "<br><br>";
                    ErrorDesc += "<b>Base Exception: </b><br>" + exception.GetBaseException().ToString() + "<br><br>";
                    ErrorDesc += "<b>Type: </b><br>" + exception.GetType().ToString() + "<br><br>";
                    ErrorDesc += "<b>Inner Exception: </b><br>" + exception.InnerException + "<br><br>";
                    ErrorDesc += "<b>Message: </b><br>" + exception.Message + "<br><br>";
                    ErrorDesc += "<b>Source: </b><br>" + exception.Source + "<br><br>";
                    ErrorDesc += "<b>Stack Trace: </b><br>" + exception.StackTrace.ToString() + "<br><br>";
                    ErrorDesc += "<b>Generic Info: </b><br>" + exception.ToString() + "<br><br><hr>";
                    ErrorDesc = String.Join("<hr>", ErrorDesc, Convert.ToString(exception));

                    subject = "Exception in application - " + ApplicationTitle;                   
                    mailBody = ErrorDesc;
                }
                else
                {
                    subject = ApplicationTitle;
                    mailBody = "<H3>Message</h3><hr>" + Message;
                    if (!String.IsNullOrEmpty(Filepath))
                    {
                        attachFile = new string[] { Filepath };
                    }
                }

                FlexiMail objSendMail = new FlexiMail
                {
                    SMTPServer = ConfigurationManager.AppSettings["SMTPServer"],
                    SMTPUserName = ConfigurationManager.AppSettings["SMTPUserName"],
                    SMTPUserPassword = ConfigurationManager.AppSettings["SMTPUserPassword"],
                    From = ConfigurationManager.AppSettings["EmailFrom"],
                    Subject = subject,
                    MailBody = mailBody,
                    AttachFile = attachFile,
                    MailBodyManualSupply = true,
                    To = ReportToEmail.IsValidEmail() ? ReportToEmail : (ConfigurationManager.AppSettings["EmailTo"].IsValidEmail() ? ConfigurationManager.AppSettings["EmailTo"] : string.Empty),
                    CC = ConfigurationManager.AppSettings["EmailCC"].IsValidEmail() ? ConfigurationManager.AppSettings["EmailCC"] : string.Empty,
                    BCC = ConfigurationManager.AppSettings["EmailBCC"].IsValidEmail() ? ConfigurationManager.AppSettings["EmailBCC"] : string.Empty
                };
                objSendMail.Send();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
