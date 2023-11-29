using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IAddressService : IDisposable
    {
        void Save(Address address, bool isNew = true);
        void Save(List<Address> addresses, bool isNew = true);
        void Delete(int id);
        Address GetAddress(int id);
        bool IsAccessibleAddress(object id, int? centralOfficeId = null, int? charityId = null, int? branchId = null);
        List<Address> GetAddressessByPersonsId(int personId);
        List<Address> GetAddressessByHouseholdId(int householdId);

        Fbaddress GetFBAddress(int id);
        void Save(Fbaddress address, bool isNew = true);
        void FbaddressDelete(int id);
        Fbaddress GetFBAddressByUserid(int Userid);
    }
}
