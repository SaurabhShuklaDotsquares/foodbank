using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class UserSessionDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public int? OrganisationID { get; set; }
        public int? CharityID { get; set; }
        public int? BranchID { get; set; }
        public string MSApiUserId { get; set; }
        public int FoodbankId { get; set; }
        public bool IsTeamManager { get; set; }
    }
}
