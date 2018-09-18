using System.Web.Mvc;

namespace FlsTaleQuiz.Controllers.Home
{
    public class HomeController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}