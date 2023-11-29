using System;
using System.Collections.Generic;
using FB.Data;
using FB.Dto;
using FB.Core;
using FB.Dto.UserDataAccessibility;
using FB.Data.Models;

namespace FB.Service
{
    public interface IQuickDonorGiftService : IDisposable
    {
        List<QuickDonorGift> GetQuickDonorGifts();
        bool IsUserNameExist(string userName, int id = 0);
        bool IsBranchReferenceExist(string reference, int branchId, int id = 0);
    }
}
