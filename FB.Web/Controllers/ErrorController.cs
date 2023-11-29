using FB.Web.Code;
using Microsoft.AspNetCore.Mvc;

namespace FB.Web.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_OperationFailed");
            }
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDeniedAjax()
        {
            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "accessDeniedAjax", IsSuccess = false });
        }

        [HttpGet]
        public IActionResult SessionOut()
        {
            return NewtonSoftJsonResult(new RequestOutcome<string> { Data = "SessionOut", IsSuccess = false });
        }
    }
}
