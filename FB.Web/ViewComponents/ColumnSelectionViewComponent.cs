using Microsoft.AspNetCore.Mvc;
using FB.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web.ViewComponents
{
    public class ColumnSelectionViewComponent : ViewComponent
    {
        public ColumnSelectionViewComponent()
        {
        }

        public Task<IViewComponentResult> InvokeAsync(int id)
        {
            ReportColumn reportColumnModel = new ReportColumn((ReportType)id);
            return System.Threading.Tasks.Task.FromResult<IViewComponentResult>(View("~/Views/Report/_ColumnSelection.cshtml", reportColumnModel));
        }
    }
}
