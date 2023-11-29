using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class TypeMaster
    {
        public int EnumId { get; set; }
        public string EnumGroup { get; set; }
        public int EnumValue { get; set; }
        public string EnumText { get; set; }
        public string EnumRefColumn { get; set; }
        public string EnumRefTable { get; set; }
    }
}
