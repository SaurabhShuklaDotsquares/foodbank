using System.ComponentModel;

namespace FB.Dto
{
    public class LoginViewDto
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public bool IsRecaptcha { get; set; }
    }
}
