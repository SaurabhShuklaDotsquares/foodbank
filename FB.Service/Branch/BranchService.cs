using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Branch;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FB.Service
{
    public class BranchService : IBranchService
    {
        private readonly IRepository<Branch> repoBranch;
        private readonly IUserPreferenceService userPreferenceService;
        public BranchService(IRepository<Branch> _repoBranch, IUserPreferenceService _userPreferenceService)
        {
            this.repoBranch = _repoBranch;
            userPreferenceService = _userPreferenceService;
        }

        public Branch GetBranch(int id)
        {
            return repoBranch.Query().Filter(b => b.BranchId == id).Include(b => b.Charity).Include(b => b.Charity.CentralOffice).Get().FirstOrDefault();
        }
        public List<Branch> GetBranchesByDataAccessibility(List<UserDataAccessDto> userDataAccess, int roleID, int charityid, int userID)
        {
            return GetCharitiyBranchesByDataAccessibility(userDataAccess: userDataAccess, roleID: roleID, charityId: charityid, userID: userID);
        }
        public List<Branch> GetCharitiyBranchesByDataAccessibility(List<UserDataAccessDto> userDataAccess, int roleID, int? centralOfficeId = null, int? charityId = null, int? agentId = null, int? userID = null, bool isUserPreference = true)
        {
            //var BranchList = new List<Branch>();
            var predicate = PredicateBuilder.True<Branch>();
            //predicate = predicate.And(b => b.IsMmo);
            //predicate = predicate.And(BranchPredicate.GetDataAccessPredicate(predicate, userDataAccess, roleID));

            //if (isUserPreference)
            //{
            //    predicate = SetBranchActiveUserPreference(predicate, userID);
            //}

            //predicate = predicate.And(e => e.IsMmo);

            if (charityId > 0)
                predicate = predicate.And(e => e.CharityId == charityId);
            else if (centralOfficeId > 0)
                predicate = predicate.And(e => e.Charity.CentralOfficeId == centralOfficeId);

            if (roleID == (int)UserRoles.Agent && agentId > 0)
                predicate = predicate.And(e => e.Charity.AgentId == agentId);

            var orderBy = OrderBranchByUserPreference(userID);
            return repoBranch.Query().Include(x => x.Charity).Include(x => x.Charity.CentralOffice).Filter(predicate).OrderBy(o => o.OrderBy(orderBy)).Get().ToList();
            //BranchList.AddRange(repoBranch.Query().Filter(predicate).OrderBy(o => o.OrderBy(orderBy)).Get().ToList());            
            //BranchList.ForEach(e =>
            //{
            //    e.BranchDescription = string.Format("{0} {1}", e.BranchReference.ISNULL(e.Charity.Prefix).AddBracket(), !string.IsNullOrWhiteSpace(e.BranchReference.ISNULL(e.Charity.Prefix)) ? e.BranchDescription.Replace(e.BranchReference.ISNULL(e.Charity.Prefix).AddBracket(), "") : e.BranchDescription).Trim();
            //});
            //return BranchList;
        }

        public List<Branch> GetBranchesByDataAccessibility(int charityid)
        {
            return GetCharitiyBranchesByDataAccessibility(charityId: charityid);
        }
        public List<Branch> GetBranchesByDataAccessibilityBranchId(int charityid, int branchID)
        {
            return GetCharitiyBranchesByDataAccessibility(charityId: charityid, branchID: branchID);
        }
        public List<Branch> GetCharitiyBranchesByDataAccessibility(int? centralOfficeId = null, int? charityId = null, int? branchID = null)
        {
            var predicate = PredicateBuilder.True<Branch>();

            if (charityId > 0)
                predicate = predicate.And(e => e.CharityId == charityId);
            else if (centralOfficeId > 0)
                predicate = predicate.And(e => e.Charity.CentralOfficeId == centralOfficeId);
             if (branchID > 0)
                predicate = predicate.And(e => e.BranchId == branchID);

            return repoBranch.Query().Filter(predicate).Get().ToList();
            //return BranchList;
        }
        private Expression<Func<Branch, bool>> SetBranchActiveUserPreference(Expression<Func<Branch, bool>> predicate, int? userId)
        {
            if (userId > 0)
            {
                var userPreference = userPreferenceService.GetUserPreferencebyUserId(userId.Value);
                if (userPreference != null && userPreference.ShowInactiveCharityBranch == true)
                    return predicate;
            }
            return predicate.And(e => e.IsActive && e.Charity.IsActive);
        }
        private Expression<Func<Branch, string>> OrderBranchByUserPreference(int? userId)
        {
            if (userId > 0)
            {
                var userPreference = userPreferenceService.GetUserPreferencebyUserId(userId.Value);
                if (userPreference != null)
                {
                    if (userPreference.OrderByCharityBranch == (byte)UserPreferenceOrderByCharityBranch.ByName)
                        return oo => oo.BranchDescription;
                    else if (userPreference.OrderByCharityBranch == (byte)UserPreferenceOrderByCharityBranch.ByRef)
                        return oo => oo.BranchReference;
                }
            }
            return oo => oo.BranchDescription;
        }
        public List<Branch> GetBranches(int? charityID = null, int? centralOfficeId = null, int? userID = null)
        {
            var predicate = PredicateBuilder.True<Branch>();
            predicate = SetBranchActiveUserPreference(predicate, userID);

            if (charityID > 0)
            {
                predicate = predicate.And(p => p.CharityId == charityID);
                var orderBy = OrderBranchByUserPreference(userID);
                return repoBranch.Query().Filter(predicate).Include(c => c.Charity).OrderBy(o => o.OrderBy(orderBy)).Get().ToList();
            }
            else if (centralOfficeId > 0)
            {
                predicate = predicate.And(b => b.Charity.CentralOfficeId == centralOfficeId);
                var orderBy = OrderBranchByUserPreference(userID);
                return repoBranch.Query().Filter(predicate).Include(c => c.Charity).OrderBy(o => o.OrderBy(orderBy)).Get().ToList();
            }
            else
                return new List<Branch>();
        }


        public DonorReferences GetDonorReferenceByBranch(int id)
        {
            DonorReferences donorReference = new DonorReferences();
            var branch = repoBranch.Query().Filter(x => x.BranchId == id).Include(x => x.Person).Get().FirstOrDefault();
            if (branch != null)
            {
                if (branch.ReferenceType == (byte)ReferenceType.Character)
                {
                    var donor = branch.Person.OrderByDescending(x => x.PersonId).FirstOrDefault();
                    donorReference = new DonorReferences { DonorReference = donor != null ? (donor.Reference != null ? donor.Reference : "---") : "---", BranchName = branch.BranchDescription != null ? branch.BranchDescription : "---" };
                }
                else
                {
                    int num;
                    var donorReferences = branch.Person.OrderByDescending(x => x.PersonId).Select(X => X.Reference).Where(t => int.TryParse(t, out num)).OrderByDescending(oo => Convert.ToInt32(oo)).FirstOrDefault();
                    var quickAddReferences = branch.QuickDonorGift.OrderByDescending(x => x.QuickAddId).Select(X => X.Reference).Where(t => int.TryParse(t, out num)).OrderByDescending(oo => Convert.ToInt32(oo)).FirstOrDefault();
                    string donorref = string.Empty;

                    if (!string.IsNullOrEmpty(donorReferences) || !string.IsNullOrEmpty(quickAddReferences))
                    {
                        //get latest reference from both tables (person and QuickDonorGift)                        
                        int donorReferencesNo, quickAddReferencesNo;
                        bool isdonorRefNumeric = int.TryParse(donorReferences, out donorReferencesNo);
                        bool isquickAddRefNumeric = int.TryParse(quickAddReferences, out quickAddReferencesNo);

                        if (isdonorRefNumeric || isquickAddRefNumeric)
                        {
                            string lastMaximumReference = string.Empty;
                            if (isdonorRefNumeric && isquickAddRefNumeric)
                                lastMaximumReference = donorReferencesNo > quickAddReferencesNo ? donorReferences : quickAddReferences;
                            else
                                lastMaximumReference = isdonorRefNumeric ? donorReferences : quickAddReferences;

                            //Below code to mainaint lenght of reference number as per client requirement.
                            var olgRefLength = lastMaximumReference.Length;
                            donorref = (Convert.ToInt32(lastMaximumReference) + 1).ToString();
                            int newReflenght = donorref.Length;
                            for (var i = 0; i < olgRefLength - newReflenght; i++)
                            {
                                donorref = "0" + donorref;
                            }
                        }
                        else
                            donorref = "0001";
                    }
                    else
                        donorref = "0001";

                    donorReference = new DonorReferences { DonorReference = donorref, BranchName = branch.BranchDescription != null ? branch.BranchDescription : "---" };
                }

            }
            return donorReference;
        }

        public string GetDonorReference(int branchId)
        {
            var branch = repoBranch.Query().Filter(x=>x.BranchId==branchId).Include(x=>x.Person).Get().FirstOrDefault();
            string lastReference = string.Empty;

            if (branch != null)
            {
                if (branch.ReferenceType == (byte)ReferenceType.Character)
                {
                    var donor = branch.Person.OrderByDescending(x => x.PersonId).FirstOrDefault();
                    if (donor != null)
                        lastReference = donor.Reference;
                    else
                        lastReference = "----";
                }
                else
                {
                    int num;
                    var donorReferences = branch.Person.OrderByDescending(x => x.PersonId).Select(X => X.Reference).Where(t => int.TryParse(t, out num)).OrderByDescending(oo => Convert.ToInt32(oo)).FirstOrDefault();
                    var quickAddReferences = branch.QuickDonorGift.OrderByDescending(x => x.QuickAddId).Select(X => X.Reference).Where(t => int.TryParse(t, out num)).OrderByDescending(oo => Convert.ToInt32(oo)).FirstOrDefault();

                    if (!string.IsNullOrEmpty(donorReferences) || !string.IsNullOrEmpty(quickAddReferences))
                    {
                        //get latest reference from both tables (person and QuickDonorGift)                       
                        int donorReferencesNo, quickAddReferencesNo;
                        bool isdonorRefNumeric = int.TryParse(donorReferences, out donorReferencesNo);
                        bool isquickAddRefNumeric = int.TryParse(quickAddReferences, out quickAddReferencesNo);

                        if (isdonorRefNumeric || isquickAddRefNumeric)
                        {
                            string lastMaximumReference = string.Empty;
                            if (isdonorRefNumeric && isquickAddRefNumeric)
                                lastMaximumReference = donorReferencesNo > quickAddReferencesNo ? donorReferences : quickAddReferences;
                            else
                                lastMaximumReference = isdonorRefNumeric ? donorReferences : quickAddReferences;

                            //Below code to mainaint lenght of reference number as per client requirement.
                            var olgRefLength = lastMaximumReference.Length;
                            lastReference = (Convert.ToInt32(lastMaximumReference) + 1).ToString();
                            int newReflenght = lastReference.Length;
                            for (var i = 0; i < olgRefLength - newReflenght; i++)
                            {
                                lastReference = "0" + lastReference;
                            }
                        }
                        else
                            lastReference = "0001";
                    }
                    else
                        lastReference = "0001";
                }
            }
            return lastReference;
        }

        public void Dispose()
        {
            if (repoBranch != null)
            {
                repoBranch.Dispose();
            }
        }

    }
}
