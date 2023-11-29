using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmouserDefinedFieldOption
    {
        public int OptionId { get; set; }
        public string OptionValue { get; set; }
        public int UserDefinedFieldId { get; set; }

        public virtual MmouserDefinedField UserDefinedField { get; set; }
    }
}
