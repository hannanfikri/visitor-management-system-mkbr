using Microsoft.AspNetCore.Mvc;
using Visitor.Web.Controllers;

namespace Visitor.Web.Public.Controllers
{
    public class AboutController : VisitorControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}