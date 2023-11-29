using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
   public  class Professiondto
    {
        public int ProfessionId { get; set; }
        public int FoodBankId { get; set; }
        public string Title { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
