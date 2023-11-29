using FB.Data.Models;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public class ContactService : IContactService
    {
        private IRepository<Fbcontact> repoFbcontact;
        public ContactService(IRepository<Fbcontact> _repoFbcontact)
        {
            this.repoFbcontact = _repoFbcontact;
        }

        public Fbcontact GetContactById(int id)
        {
            return repoFbcontact.FindById(id);
        }
        public void Dispose()
        {
            if (repoFbcontact != null)
            {
                repoFbcontact.Dispose();
                repoFbcontact = null;
            }
        }

        public void Save(Fbcontact address, bool isNew = true)
        {
            if (isNew)
            {
                repoFbcontact.Insert(address);
            }
            else
            {
                repoFbcontact.Update(address);
            }
        }
    }
}
