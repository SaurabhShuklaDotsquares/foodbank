using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Dto
{
    public class FullAddressDto
    {
        public string FullAddress
        {
            get
            {

                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                                   (!string.IsNullOrWhiteSpace(HouseName) ? HouseName.Trim() + ", " : string.Empty),
                                   !string.IsNullOrWhiteSpace(HouseNumber) ? HouseNumber.Trim() + " " : string.Empty,
                                   (!string.IsNullOrWhiteSpace(District) ? District.Trim() + " " : string.Empty),
                                   (!string.IsNullOrWhiteSpace(StreetName) ? StreetName.Trim() + ", " : string.Empty),
                                   (!string.IsNullOrWhiteSpace(OtherAddressLine) ? OtherAddressLine.Trim() + ", " : string.Empty),
                                   (!string.IsNullOrWhiteSpace(City) ? City.Trim() + ", " : string.Empty),
                                   (IsCountyAddress ? (!string.IsNullOrWhiteSpace(County) ? County.Trim() + ", " : string.Empty) : string.Empty),
                                   (!string.IsNullOrWhiteSpace(CountryName) ? CountryName.Trim() + ", " : string.Empty),
                                   (!string.IsNullOrWhiteSpace(PostCode) ? PostCode.Trim() : string.Empty)).Trim().Trim(',').Trim();
            }
        }

        public string LabelFullAddress
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(HouseName))
                    sb.Append(HouseName.Trim()).Append("<br />");

                if (!string.IsNullOrWhiteSpace(HouseNumber) || !string.IsNullOrWhiteSpace(StreetName))
                {
                    string streetAddr = $"{(!string.IsNullOrWhiteSpace(HouseNumber) ? (HouseNumber.Trim() + ' ') : string.Empty)}{(!string.IsNullOrWhiteSpace(StreetName) ? StreetName.Trim() : string.Empty)}";
                    sb.Append(streetAddr.Trim()).Append("<br />");
                }

                if (!string.IsNullOrWhiteSpace(OtherAddressLine))
                    sb.Append(OtherAddressLine.Trim()).Append("<br />");

                if (!string.IsNullOrWhiteSpace(City))
                    sb.Append(City.Trim()).Append("<br />");

                if (IsCountyAddress)
                {
                    if (!string.IsNullOrWhiteSpace(County))
                        sb.Append(County.Trim()).Append("<br />");
                }

                if (!string.IsNullOrWhiteSpace(CountryName))
                    sb.Append(CountryName.Trim()).Append("<br />");

                if (!string.IsNullOrWhiteSpace(PostCode))
                    sb.Append(PostCode.Trim());

                return sb.ToString();
            }
        }

        public string AddressDescription { get; set; }
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string OtherAddressLine { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public bool IsCountyAddress { get; set; }
        public string County { get; set; }
        public string CountryName { get; set; }
    }
}
