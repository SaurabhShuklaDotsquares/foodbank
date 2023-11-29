using System;
using System.Collections.Generic;

namespace FB.Data.Models
{
    public partial class DeleteTablesData
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
