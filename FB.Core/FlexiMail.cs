using System;
using System.IO;
using System.Net.Mail;

namespace FB.Core
{
    public class FlexiMail
    {
        #region Constructors-Destructors
        public FlexiMail()
        {
            myEmail = new System.Net.Mail.MailMessage();
            _MailBodyManualSupply = false;
        }
        #endregion

        #region  Class Data
        private string _From;
        private string _FromName;
        private string _To;
        private string _ToList;
        private string _Subject;
        private string _CC;
        private string _CCList;
        private string _BCC;
        public string _ReplyTo;
        private string _TemplateDoc;
        private string[] _ArrValues;
        private string _BCCList;
        private bool _MailBodyManualSupply;
        private string _MailBody;
        private string[] _Attachment;
        private System.Net.Mail.MailMessage myEmail;

        private string _SMTPServer;
        private int? _SMTPPort;
        private string _SMTPUserName;
        private string _SMTPUserPassword;
        private bool? _SSLEnable;

        private string _WebRootPath;
        private string _DomainName;

        #endregion

        #region Properties
        public string From
        {
            set { _From = value; }
        }
        public string FromName
        {
            set { _FromName = value; }
        }
        public string To
        {
            set { _To = value; }
        }
        public string Subject
        {
            set { _Subject = value; }
        }
        public string CC
        {
            set { _CC = value; }
        }
        public string BCC
        {

            set { _BCC = value; }
        }
        public bool MailBodyManualSupply
        {

            set { _MailBodyManualSupply = value; }
        }
        public string MailBody
        {
            set { _MailBody = value; }
        }
        public string EmailTemplateFileName
        {
            //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
            set { _TemplateDoc = value; }
        }
        public string[] ValueArray
        {
            //ARRAY OF VALUES TO REPLACE VARS IN TEMPLATE 
            set { _ArrValues = value; }
        }
        public string[] AttachFile
        {
            set { _Attachment = value; }
        }

        public string SMTPServer
        {
            set { _SMTPServer = value; }
        }

        public bool? SSLEnable
        {
            set { _SSLEnable = value; }
        }

        public int? SMTPPort
        {
            set { _SMTPPort = value; }
        }

        public string SMTPUserName
        {
            set { _SMTPUserName = value; }
        }

        public string SMTPUserPassword
        {
            set { _SMTPUserPassword = value; }
        }

        public string WebRootPath
        {
            set { _WebRootPath = value; }
        }

        public string DomainName
        {
            set { _DomainName = value; }
        }

        #endregion

        #region SEND EMAIL

        public void Send()
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, _FromName);
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (_CC.IsNotNullAndNotEmpty())
            {
                _CCList = _CC.Replace(";", ",");
            }
            
            if (_CCList.IsNotNullAndNotEmpty())
            {
                string[] arr = _CCList.Split(',');
                myEmail.CC.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.CC.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.CC.Add(new MailAddress(_CCList));
                }
            }

            //---Set recipients in BCC List 
            if (_BCC.IsNotNullAndNotEmpty())
            {
                _BCCList = _BCC.Replace(";", ",");
            }
          
            if (_BCCList.IsNotNullAndNotEmpty())
            {
                string[] arr = _BCCList.Split(',');
                myEmail.Bcc.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.Bcc.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.Bcc.Add(new MailAddress(_BCCList));
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
            }

            // set attachment 
            if (_Attachment != null)
            {
                for (int i = 0; i < _Attachment.Length; i++)
                {
                    if (_Attachment[i] != null)
                    {
                        var attachment = new Attachment(_Attachment[i]);
                        myEmail.Attachments.Add(attachment);
                    }
                }

            }

            //Send mail 
            SmtpClient client = new SmtpClient();
            if (_SMTPPort.HasValue)
            {
                client.Port = _SMTPPort.Value;
            }
            else
            {
                client.Port = 587;
            }

            client.Host = !string.IsNullOrWhiteSpace(_SMTPServer) ? _SMTPServer : SiteKeys.SMTPServer;
            client.Credentials = new System.Net.NetworkCredential(!string.IsNullOrWhiteSpace(_SMTPUserName) ? _SMTPUserName : SiteKeys.SMTPUserName, !string.IsNullOrWhiteSpace(_SMTPUserPassword) ? _SMTPUserPassword : SiteKeys.SMTPUserPassword);

            if (_SSLEnable == true)
                client.EnableSsl = true;

            client.Send(myEmail);
            myEmail.Attachments.Dispose();
        }

        #endregion

        #region GetHtml

        public string GetHtml(string argTemplateDocument)
        {
            int i;
            StreamReader filePtr;
            var templatePath = !string.IsNullOrWhiteSpace(_WebRootPath) ? $"{_WebRootPath}/EmailTemplates/" : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmailTemplates/");
            //var templatePath = !string.IsNullOrWhiteSpace(_WebRootPath) ? $"{_WebRootPath}/EmailTemplates/" : "C:/Websites/mymembershipmanager.online/MMOWeb/wwwroot/EmailTemplates/";
            filePtr = File.OpenText(templatePath + argTemplateDocument);
            string fileData = filePtr.ReadToEnd();

            if ((_ArrValues == null))
            {
                fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
                return fileData;
            }
            else
            {
                for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
                {
                    fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
                }
                fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
                fileData = fileData.Replace("@headerlogo1@", !string.IsNullOrWhiteSpace(_DomainName) ? _DomainName + "Content/images/device_logo.png" : SiteKeys.DomainName + "Content/images/device_logo.png");
                fileData = fileData.Replace("@headerlogo2@", !string.IsNullOrWhiteSpace(_DomainName) ? _DomainName + "Content/images/Food-Bank-Logo_New.png" : SiteKeys.DomainName + "Content/images/Food-Bank-Logo_New.png");
                return fileData;
            }
        }

        #endregion

    }
}
