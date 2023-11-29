using FB.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
namespace FB.Dto
{
    public class VolunteerDto
    {

        public int VolunteerId { get; set; }
        public int? LocationId { get; set; }
        public int? PartnerId { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ContactNumber { get; set; }
        public bool CanDrive { get; set; }
        public bool DeliveryDriver { get; set; }
        public int? DeliveryLimitPerShift { get; set; }
        public int ContactId { get; set; }
        public string OrganisationName { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? Frequency { get; set; }
        public int? IndividualCouple { get; set; }
        public int? Packingordelivery { get; set; }
        public string Userid { get; set; }
        public string Howelsecanyouhelp { get; set; }
        public int? Daysofweeks { get; set; }
        public IFormFile ImportFile { get; set; }
        public DateTime? Singledate { get; set; }
        public string CredentialsRequest { get; set; }
        public string VolunteerName { get; set; }
        public int UserId { get; set; }
        public string HowCanYouHelp { get; set; }
        public int WorkType { get; set; }
        public int MaritalStatus { get; set; }
        public bool IsRegularAvailability { get; set; }
        public bool IsUnavailability { get; set; }
        //public int Frequency { get; set; }
        [DisplayName("Change Password")]
        public bool IsChangePassword { get; set; }
        public bool ChangePassword { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string EditPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }
        public string IndividualCouplename { get; set; }
        public string Packingordeliveryname { get; set; }
        public bool IsDBScheck { get; set; }
        public int TotalDelivery { get; set; }
        public int TotalPacking { get; set; }
        public string ProfilePic { get; set; }
        public string DocPath { get; set; }
        public List<Skills> Skills { get; set; }
        public List<VolunteerSkill> VolunteerSkill { get; set; }
        public UnavailabilityDto Unavailability { get; set; }
        public AvailabilityDto Availability { get; set; }
    }
    public class VolunteerContactDto
    {
        public int DonorId { get; set; }
        public string Refrence { get; set; }
        public int DonationType { get; set; }
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public string Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public DateTime DonationDate { get; set; }
        public String CauseofDonation { get; set; }
        public int Status { get; set; }
    }
    public class VolunteerSkiilDto
    {
        public DateTime? CreatedDate { get; set; }
        public string PaymentGateway { get; set; }
        public string CustomerName { get; set; }
        public string Amount { get; set; }
    }
    public class VolunteerAdminDto
    {

        public int VolunteerId { get; set; }
        public int? LocationId { get; set; }
        public int? PartnerId { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public DateTime? AvailableTo { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public string ContactNumber { get; set; }
        public bool CanDrive { get; set; }
        public bool DeliveryDriver { get; set; }
        public int? DeliveryLimitPerShift { get; set; }
        public int ContactId { get; set; }
        public string OrganisationName { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? Frequency { get; set; }
        public int? IndividualCouple { get; set; }
        public int? Packingordelivery { get; set; }
        public string Userid { get; set; }
        public string Howelsecanyouhelp { get; set; }
        public int? Daysofweeks { get; set; }
        public IFormFile ImportFile { get; set; }
        public DateTime? Singledate { get; set; }
        public string CredentialsRequest { get; set; }
        public string VolunteerName { get; set; }
        public int UserId { get; set; }
        public string HowCanYouHelp { get; set; }
        public int WorkType { get; set; }
        public int MaritalStatus { get; set; }
        public bool IsRegularAvailability { get; set; }
        public bool IsUnavailability { get; set; }
        //public int Frequency { get; set; }
        [DisplayName("Change Password")]
        public bool IsChangePassword { get; set; }
        public bool ChangePassword { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string EditPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Password Question")]
        public string PasswordQuestion { get; set; }

        [DisplayName("Password Answer")]
        public string PasswordAnswer { get; set; }
        public string IndividualCouplename { get; set; }
        public string Packingordeliveryname { get; set; }
        public bool IsDBScheck { get; set; }
        public int FoodbankId { get; set; }
        public string SkillsIds { get; set; }
        public int SkillsId { get; set; }
        public string DocPath { get; set; }
        public UnavailabilityDto Unavailability { get; set; }
        public AvailabilityDto Availability { get; set; }
        public List<VolunteerSkill> VolunteerSkill { get; set; }
    }
}
