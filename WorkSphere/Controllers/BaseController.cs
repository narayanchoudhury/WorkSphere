using System.Web.Mvc;

namespace WorkSphere.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserRole"] == null)
            {
                filterContext.Result = new RedirectResult("~/Auth/Login");
            }
        }
    }
}
