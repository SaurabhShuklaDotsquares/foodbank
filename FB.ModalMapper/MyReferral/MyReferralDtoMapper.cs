using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.ModalMapper
{
    public static class MyReferralDtoMapper
    {
        public static List<MyReferralsDto> MyReferralMap(List<ReferrerFamily> myReferralItems)
        {
            var myReferralsList = new List<MyReferralsDto>();
            foreach (var myReferralItem in myReferralItems)
            {
                myReferralsList.Add(new MyReferralsDto
                {
                    FamilyName = myReferralItem.Family.FamilyName,
                    Mobile = string.IsNullOrWhiteSpace(myReferralItem.Family.Contactno) ? "" : myReferralItem.Family.Contactno,
                    ReferralDate = myReferralItem.Family.AddedDate,
                    Id = myReferralItem.FamilyId,
                    Status= (myReferralItem.Family.Confirmed==null?0: (myReferralItem.Family.Confirmed == true ? 1 : (myReferralItem.Family.Confirmed == false ? 2 : 2))),
                    CountVoucher = (myReferralItem.Family == null ? 0 : (myReferralItem.Family.Voucher == null ? 0 : myReferralItem.Family.Voucher.Count)),
                });
            }
            return myReferralsList;
        }
        public static List<MyReferralsDto> FamilyListMap(List<FoodbankFamily> myReferralItems)
        {
            var myReferralsList = new List<MyReferralsDto>();
            foreach (var myReferralItem in myReferralItems)
            {
                myReferralsList.Add(new MyReferralsDto
                {
                    FamilyName = myReferralItem.Family.FamilyName,
                    Mobile = string.IsNullOrWhiteSpace(myReferralItem.Family.Contactno) ? "" : myReferralItem.Family.Contactno,
                    ReferralDate = myReferralItem.Family.AddedDate,
                    Id = myReferralItem.FamilyId,
                    Status = (myReferralItem.Family.Confirmed == null ? 0 : (myReferralItem.Family.Confirmed == true ? 1 : (myReferralItem.Family.Confirmed == false ? 2 : 2))),
                });
            }
            return myReferralsList;
        }
        public static List<MyReferralsDto> MyReferralYearMap(List<ReferrerFamily> myReferralItems)
        {
            var myReferralsList = new List<MyReferralsDto>();
            foreach (var myReferralItem in myReferralItems)
            {
                myReferralsList.Add(new MyReferralsDto
                {
                    ReferralDate = myReferralItem.ReferralDate,
                    Id = myReferralItem.Id

                });
            }
            return myReferralsList;
        }
        public static List<MyReferralsDto> GetAllList(List<Referrers> myReferralItems)
        {
            var myReferralsList = new List<MyReferralsDto>();
            foreach (var myReferralItem in myReferralItems)
            {
                myReferralsList.Add(new MyReferralsDto
                {
                    Name = (myReferralItem.Contact == null ? "" : myReferralItem.Contact.ForeName) + " " + (myReferralItem.Contact == null ? "": myReferralItem.Contact.Surname),
                    Id = myReferralItem.Id

                });
            }
            return myReferralsList;
        }

        public static List<FoodbankReferrerDto> ReferrerMap(List<Referrers> myReferrerItems)
        {
            var myReferrerList = new List<FoodbankReferrerDto>();
            foreach (var myReferralItem in myReferrerItems)
            {
                myReferrerList.Add(new FoodbankReferrerDto
                {
                    Id = myReferralItem.Id,
                    UserName = myReferralItem.User.UserName,
                    Profession = myReferralItem.RefType.Name,
                    ContactNumber = myReferralItem.Contact?.Mobile,
                    Status  = myReferralItem.IsStatus,
                });
            }
            return myReferrerList;
        }
        public static ReferrerDto MyReferrerMap(Referrers myReferralItems)
        {
            var myReferralsList = new ReferrerDto();
            myReferralsList.Id = myReferralItems.Id;
            myReferralsList.RefTypeId = myReferralItems.RefTypeId;
            myReferralsList.ContactId = myReferralItems.ContactId;
            myReferralsList.AddressId = myReferralItems.AddressId;
            myReferralsList.Name = myReferralItems.Name;
            myReferralsList.ReffToken = myReferralItems.ReffToken;
            myReferralsList.IsVoucher = myReferralItems.IsVoucher;
            myReferralsList.DefaultParcelType = myReferralItems.DefaultParcelType;
            myReferralsList.ServiceDescription = myReferralItems.ServiceDescription;
            myReferralsList.UserId = myReferralItems.UserId;
            myReferralsList.FoodbankId = myReferralItems.FoodbankId;
            myReferralsList.IsStatus = myReferralItems.IsStatus; ;
            myReferralsList.PostponeDate = myReferralItems.PostponeDate;
            myReferralsList.Active = myReferralItems.Active;
            myReferralsList.Address = myReferralItems.Address;
            myReferralsList.Contact = myReferralItems.Contact;
            myReferralsList.RefType = myReferralItems.RefType;
            myReferralsList.User = myReferralItems.User;
            return myReferralsList;
        }
        public static ReferrerRegisterDto MyReferrerEditMap(Referrers myReferralItems)
        {
            var myReferralsList = new ReferrerRegisterDto();
            myReferralsList.UserName = myReferralItems.User.UserName;
            myReferralsList.UserId = myReferralItems.User.UserId;
            myReferralsList.FirstName = myReferralItems.Contact.ForeName;
            myReferralsList.LastName = myReferralItems.Contact.Surname;
            myReferralsList.Profession = myReferralItems.RefType.Name;
            myReferralsList.ProfessionId = myReferralItems.RefTypeId;
            myReferralsList.OrganisationName = myReferralItems.Contact.ForeName;
            myReferralsList.ContactId = myReferralItems.ContactId??0;
            myReferralsList.AddressId = myReferralItems.AddressId ?? 0;
            myReferralsList.Email = myReferralItems.Contact.Email;
            myReferralsList.Id = myReferralItems.Id;
            myReferralsList.ContactNumber = myReferralItems.Contact.Mobile;
            myReferralsList.PostCode = myReferralItems.Address.Postcode;
            myReferralsList.HouseName = myReferralItems.Address.HouseName;
            myReferralsList.HouseNumber = myReferralItems.Address.HouseNumber;
            myReferralsList.CountryID = myReferralItems.Address.CountryId;
            myReferralsList.StreetName = myReferralItems.Address.Street;
            myReferralsList.OtherAddressLine = myReferralItems.Address.OtherAddressLine;
            myReferralsList.City = myReferralItems.Address.City;
            myReferralsList.District = myReferralItems.Address.District;
            return myReferralsList;
        }
    }
}
