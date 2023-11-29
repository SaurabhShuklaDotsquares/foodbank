using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmopersonAdditonalDetails
    {
        public int PersonId { get; set; }
        public string MiddleName { get; set; }
        public bool IsMgo { get; set; }
        public bool IsMmo { get; set; }
        public byte Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string SchoolWorkPlace { get; set; }
        public bool? IsVisitingTeamMember { get; set; }
        public string VisitorComments { get; set; }
        public bool? IsFamilyContactPerson { get; set; }
        public bool? IsIncludeOnFamilySalutations { get; set; }
        public bool? NeverMail { get; set; }
        public int? MemberShipType { get; set; }
        public string MemberShipId { get; set; }
        public DateTime? DeceasedDate { get; set; }
        public DateTime? InActiveDate { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? MaritalStatus { get; set; }
        public string Envelope { get; set; }
        public string Formal { get; set; }
        public string InFormal { get; set; }
        public string FullName { get; set; }
        public bool? IsInFormal { get; set; }
        public string Photo { get; set; }
        public int? PrimaryEmail { get; set; }
        public int? PrimaryPhoneNumber { get; set; }
        public int? BirthYear { get; set; }
        public string MsmemberObjectId { get; set; }
        public string TenantId { get; set; }
        public string MsuserPrincipalName { get; set; }
        public string Mccpkpeople { get; set; }
        public string MccpkphoneprimaryEmail { get; set; }
        public string MccpkphoneprimaryPhoneNumber { get; set; }
        public byte? CommunicationPreference { get; set; }

        public virtual MmomaritalStatus MaritalStatusNavigation { get; set; }
        public virtual MmomembershipType MemberShipTypeNavigation { get; set; }
        public virtual Person Person { get; set; }
        public virtual Mmocontact PrimaryEmailNavigation { get; set; }
        public virtual Mmocontact PrimaryPhoneNumberNavigation { get; set; }
    }
}
