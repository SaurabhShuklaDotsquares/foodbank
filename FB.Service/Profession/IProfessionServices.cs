using FB.Core;
using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IProfessionServices
    {
        void SaveProfession(Profession student);
        Profession Update(Profession appoinment);
        KeyValuePair<int, List<Profession>> GetProfessionByFoodBankId(DataTableServerSide searchModel, int foodBankId);
        //Profession GetFoodDonationById(int id);
        //void DeleteFoodDonation(int id);
        Profession GetProfessionID(int id);

        //Profession GetFoodbankByUserId(int id);
        public void DeleteFoodDonation(int id);
        public Profession GetProfessionById(int id);
        public List<Profession> GetProfessioList(int foodbankid);
    }
}
