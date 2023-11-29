using FB.Data.Models;
using FB.Dto;
using FB.Dto.Branch;
using System;
using System.Collections.Generic;

namespace FB.Service
{
    public interface IBranchService : IDisposable
    {
        Branch GetBranch(int id); 
        List<Branch> GetBranchesByDataAccessibility(List<UserDataAccessDto> userDataAccess, int roleID, int charityid, int userID);
        List<Branch> GetBranchesByDataAccessibility(int charityid);
        List<Branch> GetBranchesByDataAccessibilityBranchId(int charityid, int branchID);
        DonorReferences GetDonorReferenceByBranch(int id);
        string GetDonorReference(int branchId);
        List<Branch> GetBranches(int? charityID = null, int? centralOfficeId = null, int? userID = null);
    }


}
