using System;
using System.Collections.Generic;
using System.Linq;
using FB.Repo;
using FB.Data;
using FB.Dto;
using FB.Core;
using FB.Dto.UserDataAccessibility;
using FB.Data.Models;

namespace FB.Service
{
    public class QuickDonorGiftService : IQuickDonorGiftService
    {
        private IRepository<QuickDonorGift> repoQuickDonorGift;

        public QuickDonorGiftService(IRepository<QuickDonorGift> _repoQuickDonorGift)
        {
            this.repoQuickDonorGift = _repoQuickDonorGift;
        }

        /// <summary>
        /// To get list of all quick donor gifts
        /// </summary>
        /// <returns>List of quick donor gift</returns>
        public List<QuickDonorGift> GetQuickDonorGifts()
        {
            return repoQuickDonorGift.Query().Get().ToList();
        }
        /// <summary>
        /// To check username exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string userName, int id = 0)
        {
            return repoQuickDonorGift.Query().Filter(u => u.UserName == userName && u.QuickAddId != id).Get().Count() > 0 ? true : false;
        }

        /// <summary>
        /// To check reference exist
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsBranchReferenceExist(string reference, int branchId, int id = 0)
        {
            return repoQuickDonorGift.Query().Filter(u => u.Reference == reference && u.BranchId == branchId && u.QuickAddId != id).Get().Count() > 0 ? true : false;
        }
        public void Dispose()
        {
            if (repoQuickDonorGift != null)
            {
                repoQuickDonorGift.Dispose();
                repoQuickDonorGift = null;
            }
        }
    }
}
