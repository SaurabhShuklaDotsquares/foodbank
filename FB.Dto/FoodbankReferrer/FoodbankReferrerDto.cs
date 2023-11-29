using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FoodbankReferrerDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Profession { get; set; }
        public string ContactNumber { get; set; }
        public int Status { get; set; }
    }
    public class ReferrerDto
    {
        public int Id { get; set; }
        public int? RefTypeId { get; set; }
        public int? ContactId { get; set; }
        public int? AddressId { get; set; }
        public string Name { get; set; }
        public string ReffToken { get; set; }
        public bool IsVoucher { get; set; }
        public int? DefaultParcelType { get; set; }
        public string ServiceDescription { get; set; }
        public int UserId { get; set; }
        public int? FoodbankId { get; set; }
        public int IsStatus { get; set; }
        public DateTime? PostponeDate { get; set; }
        public bool Active { get; set; }

        public virtual Fbaddress Address { get; set; }
        public virtual Fbcontact Contact { get; set; }
        public virtual ParcelType DefaultParcelTypeNavigation { get; set; }
        public virtual ReferrerType RefType { get; set; }
        public virtual User User { get; set; }
    }
}
