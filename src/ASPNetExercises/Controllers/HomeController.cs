﻿using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using ASPNetExercises.Utils;
namespace ASPNetExercises.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            }
            if (HttpContext.Session.GetString(SessionVars.LoginStatus) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVars.Message, "most functionality requires you to login!");
            }
            ViewBag.Status = HttpContext.Session.GetString(SessionVars.LoginStatus);
            ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            return View();
        }
    }
}