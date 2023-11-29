using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;
using FB.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FB.Dto
{
    public class FeedbackDonorDto
    {
        public FeedbackDonorDto()
        {
         
        }

        public int PersonID { get; set; }
        public int FoodbankId { get; set; } 
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Suffix { get; set; }
        public string Reference { get; set; }
        public string DonorReference { get; set; }
        public string UserName { get; set; }
        public string CentralOfficeName { get; set; }
        public string CharityName { get; set; }
        public string Branch { get; set; }
        public string ReferenceType { get; set; }
        public string Title { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2} {3}",
                    !string.IsNullOrEmpty(Title) ? Title : "",
                    !string.IsNullOrEmpty(Forenames) ? Forenames : "",
                    Surname);
            }
        }

}
}
