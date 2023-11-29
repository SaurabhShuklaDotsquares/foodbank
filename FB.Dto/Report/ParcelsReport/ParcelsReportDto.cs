using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
    public class ParcelsReportDto
    {
        [DisplayName("From")]
        public DateTime? DateFrom { get; set; }
        [DisplayName("To")]
        public DateTime? DateTo { get; set; }
        [DisplayName("Parcels Delivery Date")]
        public DateTime? ParcelsDeliveryDate { get; set; }
        [DisplayName("Status")]
        public int? StatusId { get; set; }
        [DisplayName("Parcel Type")]
        public int? ParcelTypeId { get; set; }
        public string ParcelIds { get; set; }
        [DisplayName("Include food detail")]
        public bool IncludeFoodDetails { get; set; }
        [DisplayName("List by family")]
        public bool IsListByFamily { get; set; }
    }
}
