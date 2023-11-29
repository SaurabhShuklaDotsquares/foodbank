using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB.Core;
using FB.Data.Models;
using FB.Dto;

namespace FB.ModalMapper
{
    public class FamilyMapper
    {
        public static EditFamilyDto EditFamilyMapper(Family family)
        {
            EditFamilyDto fmt = new EditFamilyDto();
            fmt.Id = family.Id;
            fmt.FamilyName = family.FamilyName;
            fmt.FamilyToken = family.FamilyToken;
            fmt.LocalAuthCodeId = family.LocalAuthCodeId;
            fmt.DeliveryNote = family.DeliveryNote;
            fmt.Confirmed = family.Confirmed;
            fmt.ConfirmedById = family.ConfirmedById;
            fmt.Email = family.Email;
            fmt.Contactno = family.Contactno;
            fmt.Addressid = family.FamilyAddress.FirstOrDefault()?.AddressId;
            fmt.TotalFamily = family.TotalFamily;
            fmt.TotalAdults = family.TotalAdults;
            fmt.TotalChild = family.TotalChild;
            fmt.CentralOfficeID = family.CentralOfficeId ?? 0;
            fmt.CharityID = family.CharityId ?? 0;
            fmt.BranchID = family.BranchId ?? 0;
            fmt.CentralOfficeName = family.CentralOffice != null ? family.CentralOffice.OrganisationName : string.Empty;
            fmt.CharityName = family.Charity != null ? family.Charity.CharityName : string.Empty;
            fmt.BranchName = family.Branch != null ? family.Branch.BranchDescription : string.Empty;
            fmt.Active = family.Active;
            fmt.SelfRefferedName = ((eFamilyReferred)(int)family.SelfReffered).GetDescription();
            fmt.OtherAgencyName = (family.OtherAgency == true ? "Yes" : "No");
            fmt.ParcelDeliverDate = family.ParcelDeliverDate?.ToString("dd/MM/yyyy");
            fmt.StatusName = (family.Confirmed == null ? (ReferrersStatus.Pending).GetDescription() : (family.Confirmed == true ? (ReferrersStatus.Accept).GetDescription() : (family.Confirmed == false ? (ReferrersStatus.Reject).GetDescription() : (ReferrersStatus.Pending).GetDescription())));
            fmt.GDPRPreferences = family.Gdprpreferences;
            if (family.FamilyAddress.Count() > 0)
            {
                fmt.HouseName = family.FamilyAddress.FirstOrDefault().Address.HouseName;
                fmt.HouseNumber = family.FamilyAddress.FirstOrDefault().Address.HouseNumber;
                fmt.OtherAddressLine = family.FamilyAddress.FirstOrDefault().Address.OtherAddressLine;
                fmt.District = family.FamilyAddress.FirstOrDefault().Address.District;
                fmt.CountryID = family.FamilyAddress.FirstOrDefault().Address.CountryId;
                fmt.City = family.FamilyAddress.FirstOrDefault().Address.City;
                fmt.StreetName = family.FamilyAddress.FirstOrDefault().Address.Street;
                fmt.PostCode = family.FamilyAddress.FirstOrDefault().Address.Postcode;
            }
            foreach (var item in family.FamilyMember)
            {
                fmt.subfamilyname.Add((item.ForeName == null ? "" : item.ForeName));
                fmt.subfamilydob.Add(item.Dob?.ToString("dd/MM/yyyy"));
                fmt.subfamilyisadult.Add(item.IsAdult.ToString());
                fmt.subfamilynameIds.Add(item.Id.ToString());
                var alleryids = ""; var allerytableids = "";
                foreach (var itemsub in item.FamilyMemberAllergy)
                {
                    alleryids += itemsub.AllergyId.ToString() + "%";
                    allerytableids += itemsub.Id.ToString() + "%";
                }

                fmt.SubFamilyAllergryarry.Add(alleryids);
                fmt.SubFamilyAllergryarryId.Add(allerytableids);
            }
            return fmt;
        }
        public static AdminEditFamilyDto AdminEditFamilyMapper(Family family)
        {
            AdminEditFamilyDto fmt = new AdminEditFamilyDto();
            fmt.Id = family.Id;
            fmt.FamilyName = family.FamilyName;
            fmt.FamilyToken = family.FamilyToken;
            fmt.LocalAuthCodeId = family.LocalAuthCodeId?.ToString();
            fmt.DeliveryNote = family.DeliveryNote;
            fmt.Confirmed = family.Confirmed;
            fmt.ConfirmedById = family.ConfirmedById;
            fmt.Email = family.Email;
            fmt.Contactno = family.Contactno;
            fmt.Addressid = family.FamilyAddress.FirstOrDefault()?.AddressId;
            fmt.TotalFamily = family.TotalFamily;
            fmt.TotalAdults = family.TotalAdults;
            fmt.TotalChild = family.TotalChild;
            fmt.CentralOfficeID = family.CentralOfficeId ?? 0;
            fmt.CharityID = family.CharityId ?? 0;
            fmt.BranchID = family.BranchId ?? 0;
            fmt.CentralOfficeName = family.CentralOffice != null ? family.CentralOffice.OrganisationName : string.Empty;
            fmt.CharityName = family.Charity != null ? family.Charity.CharityName : string.Empty;
            fmt.BranchName = family.Branch != null ? family.Branch.BranchDescription : string.Empty;
            fmt.Active = family.Active;
            fmt.SelfRefferedName = ((eFamilyReferred)(int)family.SelfReffered).GetDescription();
            fmt.OtherAgencyName = (family.OtherAgency == true ? "Yes" : "No");
            fmt.ParcelDeliverDate = family.ParcelDeliverDate?.ToString("dd/MM/yyyy");
            fmt.GDPRPreferences = family.Gdprpreferences;
            if (family.FamilyAddress.Count() > 0)
            {
                fmt.HouseName = family.FamilyAddress.FirstOrDefault().Address.HouseName;
                fmt.HouseNumber = family.FamilyAddress.FirstOrDefault().Address.HouseNumber;
                fmt.OtherAddressLine = family.FamilyAddress.FirstOrDefault().Address.OtherAddressLine;
                fmt.District = family.FamilyAddress.FirstOrDefault().Address.District;
                fmt.CountryID = family.FamilyAddress.FirstOrDefault().Address.CountryId;
                fmt.City = family.FamilyAddress.FirstOrDefault().Address.City;
                fmt.StreetName = family.FamilyAddress.FirstOrDefault().Address.Street;
                fmt.PostCode = family.FamilyAddress.FirstOrDefault().Address.Postcode;
            }
            foreach (var item in family.FamilyMember)
            {
                fmt.subfamilyname.Add((item.ForeName == null ? "" : item.ForeName));
                fmt.subfamilydob.Add(item.Dob?.ToString("dd/MM/yyyy"));
                fmt.subfamilyisadult.Add(item.IsAdult.ToString());
                fmt.subfamilynameIds.Add(item.Id.ToString());
                var alleryids = ""; var allerytableids = "";
                foreach (var itemsub in item.FamilyMemberAllergy)
                {
                    alleryids += itemsub.AllergyId.ToString() + "%";
                    allerytableids += itemsub.Id.ToString() + "%";
                }

                fmt.SubFamilyAllergryarry.Add(alleryids);
                fmt.SubFamilyAllergryarryId.Add(allerytableids);
            }
            return fmt;
        }

    }
}
