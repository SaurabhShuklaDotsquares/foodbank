using FB.Core;
using FB.Data.Models;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.ModalMapper
{
    public static class VolunteerAvailabilityUnavailabilityDtoMapper
    {
        public static VolunteerUnavailability MapUnavailability(VolunteerDto model, VolunteerUnavailability entity)
        {
            VolunteerUnavailability unavailability = new VolunteerUnavailability();
            //entity.Id = model.Unavailability.UnavailabilityId;
            unavailability.VolunteerId = model.VolunteerId;
            unavailability.FormDate = model.Unavailability.FromDate.ToDateTimeNullable();
            unavailability.ToDate = model.Unavailability.ToDate.ToDateTimeNullable();

            if (model.Unavailability.UnavailabilityTimeType == 1)
            {
                unavailability.AllDay = true;
                unavailability.TimeForm = TimeSpan.Parse("00:00:00");
                unavailability.TimeTo = TimeSpan.Parse("00:00:00");
            }
            else
            {
                unavailability.AllDay = false;
                unavailability.TimeForm = TimeSpan.Parse(model.Unavailability.TimeForm);
                unavailability.TimeTo = TimeSpan.Parse(model.Unavailability.TimeTo);
            }
            //entity.Checkbox = model.AllDayCheckbox;
            unavailability.Pattern = model.Unavailability.Pattern;
            unavailability.Frequency = model.Unavailability.FrequencyType;
            //entity.Comment = model.Unavailability.Comment;
            //entity.Active = model.Unavailability.Active;
            //entity.AuditIp = model.Unavailability.AuditIp;
            //entity.AuditUserId = model.Unavailability.AuditUserId;
            return unavailability;
        }

        public static VolunteerAvailability MapAvailability(VolunteerDto model, VolunteerAvailability entity)
        {
            VolunteerAvailability availability = new VolunteerAvailability();
            //entity.Id = model.Availability.AvailabilityId;
            availability.VolunteerId = model.VolunteerId;
            availability.FormDate = model.Availability.FromDate.ToDateTimeNullable();
            availability.ToDate = model.Availability.ToDate.ToDateTimeNullable();

            if (model.Availability.UnavailabilityTimeType == 1)
            {
                availability.AllDay = true;
                availability.TimeForm = TimeSpan.Parse("00:00:00");
                availability.TimeTo = TimeSpan.Parse("00:00:00");
            }
            else
            {
                availability.AllDay = false;
                availability.TimeForm = TimeSpan.Parse(model.Availability.TimeForm);
                availability.TimeTo = TimeSpan.Parse(model.Availability.TimeTo);
            }
            availability.Pattern = model.Availability.Pattern;
            availability.Frequency = model.Availability.FrequencyType;
            //entity.Comment = model.Unavailability.Comment;
            //entity.Active = model.Unavailability.Active;
            //entity.AuditIp = model.Unavailability.AuditIp;
            //entity.AuditUserId = model.Unavailability.AuditUserId;
            return availability;
        }

        public static User MapUser(VolunteerDto model, User entity)
        {
            entity.UserName = model.UserName;
            entity.Password = model.EditPassword;
            entity.PasswordQuestion = model.PasswordQuestion;
            entity.PasswordAnswer = model.PasswordAnswer;
            return entity;
        }

        public static Volunteer MapVolunteer(VolunteerDto model, Volunteer entity)
        {
            entity.Howelsecanyouhelp = model.HowCanYouHelp;
            entity.IndividualCouple = model.MaritalStatus;
            entity.Packingordelivery = model.WorkType;
            return entity;
        }
    }
}
