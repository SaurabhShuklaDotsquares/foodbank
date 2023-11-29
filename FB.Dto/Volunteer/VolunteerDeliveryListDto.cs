using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class VolunteerDeliveryListDto
    {
        public DateTime DateOfDelivery { get; set; }
        public string DeliveryAddress { get; set; }
        public string ParcelType { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
        public string FamilyName { get; set; }
    }
}
