using System;
using System.Collections.Generic;
using FB.Core;
using FB.Data;
using FB.Data.Models;
using FB.Dto;

namespace FB.Service
{
    public interface ICountryService : IDisposable
    {
        void Save(Country country, bool isNew = true);
        void Delete(int id);

        List<Country> GetCountries(int? organisationId = null);
    }
}
