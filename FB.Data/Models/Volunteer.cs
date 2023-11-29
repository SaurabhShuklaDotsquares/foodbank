using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class Volunteer
    {
        public Volunteer()
        {
            ParcelsDeliverer = new HashSet<Parcels>();
            ParcelsPacker = new HashSet<Parcels>();
            VolunteerAvailability = new HashSet<VolunteerAvailability>();
            VolunteerSkill = new HashSet<VolunteerSkill>();
            VolunteerUnavailability = new HashSet<VolunteerUnavailability>();
        }

        public int Id { get; set; }
        public int? LocationId { get; set; }
        public int? PartnerId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ConfirmedBy { get; set; }
        public bool CanDrive { get; set; }
        public bool DeliveryDriver { get; set; }
        public int? DeliveryLimitPerShift { get; set; }
        public int ContactId { get; set; }
        public int? IndividualCouple { get; set; }
        public int? Packingordelivery { get; set; }
        public string Howelsecanyouhelp { get; set; }
        public int? UserId { get; set; }
        public int? FoodbankId { get; set; }
        public bool IsDbscheck { get; set; }
        public DateTime? IsDbscheckDate { get; set; }
        public bool? Active { get; set; }
        public string ProfilePic { get; set; }
        public string DocPath { get; set; }

        public virtual Fbcontact Contact { get; set; }
        public virtual Foodbank Foodbank { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Parcels> ParcelsDeliverer { get; set; }
        public virtual ICollection<Parcels> ParcelsPacker { get; set; }
        public virtual ICollection<VolunteerAvailability> VolunteerAvailability { get; set; }
        public virtual ICollection<VolunteerSkill> VolunteerSkill { get; set; }
        public virtual ICollection<VolunteerUnavailability> VolunteerUnavailability { get; set; }
    }
}
