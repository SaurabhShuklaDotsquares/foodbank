using FB.Core;
using FB.Data.Models;
using FB.Dto;
using FB.ModalMapper;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FB.Service
{
    public class AllergiesService : IAllergiesService
    {
        private IRepository<Allergies> repoAllergies;

        public AllergiesService(IRepository<Allergies> _repoAllergies)
        { 
            this.repoAllergies = _repoAllergies;
           
        }
        public List<Allergies> GetAllergies()
        {
            return repoAllergies.Query().Get().ToList();

        }




        public void Dispose()
        {
            if (repoAllergies != null)
            {
                repoAllergies.Dispose();
                repoAllergies = null;
            }
            
            
        }

        
    }
}
