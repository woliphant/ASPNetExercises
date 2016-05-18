using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
namespace ASPNetExercises.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Message = HttpContext.Session.GetString("Message");
            return View();
        }
    }
}