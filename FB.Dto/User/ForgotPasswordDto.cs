using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class ForgotPasswordDto
    {
        public int UserID { get; set; }
        public string Guid { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
    }
}
