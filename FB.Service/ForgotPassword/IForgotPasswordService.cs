using FB.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Service
{
 public interface IForgotPasswordService: IDisposable
    {
        void Save(ForgotPassword entity, bool isNew = true);
        ForgotPassword GetForgotPassword(string Guid);   

    }
}
