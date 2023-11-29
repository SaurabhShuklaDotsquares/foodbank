using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
    public interface IContactService : IDisposable
    {
        Fbcontact GetContactById(int id);
        public void Save(Fbcontact address, bool isNew = true);
    }
}
