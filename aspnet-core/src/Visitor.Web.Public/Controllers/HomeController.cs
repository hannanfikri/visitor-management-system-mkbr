using Microsoft.AspNetCore.Mvc;
using Visitor.Web.Controllers;

namespace Visitor.Web.Public.Controllers
{
    public class HomeController : VisitorControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}