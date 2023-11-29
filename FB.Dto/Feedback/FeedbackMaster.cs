using FB.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
  public class FeedbackMasterDTO
    {
        public int FieldId { get; set; }
        [DisplayName("Families or Individual")]
        public PersonTypes IsHouseHold { get; set; }
        [DisplayName("Field Description*")]
        public string FieldDescription { get; set; }
        [DisplayName("Field Type*")]
        public int FieldType { get; set; }
        public string FieldTypeName { get; set; }
        [DisplayName("Default Value")]
        public string FieldDefaultValue { get; set; }
        [DisplayName("Option Default Value")]
        public string ListFieldDefaultValue { get; set; }
        public int? CentralOfficeId { get; set; }
        public string CentralOfficeName { get; set; }
        public int? CharityId { get; set; }
        public string CharityName { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }
        [DisplayName("Options")]
        public string UserDefinedFieldOptionList { get; set; }
        [DisplayName("Default Logical Value")]
        public bool IsLogical { get; set; }
        [DisplayName("Auto Assign Default Value")]
        public bool IsAutoAssignDefaultValue { get; set; }
        public List<FeedbackMasterFieldOptionDto> Option { get; set; }

    }

    public class FeedbackMasterFieldOptionDto
    {
        public int OptionId { get; set; }
        public string OptionVaue { get; set; }
        public int UserDefinedFieldId { get; set; }
    }
}
