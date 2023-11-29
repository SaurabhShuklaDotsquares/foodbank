using FB.Core;
using FB.Data.Models;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
    public class AddressService : IAddressService
    {
        private IRepository<Address> repoAddress;
        private IRepository<Fbaddress> repoFbaddress;
        public AddressService(IRepository<Address> _repoAddress, IRepository<Fbaddress> _repoFbaddress)
        {
            this.repoAddress = _repoAddress;
            repoFbaddress = _repoFbaddress;
        }

        /// <summary>
        /// To get single entity of address
        /// </summary>
        /// <param name="id"></param>
        /// <returns>single record of address</returns>
        public Address GetAddress(int id)
        {
            return repoAddress.FindById(id);
        }

        /// <summary>
        /// To save the address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="isNew"></param>
        public void Save(Address address, bool isNew = true)
        {
            if (isNew)
            {
                repoAddress.Insert(address);
            }
            else
            {
                repoAddress.Update(address);
            }
        }

        /// <summary>
        /// To delete address
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repoAddress.Delete(id);
        }

        /// <summary>
        /// Get Accessible membership Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="centralOfficeId"></param>
        /// <returns></returns>
        public bool IsAccessibleAddress(object id, int? centralOfficeId = null, int? charityId = null, int? branchId = null)
        {
            int checkAddressId = Convert.ToInt32(id);

            var predicate = PredicateBuilder.True<Address>();
            predicate = predicate.And(m => m.PersonId == checkAddressId);

            if (centralOfficeId.HasValue)
                predicate = predicate.And(m => m.CentralOfficeId == centralOfficeId.Value);
            if (charityId.HasValue)
                predicate = predicate.And(m => m.CharityId == charityId.Value);
            if (branchId.HasValue)
                predicate = predicate.And(m => m.BranchId == branchId.Value);

            return repoAddress
                .Query()
                .Filter(predicate)
                .Get() != null;
        }



        /// <summary>
        /// Get Primary Addressess By PersonsId
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<Address> GetAddressessByPersonsId(int personId)
        {
            return repoAddress.Query().Filter(x => x.PersonId == personId && x.MmoaddressType == (byte)AddressTypes.Primary).Get().ToList();
        }

        /// <summary>
        /// Get Primary Addressess By HouseholdId
        /// </summary>
        /// <param name="householdId"></param>
        /// <returns></returns>
        public List<Address> GetAddressessByHouseholdId(int householdId)
        {
            return repoAddress.Query().Filter(x => x.HouseholdId == householdId && x.MmoaddressType == (byte)AddressTypes.Primary).Get().ToList();
        }


        /// <summary>
        /// To save or update the multiple address
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="isNew"></param>
        public void Save(List<Address> addresses, bool isNew = true)
        {
            if (isNew)
            {
                repoAddress.InsertCollection(addresses);
            }
            else
            {
                repoAddress.UpdateCollection(addresses);
            }
        }


        public Fbaddress GetFBAddress(int id)
        {
            return repoFbaddress.FindById(id);
        }
        public Fbaddress GetFBAddressByUserid(int Userid)
        {
            return repoFbaddress.Query().Filter(x => x.UserId == Userid).Get().FirstOrDefault();
        }
        /// <summary>
        /// To save the address
        /// </summary>
        /// <param name="address"></param>
        /// <param name="isNew"></param>
        public void Save(Fbaddress address, bool isNew = true)
        {
            if (isNew)
            {
                repoFbaddress.Insert(address);
            }
            else
            {
                repoFbaddress.Update(address);
            }
        }
        public void FbaddressDelete(int id)
        {
            repoFbaddress.Delete(id);
        }

        public void Dispose()
        {
            if (repoAddress != null)
            {
                repoAddress.Dispose();
                repoAddress = null;
            }
            if (repoFbaddress != null)
            {
                repoFbaddress.Dispose();
                repoFbaddress = null;
            }
        }
    }
}
