using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public abstract class BaseViewPage<TModel> : RazorPage<TModel>
    {
        protected CustomPrincipal CurrentUser => new CustomPrincipal(ContextProvider.HttpContext.User);

    }
}
