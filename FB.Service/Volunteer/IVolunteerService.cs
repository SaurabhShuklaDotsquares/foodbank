using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IVolunteerService : IDisposable
    {
        void Save(Volunteer volunteer, bool isNew = true);
        KeyValuePair<int, List<VolunteerDeliveryListDto>> GetDeliveryList(DataTableServerSide searchModel, int volunteerid);
        int GetVolunteerCount();
        KeyValuePair<int, List<VolunteerDto>> GetVolunteers(DataTableServerSide searchModel);
        public void Savevolunteerandunavilvolunteer(VolunteerUnavailability entity, bool isNew = true);
        int GetVolunteerSkillCount();
        List<Skills> GetSkillByFoodbank(int? FoodbankId);
        List<VolunteerSkill> GetVolunteerSkillByVolunteerId(int? volunteerId);
        void DeleteVolunteerSkillById(int VolunteerSkillid);

        KeyValuePair<int, List<VolunteerPackingListDto>> GetPackingList(DataTableServerSide searchModel, int volunteerid);
        VolunteerUnavailability GetUnavailability(int id);
        void Save(VolunteerUnavailability unavailability, bool isNew = true);
        Volunteer GetVolunteerByUserId(int id);
        VolunteerAvailability GetAvailability(int id);
        void Saveavailability(VolunteerAvailability availability, bool isNew = true);
        Volunteer GetVolunteerById(int Id);
        KeyValuePair<int, List<AvailabilityDto>> GetAvailabilityList(DataTableServerSide searchModel, int volunteerId);
        KeyValuePair<int, List<UnavailabilityDto>> GetUnavailabilityList(DataTableServerSide searchModel, int volunteerId);
        VolunteerAvailability GetAvailabilityById(int AvailiabilityId);
        void DeleteVolunteerAvailability(int id);
        void DeleteVolunteerUnavailability(int id);
        VolunteerUnavailability GetUnavailabilityById(int UnavailiabilityId);
        KeyValuePair<int, List<VolunteerDto>> GetVolunteersByFoodbank(DataTableServerSide searchModel, int FoodbankID,int CharityId);
        KeyValuePair<int, List<VolunteerPackingAdminListDto>> GetPackingListByFoodbank(DataTableServerSide searchModel, int FoodbankId,int CharityId);

        List<Volunteer> GetVolunteerList(int foodbankId);
        List<Volunteer> GetVolunteersListByDate(string fromdate, string todate);
        List<ReportVolunterrsDto> GetVolunteersDetailsForReport(VolunteerReportDto model);
    }
}
