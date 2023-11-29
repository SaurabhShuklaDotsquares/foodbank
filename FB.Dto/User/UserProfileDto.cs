using System.ComponentModel;

namespace FB.Dto
{
   public class UserProfileDto
    {
        public int UserID { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Primary Mobile")]
        public string PrimaryMobile { get; set; }

        [DisplayName("Alternate Mobile")]
        public string AlternateMobile { get; set; }

        public string Landline { get; set; }

        public string Address { get; set; }

        [DisplayName("City")]
        public string CityName { get; set; }

        public string Email { get; set; }

        public bool IsPasswordChange { get; set; }

        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        [DisplayName("New Password")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }

        [DisplayName("Include Inactive Records")]
        public bool ShowInactiveRecords { get; set; }

        [DisplayName("Autosave Changes to Member Records")]
        public bool AutosaveDonor { get; set; }
    }
}
