using System.Linq;
using FB.Repo;
using FB.Core;
using FB.Data;
using FB.Dto;
using FB.Data.Models;

namespace FB.Service
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private IRepository<UserPreference> repoUserPreference;

        public UserPreferenceService(IRepository<UserPreference> _repoUserPreference)
        {
            this.repoUserPreference = _repoUserPreference;
        }

        /// <summary>
        /// To save the user preferences
        /// </summary>
        /// <param name="userPreference"></param>
        /// <param name="isNew"></param>
        public void Save(UserPreference userPreference, bool isNew = true)
        {
            if (isNew)
            {
                repoUserPreference.InsertGraph(userPreference);
            }
            else
            {
                repoUserPreference.Update(userPreference);
            }
        }

        /// <summary>
        /// To get user preference
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserPreference GetUserPreferencebyUserId(int userID)
        {
            return repoUserPreference.Query().Filter(u => u.UserId == userID).Get().FirstOrDefault();
        }

        /// <summary>
        /// delete user preference
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            repoUserPreference.Delete(id);
        }

        public void Dispose()
        {
            if (repoUserPreference != null)
            {
                repoUserPreference.Dispose();
                repoUserPreference = null;
            }
        }
    }
}
