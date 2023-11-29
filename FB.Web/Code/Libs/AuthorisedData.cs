using Microsoft.AspNetCore.Http;
using FB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class AuthorisedData
    {

        private readonly ICentralOfficeService centralOfficeService;
        private readonly ICharityService charityService;
        private readonly IBranchService branchService;
        private readonly IUserService userService;
        private readonly IAddressService addressService;
        private readonly IContactService contactService;
        
        private readonly IRoleService roleService;
        private readonly INoteService noteService;
        public AuthorisedData(
        ICentralOfficeService _centralOfficeService,
        ICharityService _charityService,
        IBranchService _branchService
        )
        {
            this.centralOfficeService = _centralOfficeService;
            this.charityService = _charityService;
            this.branchService = _branchService;
        }

        protected CustomPrincipal CurrentUser => new CustomPrincipal(ContextProvider.HttpContext.User);

        public bool CheckAuthorisedUser(object id)
        {
            var dataId = Convert.ToInt32(id);
            if (dataId == 0)
                return true;
            //return userService.IsAccessibleUser(dataId, CurrentUser.UserID, CurrentUser.RoleID, CurrentUser.BranchID, CurrentUser.CharityID, CurrentUser.OrganisationID);
            return true;
        }

        public bool CheckAuthorisedCentralOffice(object id)
        {
            var dataId = Convert.ToInt32(id);
            if (dataId == 0)
                return true;
            return CurrentUser.OrganisationID == dataId;
        }

        public bool CheckAuthorisedCharity(object id)
        {
            var dataId = Convert.ToInt32(id);
            if (dataId == 0)
                return true;

            bool IsAccessibilityCheck = (CurrentUser.BranchID.HasValue || CurrentUser.PersonID.HasValue ) ? false : true;
            return charityService.IsAccessibleCharity(id, IsAccessibilityCheck, CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.UserID);
        }

        public bool CheckAuthorisedBranch(object id)
        {
            if (id == null)
                return true;

            bool IsAccessibilityCheck = (CurrentUser.BranchID.HasValue || CurrentUser.PersonID.HasValue ) ? false : true;
            //return branchService.IsAccessibleBranch(id, IsAccessibilityCheck, CurrentUser.DataAccessibilities, CurrentUser.RoleID, CurrentUser.OrganisationID, CurrentUser.CharityID);
            return true;
        }

        public bool CheckAuthorisedRole(object id)
        {
            var dataId = Convert.ToInt32(id);
            if (dataId == 0)
                return true;
            //return roleService.IsAccessibleRole(id, CurrentUser.OrganisationID);
            return true;
        }

        

        public bool CheckAuthorisedAddress(object id)
        {
            var dataId = Convert.ToInt32(id);
            if (dataId == 0)
                return true;
            return addressService.IsAccessibleAddress(id, CurrentUser.OrganisationID, CurrentUser.CharityID, CurrentUser.BranchID);
        }

    }
}
