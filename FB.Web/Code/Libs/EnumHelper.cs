using Microsoft.AspNetCore.Mvc.Rendering;
using FB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.Code.Libs
{
    public static class EnumHelper
    {
        public static SelectList SelectListFor<T>(bool replaceUnderScore = false) where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t, replaceUnderScore), "Value", "Text");
        }

        public static SelectList SelectListFor<T>(T selected) where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null
                             : new SelectList(BuildSelectListItems(t), "Value", "Text", Convert.ToByte(selected).ToString());
        }

        private static IEnumerable<SelectListItem> BuildSelectListItems(Type t, bool replaceUnderScore = false)
        {
            return Enum.GetValues(t)
                       .Cast<Enum>()
                       .Select(e => new SelectListItem { Value = Convert.ToByte(e).ToString(), Text = replaceUnderScore ? e.GetDescription().Replace("_", " ") : e.GetDescription() });
        }
    }
}
