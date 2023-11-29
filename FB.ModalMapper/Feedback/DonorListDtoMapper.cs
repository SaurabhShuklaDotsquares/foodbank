using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
namespace FB.ModalMapper
{
    public class FeedbackDonorDtoMapper
    {
        public static List<FeedbackDonorDto> MapFeedbackDonorListDto(List<DonorFoodbank> List,string OrganisationName,string BranchDescription)
        {
            List<FeedbackDonorDto> returnList = new List<FeedbackDonorDto>();

            foreach (var donorlist in List)
            {
                FeedbackDonorDto fmt = new FeedbackDonorDto();
                fmt.PersonID = donorlist.DonorId;
                fmt.FoodbankId = donorlist.FoodBankId;
                fmt.Forenames = donorlist.Donor.Forenames;
                fmt.Surname = donorlist.Donor.Surname;
                fmt.Email = donorlist.Donor.Email;
                fmt.Suffix = donorlist.Donor.Suffix;
                fmt.Reference = donorlist.Donor.Reference;
                fmt.DonorReference = donorlist.Donor.DonorReference;
                fmt.CentralOfficeName = OrganisationName;
                fmt.Branch = BranchDescription;
                fmt.Title = donorlist.Donor.Title;
                returnList.Add(fmt);
            }
            return returnList;
        }
    }
}