using System;
using System.Collections.Generic;
using System.Linq;
using FB.Data;
using FB.Dto;
using FB.Core;
using FB.Repo;
using FB.Data.Models;

namespace FB.Service
{
    public class CountryService : ICountryService
    {
        private IRepository<Country> repoCountry;

        public CountryService(IRepository<Country> _repoCountry)
        {
            this.repoCountry = _repoCountry;
        }

        /// <summary>
        /// To save the country
        /// </summary>
        /// <param name="country"></param>
        /// <param name="isNew"></param>
        public void Save(Country country, bool isNew = true)
        {
            if (isNew)
            {
                repoCountry.Insert(country);
            }
            else
            {
                repoCountry.Update(country);
            }
        }

        /// <summary>
        /// To delete the country
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repoCountry.Delete(id);
        }

        /// <summary>
        /// To get the single country
        /// </summary>
        /// <param name="CountryName">string</param>
        /// <returns></returns>
        public Country GetCountryByName(string CountryName)
        {
            return repoCountry.Query().Filter(c => c.CountryName == CountryName).Get().FirstOrDefault();
        }


        /// <summary>
        /// To get the single country entity
        /// </summary>
        /// <param name="id">int</param>
        /// <returns></returns>
        public Country GetCountryEntity(int id)
        {
            return repoCountry.FindById(id);
        }

        /// <summary>
        /// To get the list of countries
        /// </summary>
        /// <returns></returns>
        public List<Country> GetCountries(int? organisationId = null)
        {
            return repoCountry.Query().Filter(x=>!string.IsNullOrWhiteSpace(x.CountryName)).OrderBy(o => o.OrderBy(oo => oo.CountryName)).Get().ToList();
        }

        public void Dispose()
        {
            if (repoCountry != null)
            {
                repoCountry.Dispose();
                repoCountry = null;
            }
        }
    }
}
