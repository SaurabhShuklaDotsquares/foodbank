using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class ResponsePdf
    {
        public bool IsSuccess { get; set; }
        public string FilePath { get; set; }
        public string ErrorMessage { get; set; }
    }
}
