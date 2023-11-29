using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class MmoreportLabel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TopMargin { get; set; }
        public decimal BottomMargin { get; set; }
        public decimal LeftMargin { get; set; }
        public decimal RightMargin { get; set; }
        public decimal HorizontalPitch { get; set; }
        public decimal VerticalPitch { get; set; }
        public decimal LabelWidth { get; set; }
        public decimal LabelHeight { get; set; }
        public int NumberAcross { get; set; }
        public int NumberDown { get; set; }
        public int FontSize { get; set; }
        public bool? IsActive { get; set; }
        public int? CentralOfficeId { get; set; }
        public int? CharityId { get; set; }
        public int? BranchId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string AuditIp { get; set; }
        public int? AuditUserId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual CentralOffice CentralOffice { get; set; }
        public virtual Charity Charity { get; set; }
    }
}
