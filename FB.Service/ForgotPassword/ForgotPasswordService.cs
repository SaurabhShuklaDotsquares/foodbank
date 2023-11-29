using FB.Data.Models;
using FB.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Service
{
  public class ForgotPasswordService : IForgotPasswordService
    {
        private IRepository<ForgotPassword> repoForgotPassword;

        public ForgotPasswordService(IRepository<ForgotPassword> _repoForgotPassword)
        {
            this.repoForgotPassword = _repoForgotPassword;
        }

        /// <summary>
        /// To save the forgot password
        /// </summary>
        /// <param name="envelope"></param>
        /// <param name="isNew"></param>
        public void Save(ForgotPassword entity, bool isNew = true)
        {
            if (isNew)
            {
                repoForgotPassword.Insert(entity);
            }
            else
            {
                repoForgotPassword.Update(entity);
            }
        }

        /// <summary>
        /// To get single entity of forgot password
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ForgotPassword GetForgotPassword(string Guid)
        {
            return repoForgotPassword.Query().Filter(x => x.Guid == Guid && !x.IsExpire).Include(u=>u.User).Get().FirstOrDefault();
        }

        public void Dispose()
        {
            if (repoForgotPassword != null)
            {
                repoForgotPassword.Dispose();
                repoForgotPassword = null;
            }
        }
    }
}
