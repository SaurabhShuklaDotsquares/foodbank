using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FB.Core;
using System.Text.Json.Serialization;
using FB.Dto;

namespace FB.Web
{
    public class CustomPrincipal : IPrincipal
    {
        public bool IsAuthenticated { get; private set; }
        public int UserID { get; set; }
        public string UserName { get; private set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleID { get; set; }
        public int OrganisationID { get; set; }
        public int? CharityID { get; set; }
        public int? BranchID { get; set; }
        public int? PersonID { get; set; }
        public int? ParentRoleID { get; set; }
        public List<UserDataAccessDto> DataAccessibilities { get; set; }
        public string ScreenImage { get; set; }
        public string MSApiUserId { get; set; }
        public bool IsTeamManager { get; set; }
        public int FoodbankId { get; set; }

        [JsonIgnore]
        public IIdentity Identity { get; private set; }


        private readonly ClaimsPrincipal claimsPrincipal;

        public CustomPrincipal(ClaimsPrincipal principal)
        {
            claimsPrincipal = principal;
            this.IsAuthenticated = claimsPrincipal.Identity.IsAuthenticated;
            if (this.IsAuthenticated)
            {
                this.Identity = new GenericIdentity(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.UserName))?.Value);
                this.UserID = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.PrimarySid)?.Value);
                this.FirstName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.GivenName)?.Value;
                this.LastName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.LastName))?.Value;
                this.UserName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.UserName))?.Value;
                this.Email = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value;
                this.RoleID = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.RoleID))?.Value);
                this.OrganisationID = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.OrganisationID))?.Value);
                this.CharityID = !string.IsNullOrEmpty(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.CharityID))?.Value) ? int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.CharityID))?.Value) : (int ?)null;
                this.BranchID = !string.IsNullOrEmpty(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.BranchID))?.Value) ? int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.BranchID))?.Value) : (int?)null;
                this.DataAccessibilities = Common.GetDataAccessibility(this.RoleID, this.UserID);
                this.MSApiUserId = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.MSApiUserId))?.Value;
                this.FoodbankId = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.FoodbankId))?.Value);
                this.IsTeamManager= bool.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.IsTeamManager))?.Value);
            }
        }

        private void UpdateClaim(string key, string value)
        {
            var claims = claimsPrincipal.Claims.ToList();
            if (claims.Any())
            {
                var pmClaim = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == key);
                if (pmClaim != null)
                {
                    claims.Remove(pmClaim);
                    claims.Add(new Claim(key, value));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };
            ContextProvider.HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties
                 ).Wait();
        }
        public bool IsInRole(int roleID)
        {
            return RoleID == roleID || ParentRoleID == roleID;
        }


        public bool IsInRole(string roleID)
        {
            return RoleID == Convert.ToInt32(roleID) || ParentRoleID == Convert.ToInt32(roleID);
        }

        public bool IsSuperAdminUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.SuperAdmin) || ParentRoleID == Convert.ToInt32((int)UserRoles.SuperAdmin) && OrganisationID <= 0);
        }

        public bool IsInternalUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.Internal) || ParentRoleID == Convert.ToInt32((int)UserRoles.Internal) && OrganisationID <= 0);
        }

        public bool IsOrganisationUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.Organisation) || ParentRoleID == Convert.ToInt32((int)UserRoles.Organisation) && OrganisationID > 0 && !CharityID.HasValue && !BranchID.HasValue);
        }

        public bool IsOnlyOrganisationUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.Organisation) && OrganisationID > 0 && !CharityID.HasValue && !BranchID.HasValue);
        }

        public bool IsCharityUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.Charity) || ParentRoleID == Convert.ToInt32((int)UserRoles.Charity) && OrganisationID > 0 && CharityID > 0 && !BranchID.HasValue);
        }

        public bool IsBranchUser()
        {
            return (RoleID == Convert.ToInt32((int)UserRoles.Branch) || ParentRoleID == Convert.ToInt32((int)UserRoles.Branch) && OrganisationID > 0 && CharityID > 0 && BranchID > 0);
        }

        public bool IsContactLessTerminalAccess()
        {
            return (IsOrganisationUser() || IsSuperAdminUser());
        }

    }
}
