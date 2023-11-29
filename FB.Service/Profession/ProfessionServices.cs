using FB.Core;
using FB.Data.Models;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class ProfessionServices : IProfessionServices
    {
        private readonly IRepository<Profession> _repoprofession;



        public ProfessionServices(IRepository<Profession> _repoprofession)
        {
            this._repoprofession = _repoprofession;
        }

        //public bool SaveFoodDonation(FoodItem foodItem)
        //{
        //    try
        //    {
        //        if (foodItem.Id > 0)
        //        {
        //            repoFoodItem.Update(foodItem);
        //            return true;
        //        }
        //        else
        //        {
        //            repoFoodItem.Insert(foodItem);
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        public void DeleteFoodDonation(int id)
        {
            _repoprofession.Delete(id);
        }

        public void SaveProfession(Profession profession)
        {
            profession.AddedDate = DateTime.UtcNow;
            profession.ModifiedDate = DateTime.UtcNow;
            _repoprofession.Insert(profession);

        }

        //public bool SaveProfession(Profession personfoodbank)
        //{
        //    try
        //    {
        //        _repoprofession.Insert(personfoodbank);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public Profession Update(Profession profession)
        {
            profession.ModifiedDate = DateTime.UtcNow;
            _repoprofession.Update(profession);
            return profession;
        }
        public Profession GetProfessionID(int id)
        {
            return _repoprofession.FindById(id);
        }

        public KeyValuePair<int, List<Profession>> GetProfessionByFoodBankId(DataTableServerSide searchModel, int foodBankId)
        {
            var predicate = PredicateBuilder.True<Profession>();
            predicate = CustomPredicate.BuildPredicate<Profession>(searchModel, new Type[] { typeof(Profession) });
            predicate = predicate.And(m => m.FoodbankId == foodBankId);

            int totalCount;
            int page = searchModel.start == 0 ? 1 : (Convert.ToInt32(Decimal.Floor(Convert.ToDecimal(searchModel.start) / searchModel.length)) + 1);

            var ProfessionList = _repoprofession
            .Query()
            .Filter(predicate)
            .OrderBy(x => x.OrderByDescending(oo => oo.ProfessionId)) //for the sorting 
            .CustomOrderBy(u => u.OrderBy(searchModel, new Type[] { typeof(Profession) }))
            .GetPage(page, searchModel.length, out totalCount).ToList(); // for the pagination

            KeyValuePair<int, List<Profession>> resultResponse = new KeyValuePair<int, List<Profession>>(totalCount, ProfessionList);

            return resultResponse;

        }
        public Profession GetProfessionById(int id)
        {

            return _repoprofession.Query().Filter(x => x.ProfessionId == id).Get().FirstOrDefault();
        }

        public List<Profession> GetProfessioList(int Professionid)
        {
            return _repoprofession.Query().Filter(x => x.ProfessionId == Professionid).Get().ToList();

        }

    }
}
