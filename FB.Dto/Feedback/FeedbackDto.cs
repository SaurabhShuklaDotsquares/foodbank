using FB.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FB.Dto
{
  public class FeedbackDto
    {
        public FeedbackDto()
        {
            FeedbackFormDetails = new List<FeedbackFormDetailsDto>();
        }
        public int Id { get; set; }
        public int FamilyId { get; set; }
        public int FoodbankId { get; set; }
        public DateTime? DateCompletd { get; set; }
        public int? ParcelId { get; set; }

        //public  Family Family { get; set; }
        //public  Foodbank Foodbank { get; set; }
        public  string ParcelToken { get; set; }
        public string ToDaydate { get; set; }
        public string dynamicString { get; set; }
        public string FamilyName { get; set; }
        public string ParcelTypeName { get; set; }
        public string PackingDate { get; set; }
        public string PackersName { get; set; }

        public string DeliversName { get; set; }


        public string DeliveryDate { get; set; }
        public List<FeedbackFormDetailsDto> FeedbackFormDetails { get; set; }
    }

    
}
