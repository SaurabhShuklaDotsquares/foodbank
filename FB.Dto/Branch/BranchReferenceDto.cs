using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FB.Dto.Branch
{
   public class BranchReferenceDto
    {
       public List<DonorReferences> References { get; set; }
       public string DonorReference { get; set; }
       public bool IsNextReference { get; set; }
    }

   public class DonorReferences
   {
       public string BranchName { get; set; }
       public string DonorReference { get; set; }
   }
}
