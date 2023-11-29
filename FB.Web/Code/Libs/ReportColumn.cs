using Microsoft.AspNetCore.Mvc.Rendering;
using FB.Core;
using FB.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class ReportColumn
    {
        #region Constructors
        public ReportColumn() { }

        public ReportColumn(ReportType reportType)
        {
            ReportType = reportType;
            //Columns = GetColumnsName();
            FileName = reportType.ToString();
        }
        #endregion

        #region Properties
        public ReportType ReportType { get; set; }
        public string SelectedColumn { get; set; }
        public List<SelectListItem> Columns { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        [DisplayName("Enter a Footer to appear at the bottom of every page")]
        public string FooterName { get; set; }
        public bool IsCreateList { get; set; }
        public string APiKey { get; set; }
        #endregion

        #region Methods
        //private List<SelectListItem> GetColumnsName()
        //{
        //    switch (ReportType)
        //    {
        //        case ReportType.SimpleListofPeople:
        //            return new ReportSimpleListofPeopleDto().GetType().GetProperties().Where(pi => pi.GetCustomAttributes(typeof(SkipPropertyAttribute), true).Length == 0).Select(a => new SelectListItem { Text = a.Name, Value = a.Name }).ToList();
        //        default:
        //            return new List<SelectListItem>();
        //    }
        //}
        #endregion
    }
}
