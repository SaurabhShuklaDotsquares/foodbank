using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FB.Core;
using FB.Data.Models;
using FB.Dto;
namespace FB.ModalMapper
{
    public class FeedbackDtoMapper
    {
        public static FeedbackMasterDTO Map(FeedbackMaster userDefinedField)
        {
            string userDefinedFieldOptionList = string.Empty;
            if (userDefinedField.FeedbackMasterFieldOption != null && userDefinedField.FeedbackMasterFieldOption.Count > 0)
            {
                userDefinedFieldOptionList = String.Join(",", userDefinedField.FeedbackMasterFieldOption.Select(uf => uf.OptionValue).ToList());
            }

            return new FeedbackMasterDTO
            {
                FieldId = userDefinedField.FieldId,
                IsHouseHold = (PersonTypes)userDefinedField.IsHouseHold,
                FieldDescription = userDefinedField.FieldDescription,
                FieldType = (int)userDefinedField.FieldType,
                FieldTypeName = ((UserFieldType)userDefinedField.FieldType).GetDescription(), //Enum.GetName(typeof(UserFieldType), userDefinedField.FieldType),
                FieldDefaultValue = (userDefinedField.FieldType != (int)UserFieldType.List && userDefinedField.FieldType != (int)UserFieldType.Logical ? userDefinedField.FieldDefaultValue : string.Empty),
                ListFieldDefaultValue = userDefinedField.FieldType == (int)UserFieldType.List ? userDefinedField.FieldDefaultValue : string.Empty,
                IsLogical = (userDefinedField.FieldType == (int)UserFieldType.Logical) ? userDefinedField.FieldDefaultValue.ToBoolean() : false,
                AuditIp = userDefinedField.AuditIp,
                AuditUserId = userDefinedField.AuditUserId,
                UserDefinedFieldOptionList = userDefinedField.FieldType == (int)UserFieldType.List ? userDefinedFieldOptionList : string.Empty,
                IsAutoAssignDefaultValue = userDefinedField.IsAutoAssignDefaultValue ?? false
            };
        }

      
        public static List<FeedbackMasterDTO> Map(List<FeedbackMaster> userDefinedFields)
        {
            var userDefinedFieldList = new List<FeedbackMasterDTO>();
            foreach (var userDefinedField in userDefinedFields)
            {
                userDefinedFieldList.Add(new FeedbackMasterDTO
                {
                    FieldId = userDefinedField.FieldId,
                    FieldDescription = userDefinedField.FieldDescription,
                    FieldType = (int)userDefinedField.FieldType,
                    FieldTypeName = ((UserFieldType)userDefinedField.FieldType).GetDescription(),
                    FieldDefaultValue = userDefinedField.FieldDefaultValue,
                    AuditIp = userDefinedField.AuditIp,
                    AuditUserId = userDefinedField.AuditUserId,
                    IsAutoAssignDefaultValue = userDefinedField.IsAutoAssignDefaultValue ?? false
                });
            }
            return userDefinedFieldList;
        }
        public static FeedbackDto FeedbackManage(Parcels feedback)
        {
            var Feedbackres = new FeedbackDto();
            if(feedback!=null)
            { 
            Feedbackres.ToDaydate = System.DateTime.Now.ToString("dd/MM/yyyy");
            Feedbackres.PackingDate = feedback.PackOnDate==null?"":feedback.PackOnDate?.ToString("dd/MM/yyyy");
            Feedbackres.FoodbankId = feedback.FoodbankId??0;
            Feedbackres.DeliveryDate = feedback.DeliveryDate?.ToString("dd/MM/yyyy");
            Feedbackres.ParcelTypeName = ((ParcelTypes)feedback.ParcelTypeId).GetDescription();
           
            Feedbackres.ParcelId = feedback.Id;
            Feedbackres.PackersName = (feedback.Packer!=null? (feedback.Packer.Contact != null ? (feedback.Packer.Contact.ForeName ) : "") : "");
            Feedbackres.DeliversName = (feedback.Deliverer != null ? (feedback.Deliverer.Contact != null ? (feedback.Deliverer.Contact.ForeName) : "") : "");
            Feedbackres.ParcelToken = feedback.ParcelToken;
            if (feedback.Family != null)
            {
                Feedbackres.FamilyName = feedback.Family.FamilyName;
                Feedbackres.FamilyId = feedback.Family.Id;
            }
            }
            return Feedbackres;
        }
        public static List<FeedbackMasterDTO> MapList(List<FeedbackMaster> userDefinedFieldlist)
        {
            List<FeedbackMasterDTO> returnList = new List<FeedbackMasterDTO>();

            foreach (var userDefinedField in userDefinedFieldlist)
            {
                FeedbackMasterDTO fmt = new FeedbackMasterDTO();
                string userDefinedFieldOptionList = string.Empty;
                if (userDefinedField.FeedbackMasterFieldOption != null && userDefinedField.FeedbackMasterFieldOption.Count > 0)
                {
                    userDefinedFieldOptionList = String.Join(",", userDefinedField.FeedbackMasterFieldOption.Select(uf => uf.OptionValue).ToList());
                }
                fmt.FieldId = userDefinedField.FieldId;
                fmt.IsHouseHold = (PersonTypes)userDefinedField.IsHouseHold;
                fmt.FieldDescription = userDefinedField.FieldDescription;
                fmt.FieldType = (int)userDefinedField.FieldType;
                fmt.FieldTypeName = ((UserFieldType)userDefinedField.FieldType).GetDescription(); //Enum.GetName(typeof(UserFieldType), userDefinedField.FieldType),
                fmt.FieldDefaultValue = (userDefinedField.FieldType != (int)UserFieldType.List && userDefinedField.FieldType != (int)UserFieldType.Logical ? userDefinedField.FieldDefaultValue : string.Empty);
                fmt.ListFieldDefaultValue = userDefinedField.FieldType == (int)UserFieldType.List ? userDefinedField.FieldDefaultValue : string.Empty;
                fmt.IsLogical = (userDefinedField.FieldType == (int)UserFieldType.Logical) ? userDefinedField.FieldDefaultValue.ToBoolean() : false;
                fmt.AuditIp = userDefinedField.AuditIp;
                fmt.AuditUserId = userDefinedField.AuditUserId;
                fmt.UserDefinedFieldOptionList = userDefinedField.FieldType == (int)UserFieldType.List ? userDefinedFieldOptionList : string.Empty;
                fmt.IsAutoAssignDefaultValue = userDefinedField.IsAutoAssignDefaultValue ?? false;
                fmt.Option = MapOptionList(userDefinedField.FeedbackMasterFieldOption.ToList());
                returnList.Add(fmt);
            }
            return returnList;
        }
        public static List<FeedbackMasterFieldOptionDto> MapOptionList(List<FeedbackMasterFieldOption> userDefinedFieldlist)
        {
            List<FeedbackMasterFieldOptionDto> returnList = new List<FeedbackMasterFieldOptionDto>();

            foreach (var userDefinedField in userDefinedFieldlist)
            {
                FeedbackMasterFieldOptionDto fmt = new FeedbackMasterFieldOptionDto();
                fmt.OptionId = userDefinedField.OptionId;
                fmt.OptionVaue = userDefinedField.OptionValue;
                fmt.UserDefinedFieldId = userDefinedField.UserDefinedFieldId ?? 0;


                returnList.Add(fmt);
            }
            return returnList;
        }
        public static Feedback SaveFeedback(FeedbackDto FeedbackDtonew)
        {
            Feedback returnList = new Feedback();
            //FeedbackFormDetails formdetails = new FeedbackFormDetails();
            returnList.FamilyId = FeedbackDtonew.FamilyId;
            returnList.ParcelId = FeedbackDtonew.ParcelId;
            returnList.FoodbankId = FeedbackDtonew.FoodbankId;
            returnList.DateCompletd = System.DateTime.Now;
            returnList.FeedbackFormDetails = MapDtotoObject(FeedbackDtonew.FeedbackFormDetails);
            return returnList;
        }
        public static List<FeedbackFormDetails> MapDtotoObject(List<FeedbackFormDetailsDto> FeedbackDtonew)
        {
            List<FeedbackFormDetails> returnList = new List<FeedbackFormDetails>();

            foreach (var userDefinedField in FeedbackDtonew)
            {
                FeedbackFormDetails fmt = new FeedbackFormDetails();
                fmt.FeedbackMasterId = userDefinedField.FeedbackMasterId;
                fmt.Answer = userDefinedField.Answer;
                returnList.Add(fmt);
            }

            return returnList;
        }
        public static List<FeedbackDto> MapListFeedbackToFeedbackDto(List<Feedback> Feedbacklist)
        {
            List<FeedbackDto> returnList = new List<FeedbackDto>();

            foreach (var Feedback in Feedbacklist)
            {
                var Feedbackres = new FeedbackDto();
                Feedbackres.FamilyName = Feedback.Family.FamilyName;
                Feedbackres.DateCompletd = Feedback.DateCompletd;
                Feedbackres.PackingDate = (Feedback.Parcel==null?"": Feedback.Parcel.PackedDate?.ToString("dd/MM/yyyy"));
                Feedbackres.DeliveryDate = Feedback.Parcel == null ? "" : Feedback.Parcel.DeliveryDate?.ToString("dd/MM/yyyy");
                Feedbackres.ParcelTypeName = Feedback.Parcel == null ? "" : ((ParcelTypes)Feedback.Parcel.ParcelTypeId).GetDescription();
                Feedbackres.FamilyId = Feedback.Family.Id;
                Feedbackres.Id = Feedback.Id;
                returnList.Add(Feedbackres);
            }

            return returnList;
        }
        public static List<FeedbackFormDetailsFormDto> MapFeedbackFormDetailsFormDto(List<FeedbackFormDetails> List)
        {
            List<FeedbackFormDetailsFormDto> returnList = new List<FeedbackFormDetailsFormDto>();

            foreach (var single in List)
            {
                FeedbackFormDetailsFormDto fmt = new FeedbackFormDetailsFormDto();
                fmt.FeedbackId = single.FeedbackId;
                fmt.FeedbackMasterId = single.FeedbackMaster.FieldId;
                fmt.Question = single.FeedbackMaster.FieldDescription;
                fmt.Answer = single.Answer;
                fmt.DateCompletd = single.Feedback.DateCompletd?.ToString("dd/MM/yyyy");
                fmt.FamilyName = single.Feedback.Family.FamilyName;
                fmt.ParcelTypeName = ((ParcelTypes)single.Feedback.Parcel.ParcelTypeId).GetDescription();
                fmt.PackingDate = single.Feedback.Parcel.PackedDate?.ToString("dd/MM/yyyy");
                fmt.DeliveryDate = single.Feedback.Parcel.DeliveryDate?.ToString("dd/MM/yyyy");
                fmt.PackersName = (single.Feedback.Parcel.Packer != null ? (single.Feedback.Parcel.Packer.User != null ? (single.Feedback.Parcel.Packer.User.FirstName + " " + single.Feedback.Parcel.Packer.User.LastName) : "") : "");
                fmt.DeliversName = (single.Feedback.Parcel.Deliverer != null ? (single.Feedback.Parcel.Deliverer.User != null ? (single.Feedback.Parcel.Deliverer.User.FirstName + " " + single.Feedback.Parcel.Deliverer.User.LastName) : "") : "");
                returnList.Add(fmt);
            }
            return returnList;
        }
    }
}