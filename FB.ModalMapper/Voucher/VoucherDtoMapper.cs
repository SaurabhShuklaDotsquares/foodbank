using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public class VoucherDtoMapper
    {
        public static List<VoucherDto> VoucherMapper(List<Voucher> vouchers)
        {
            var voucherlist = new List<VoucherDto>();
            foreach (var voucher in vouchers)
            {
                voucherlist.Add(new VoucherDto
                {
                    VoucherId = voucher.Id,
                    ParcelTypeId = voucher.ParcelTypeId,
                    VoucherToken = voucher.VoucherToken,
                    RedeemedDate = voucher.RedeemedDate,
                    ReferrerId = voucher.ReferrerId??0,
                    FamilyId = voucher.FamilyId,
                    ReferrerName = voucher.Referrer==null?"":voucher.Referrer.Name,
                    FamilyName = voucher.Family.FamilyName,
                    AddedDate = voucher.AddedDate,
                });
            }
            return voucherlist;
        }
    }
}
