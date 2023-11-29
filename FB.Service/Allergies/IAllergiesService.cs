using FB.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using FB.Data.Models;
using FB.Dto;
using FB.Core;

namespace FB.Service
{
    public interface IAllergiesService : IDisposable
    {
        List<Allergies> GetAllergies();
    }
}
