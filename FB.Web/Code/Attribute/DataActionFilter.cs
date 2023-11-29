using FB.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Code
{

    public class DataActionFilter : ActionFilterAttribute
    {
        public string dataId;
        public DataEnityNames tableName;
        public DataActionFilter(string _dataId, DataEnityNames _tableName)
        {
            this.dataId = _dataId;
            this.tableName = _tableName;
        }
        protected virtual CustomPrincipal CurrentUser
        {
            get { return new CustomPrincipal(ContextProvider.HttpContext.User); }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isValid = false;
            if (tableName == DataEnityNames.Menu)
                isValid = CurrentUser.RoleID == (int)UserRoles.SuperAdmin;

            if (!CheckAuthorisedData(filterContext))
            {
                ReturnAccessDenied(filterContext);
            }
        }

        private void ReturnAccessDenied(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDeniedAjax"
                }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "error",
                    action = "accessDenied"
                }));
            }
        }

        private bool CheckAuthorisedData(ActionExecutingContext filterContext)
        {
            return true;
        //    if (CurrentUser.RoleID == (int)UserRoles.SuperAdmin || CurrentUser.RoleID == (int)UserRoles.Internal)
        //        return true;

        //    if (dataId == null)
        //        return true;

        //    var isAuthoried = true;
        //    object dataCheckId = null;

        //    var dataIds = dataId.Split(',');

        //    AuthorisedData authoriseData = new AuthorisedData(
        //(ICentralOfficeService)filterContext.HttpContext.RequestServices.GetService(typeof(ICentralOfficeService)),
        //(ICharityService)filterContext.HttpContext.RequestServices.GetService(typeof(ICharityService)),
        //(IBranchService)filterContext.HttpContext.RequestServices.GetService(typeof(IBranchService)),
        //(IUserService)filterContext.HttpContext.RequestServices.GetService(typeof(IUserService)),
        //(IPersonService)filterContext.HttpContext.RequestServices.GetService(typeof(IPersonService)),
        //(IMembershipTypeService)filterContext.HttpContext.RequestServices.GetService(typeof(IMembershipTypeService)),
        //(IMembershipCodeService)filterContext.HttpContext.RequestServices.GetService(typeof(IMembershipCodeService)),
        //(IContactTypeService)filterContext.HttpContext.RequestServices.GetService(typeof(IContactTypeService)),
        //(IRelationshipTypeService)filterContext.HttpContext.RequestServices.GetService(typeof(IRelationshipTypeService)),
        //(IUserDefinedFieldService)filterContext.HttpContext.RequestServices.GetService(typeof(IUserDefinedFieldService)),
        //(IHouseHoldService)filterContext.HttpContext.RequestServices.GetService(typeof(IHouseHoldService)),
        //(IAddressService)filterContext.HttpContext.RequestServices.GetService(typeof(IAddressService)),
        //(IContactService)filterContext.HttpContext.RequestServices.GetService(typeof(IContactService)),
        //(IRelationshipService)filterContext.HttpContext.RequestServices.GetService(typeof(IRelationshipService)),
        //(ICorrespondenceService)filterContext.HttpContext.RequestServices.GetService(typeof(ICorrespondenceService)),
        //(IGroupTypeService)filterContext.HttpContext.RequestServices.GetService(typeof(IGroupTypeService)),
        //(IGroupService)filterContext.HttpContext.RequestServices.GetService(typeof(IGroupService)),
        //(IGroupMembersService)filterContext.HttpContext.RequestServices.GetService(typeof(IGroupMembersService)),
        //(IGroupEventsService)filterContext.HttpContext.RequestServices.GetService(typeof(IGroupEventsService)),
        //(IGroupEventAttendanceService)filterContext.HttpContext.RequestServices.GetService(typeof(IGroupEventAttendanceService)),
        //(ISkillGroupService)filterContext.HttpContext.RequestServices.GetService(typeof(ISkillGroupService)),
        //(ISkillService)filterContext.HttpContext.RequestServices.GetService(typeof(ISkillService)),
        //(ICertificateService)filterContext.HttpContext.RequestServices.GetService(typeof(ICertificateService)),
        //(ICertificateIssuerService)filterContext.HttpContext.RequestServices.GetService(typeof(ICertificateIssuerService)),
        //(IMemberSkillService)filterContext.HttpContext.RequestServices.GetService(typeof(IMemberSkillService)),
        //(IMemberCertificateService)filterContext.HttpContext.RequestServices.GetService(typeof(IMemberCertificateService)),
        //(ITaskService)filterContext.HttpContext.RequestServices.GetService(typeof(ITaskService)),
        //(ITaskWillService)filterContext.HttpContext.RequestServices.GetService(typeof(ITaskWillService)),
        //(IMemberRuleService)filterContext.HttpContext.RequestServices.GetService(typeof(IMemberRuleService)),
        //(IShiftService)filterContext.HttpContext.RequestServices.GetService(typeof(IShiftService)),
        //(IUnavailabilityService)filterContext.HttpContext.RequestServices.GetService(typeof(IUnavailabilityService)),
        //(IVisitTypeService)filterContext.HttpContext.RequestServices.GetService(typeof(IVisitTypeService)),
        //(IVisitService)filterContext.HttpContext.RequestServices.GetService(typeof(IVisitService)),
        //(IRoleService)filterContext.HttpContext.RequestServices.GetService(typeof(IRoleService)),
        //(IMMOUserDataAccessibilityService)filterContext.HttpContext.RequestServices.GetService(typeof(IMMOUserDataAccessibilityService)),
        //(ILabelService)filterContext.HttpContext.RequestServices.GetService(typeof(ILabelService)),
        //(IAttendanceCodeService)filterContext.HttpContext.RequestServices.GetService(typeof(IAttendanceCodeService)),
        //(IMaritalStatusService)filterContext.HttpContext.RequestServices.GetService(typeof(IMaritalStatusService)),
        //(IDonationService)filterContext.HttpContext.RequestServices.GetService(typeof(IDonationService)),
        //(INoteService)filterContext.HttpContext.RequestServices.GetService(typeof(INoteService)),
        //(IMembershipEnrolmentFormService)filterContext.HttpContext.RequestServices.GetService(typeof(IMembershipEnrolmentFormService)),
        //(IWebsiteButtonService)filterContext.HttpContext.RequestServices.GetService(typeof(IWebsiteButtonService))
        //);

        //    foreach (var id in dataIds)
        //    {
        //        object param;
        //        if (filterContext.ActionArguments.ContainsKey(id))
        //        {
        //            filterContext.ActionArguments.TryGetValue(id, out param);
        //            dataCheckId = param;
        //        }

        //        switch (tableName)
        //        {
        //            case DataEnityNames.CentralOffice:
        //                if (CurrentUser.RoleID == (int)UserRoles.Donor)
        //                    return true;
        //                isAuthoried = authoriseData.CheckAuthorisedCentralOffice(dataCheckId);
        //                break;
        //            case DataEnityNames.Charity:
        //                if (CurrentUser.RoleID == (int)UserRoles.Donor)
        //                    return true;
        //                isAuthoried = authoriseData.CheckAuthorisedCharity(dataCheckId);
        //                break;
        //            case DataEnityNames.Branch:
        //                if (CurrentUser.RoleID == (int)UserRoles.Donor)
        //                    return true;
        //                isAuthoried = authoriseData.CheckAuthorisedBranch(dataCheckId);
        //                break;

        //            case DataEnityNames.Role:
        //                isAuthoried = authoriseData.CheckAuthorisedRole(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMembershipType:
        //                isAuthoried = authoriseData.CheckAuthorisedMembershipType(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMembershipCode:
        //                isAuthoried = authoriseData.CheckAuthorisedMembershipCode(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOContactType:
        //                isAuthoried = authoriseData.CheckAuthorisedContactType(dataCheckId);
        //                break;

        //            case DataEnityNames.MMORelationshipType:
        //                isAuthoried = authoriseData.CheckAuthorisedRelationshipType(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOUserDefinedField:
        //                isAuthoried = authoriseData.CheckAuthorisedUserDefinedField(dataCheckId);
        //                break;

        //            case DataEnityNames.User:
        //                isAuthoried = authoriseData.CheckAuthorisedUser(dataCheckId);
        //                break;

        //            case DataEnityNames.Person:
        //                isAuthoried = authoriseData.CheckAuthorisedPerson(dataCheckId);
        //                break;

        //            case DataEnityNames.HouseHold:
        //                isAuthoried = authoriseData.CheckAuthorisedHouseHold(dataCheckId);
        //                break;

        //            case DataEnityNames.Address:
        //                isAuthoried = authoriseData.CheckAuthorisedAddress(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOContact:
        //                isAuthoried = authoriseData.CheckAuthorisedContact(dataCheckId);
        //                break;

        //            case DataEnityNames.MMORelationshipMember:
        //                isAuthoried = authoriseData.CheckAuthorisedRelationship(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOCorrespondence:
        //                isAuthoried = authoriseData.CheckAuthorisedCorrespondence(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroupType:
        //                isAuthoried = authoriseData.CheckAuthorisedGroupType(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroup:
        //                isAuthoried = authoriseData.CheckAuthorisedGroup(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroup1:
        //                isAuthoried = authoriseData.CheckAuthorisedGroup(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroupMembers:
        //                isAuthoried = authoriseData.CheckAuthorisedGroupMembers(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroupEvent:
        //                isAuthoried = authoriseData.CheckAuthorisedGroupEvent(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOGroupEventAttendance:
        //                isAuthoried = authoriseData.CheckAuthorisedGroupEventAttendance(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOSkillGroup:
        //                isAuthoried = authoriseData.CheckAuthorisedSkillGroup(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOSkill:
        //                isAuthoried = authoriseData.CheckAuthorisedSkill(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOCertificateIssuer:
        //                isAuthoried = authoriseData.CheckAuthorisedCertificateIssuer(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOCertificate:
        //                isAuthoried = authoriseData.CheckAuthorisedCertificate(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMemberSkill:
        //                isAuthoried = authoriseData.CheckAuthorisedMemberSkill(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMemberCertificate:
        //                isAuthoried = authoriseData.CheckAuthorisedMemberCertificate(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMasterTask:
        //                isAuthoried = authoriseData.CheckAuthorisedMasterTask(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOVisitType:
        //                isAuthoried = authoriseData.CheckAuthorisedVisitType(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOAttendanceCode:
        //                isAuthoried = authoriseData.CheckAuthorisedAttendanceCode(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOTaskShift:
        //                isAuthoried = authoriseData.CheckAuthorisedTaskShift(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOReportLabel:
        //                isAuthoried = authoriseData.CheckAuthorisedLabel(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMaritalStatus:
        //                isAuthoried = authoriseData.CheckAuthorisedMaritalStatus(dataCheckId);
        //                break;

        //            case DataEnityNames.Donor:
        //                if (CurrentUser.RoleID == (int)UserRoles.Donor)
        //                    return true;
        //                isAuthoried = authoriseData.CheckAuthorisedDonor(dataCheckId);
        //                break;

        //            case DataEnityNames.MMOMembershipEnrolmentForm:
        //                isAuthoried = authoriseData.CheckAuthorisedMembershipEnrolmentForm(dataCheckId);
        //                break;

        //                //case DataEnityNames.Role:
        //                //    isAuthoried = authoriseData.CheckAuthorisedRole(dataCheckId);
        //                //    break;


        //        }
        //    }
        //    return isAuthoried;
        }
    }
}
