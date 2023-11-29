using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Repo
{
    public class SPRequestOutcome
    {
        public SPRequestOutcome()
        {
            IsSuccess = true;
        }

        public object Data { get; set; }
        public bool IsSuccess { get; set; }
    }
}
