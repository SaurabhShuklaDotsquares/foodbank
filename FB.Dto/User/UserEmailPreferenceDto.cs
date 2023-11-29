using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class UserEmailPreferenceDto
    {
        public int UserID { get; set; }
        [DisplayName("Email Format")]
        public byte? EmailFormat { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("SMTP Server")]
        public string SMTPServer { get; set; }

        [DisplayName("ISP User Account Name")]
        public string ISPAccountName { get; set; }

        [DisplayName("ISP User Account Password")]
        public string ISPAccountPassword { get; set; }

        [DisplayName("ISP User Account Confirm Password")]
        public string ISPAccountConfirmPassword { get; set; }

        [DisplayName("SMTP Port")]
        public int? SMTPPort { get; set; }

        [DisplayName("Authentication Type")]
        public byte? AuthenticationType { get; set; }

        [DisplayName("Use SSL Encryption")]
        public bool IsSSLEnccyption { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string ButtonType { get; set; }

    }
}
