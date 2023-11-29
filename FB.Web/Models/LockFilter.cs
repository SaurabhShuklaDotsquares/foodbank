using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FB.Core;

namespace FB.Web
{
    public class LockFilter
    {
        public int Organisation { get; set; }
        public int Charity { get; set; }
        public int[] Branches { get; set; }
    }
}
