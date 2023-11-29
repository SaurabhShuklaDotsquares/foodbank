using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IVoucherService : IDisposable
    {
        Voucher GetVoucherById(int id);
        KeyValuePair<int, List<VoucherDto>> GetVoucherList(DataTableServerSide searchModel, int foodbankId);
        KeyValuePair<int, List<VoucherDto>> GetVoucherListByFamilyId(DataTableServerSide searchModel, int familyid);
        bool CheckToken(string token);
        bool Save(Voucher voucher);
        List<Voucher> GetVoucherList(int foodbankId);
        List<Voucher> GetVoucherListByFamilyId(int FamilyId);
        void Delete(int id);
    }
}
