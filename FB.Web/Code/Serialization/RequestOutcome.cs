using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FB.Web
{
    public class RequestOutcome<T>
    {
        public RequestOutcome()
        {
            IsSuccess = true;
        }

        public T Data { get; set; }
        public string RedirectUrl { get; set; }
        public bool IsSuccess { get; set; }
    }
}
