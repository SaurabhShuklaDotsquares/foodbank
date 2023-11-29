using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class UserDataAccessDto
    {
        public int CentralOfficeID { get; set; }
        public Dictionary<int, List<int>> CharityBranches { get; set; }
    }
}
