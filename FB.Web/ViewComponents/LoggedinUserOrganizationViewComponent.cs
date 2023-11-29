using Microsoft.AspNetCore.Mvc;
using FB.Dto;
using FB.Service;
using System.Threading.Tasks;

namespace FB.Web.ViewComponents
{
    public class LoggedinUserOrganizationViewComponent : ViewComponent
    {
        private readonly ICentralOfficeService centralOfficeService;
        private readonly ICharityService charityService;
        private readonly IBranchService branchService;
        public LoggedinUserOrganizationViewComponent(
        ICentralOfficeService _centralOfficeService,
        ICharityService _charityService,
        IBranchService _branchService
            )
        {
            centralOfficeService = _centralOfficeService;
            charityService = _charityService;
            branchService = _branchService;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            UserLoggedinOrganisationDto usermoel = new UserLoggedinOrganisationDto();
            try
            {               
                string controllerName = HttpContext.Request.RouteValues["controller"].ToString();
                string actionName = HttpContext.Request.RouteValues["action"].ToString();
                if ((controllerName.ToLower() == "dashboard" || controllerName.ToLower() == "home") && (string.IsNullOrWhiteSpace(actionName) || actionName.ToLower() == "index"))
                {
                    return System.Threading.Tasks.Task.FromResult<IViewComponentResult>(View("~/Views/Shared/_LoggedinUser.cshtml", usermoel));
                }
                var user = new CustomPrincipal(HttpContext.User);

                if (user.BranchID > 0)
                {
                    var branch = branchService.GetBranch(user.BranchID.Value);
                    if (branch != null)
                    {
                        usermoel.BranchName = branch.BranchDescription;
                        usermoel.CharityName = branch.Charity.CharityName;
                        usermoel.OrganisationName = branch.Charity.CentralOffice.OrganisationName;
                    }
                }
                else if (user.CharityID > 0)
                {
                    var charity = charityService.GetCharity(user.CharityID.Value);
                    if (charity != null)
                    {
                        usermoel.BranchName = string.Empty;
                        usermoel.CharityName = charity.CharityName;
                        usermoel.OrganisationName = charity.CentralOffice.OrganisationName;
                    }
                }
                else if (user.OrganisationID > 0)
                {
                    var organisation = centralOfficeService.GetCentralOffice(user.OrganisationID);
                    if (organisation != null)
                    {
                        usermoel.BranchName = string.Empty;
                        usermoel.CharityName = string.Empty;
                        usermoel.OrganisationName = organisation.OrganisationName;
                    }
                }
            }
            catch
            {
                return System.Threading.Tasks.Task.FromResult<IViewComponentResult>(View("~/Views/Shared/_LoggedinUser.cshtml", usermoel));
            }
            return System.Threading.Tasks.Task.FromResult<IViewComponentResult>(View("~/Views/Shared/_LoggedinUser.cshtml", usermoel));
        }
    }
}
