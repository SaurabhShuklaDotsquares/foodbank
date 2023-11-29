using System;
using FB.Dto;
using FB.Data;
using FB.Data.Models;

namespace FB.Service
{
    public interface IUserPreferenceService:IDisposable
    {
        void Save(UserPreference userPreference, bool isNew = true);
        UserPreference GetUserPreferencebyUserId(int userID);
        void Delete(int id);
    }
}