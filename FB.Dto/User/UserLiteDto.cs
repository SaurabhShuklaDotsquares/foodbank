using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
   public class UserLiteDto
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
    }
}
