using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FB.Service
{
    public class VoucherService : IVoucherService
    {
        private IRepository<Voucher> repoVoucher;

        public VoucherService(IRepository<Voucher> _repoVoucher)
        {
            repoVoucher = _repoVoucher;
        }
        public KeyValuePair<int, List<VoucherDto>> GetVoucherList(DataTableServerSide searchModel, int foodbankId)
        {
            var predicate = PredicateBuilder.True<Voucher>();
            predicate = CustomPredicate.BuildPredicate<Voucher>(searchModel, new Type[] { typeof(Voucher), typeof(Family), typeof(Referrers) });

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var voucherList = repoVoucher
            .Query()
            .Include(x => x.Family)
            .Include(x => x.Referrer)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Voucher), typeof(Family), typeof(Referrers) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VoucherDtoMapper.VoucherMapper(voucherList);

            KeyValuePair<int, List<VoucherDto>> resultResponse = new KeyValuePair<int, List<VoucherDto>>(totalCount, results);

            return resultResponse;
        }
        public KeyValuePair<int, List<VoucherDto>> GetVoucherListByFamilyId(DataTableServerSide searchModel, int familyid)
        {
            var predicate = PredicateBuilder.True<Voucher>();
            predicate = CustomPredicate.BuildPredicate<Voucher>(searchModel, new Type[] { typeof(Voucher), typeof(Family), typeof(Referrers) });
            predicate = predicate.And(x => x.FamilyId == familyid);
            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var voucherList = repoVoucher
            .Query()
            .Include(x => x.Family)
            .Include(x => x.Referrer)
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.Id))
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Voucher), typeof(Family), typeof(Referrers) }))
            .GetPage(page, searchModel.length, out totalCount).ToList();

            var results = VoucherDtoMapper.VoucherMapper(voucherList);

            KeyValuePair<int, List<VoucherDto>> resultResponse = new KeyValuePair<int, List<VoucherDto>>(totalCount, results);

            return resultResponse;
        }

        public Voucher GetVoucherById(int id)
        {
            return repoVoucher.Query().Filter(x => x.Id == id).Get().FirstOrDefault();
        }

        public List<Voucher> GetVoucherList(int foodbankId)
        {
            return repoVoucher.Query().Filter(x => x.FoodbankId == foodbankId && x.RedeemedDate == null).Get().ToList();
        }
        public List<Voucher> GetVoucherListByFamilyId(int FamilyId)
        {
            return repoVoucher.Query().Filter(x => x.FamilyId == FamilyId ).Get().ToList();
        }
        public bool CheckToken(string token)
        {
            var result = repoVoucher.Query().Filter(x => x.VoucherToken == token).Get().FirstOrDefault();
            if (result != null)
                return true;
            else
                return false;
        }

        public bool Save(Voucher voucher)
        {
            try
            {
                if (voucher.Id > 0)
                {
                    repoVoucher.Update(voucher);
                }
                else
                {
                    repoVoucher.Insert(voucher);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            repoVoucher.Delete(id);
        }


        public void Dispose()
        {
            if (repoVoucher != null)
            {
                repoVoucher.Dispose();
                repoVoucher = null;
            }
        }
    }
}
