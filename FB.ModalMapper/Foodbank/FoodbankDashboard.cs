using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.Dto.Foodbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FB.ModalMapper
{
    public class FoodbankDashboard
    {
        public static List<DashboardDto> MyParcelYearMap(List<Parcels> myReferralItems)
        {
            var myReferralsList = new List<DashboardDto>();
            foreach (var myReferralItem in myReferralItems)
            {
                myReferralsList.Add(new DashboardDto
                {
                    Date = myReferralItem.DeliveredDate,
                    Id = myReferralItem.Id

                });
            }
            return myReferralsList;
        }

        public static List<int> MyParcelMonthMap(List<Parcels> parcelList)
        {
            List<int> age = new List<int>();
            var myReferralsList = new List<FamilyPersonDto>();
            foreach (var parcel in parcelList)
            {
                if (parcel.Family != null)
                {
                    foreach (var record in parcel.Family.FamilyMember)
                    {
                        age.Add(record.Dob.Value.GetAge());
                    }
                }
            }
            return age;
        }
    }
}
