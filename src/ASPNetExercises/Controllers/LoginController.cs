﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Http;
using ASPNetExercises.Models;
using ASPNetExercises.ViewModels;
using ASPNetExercises.Utils;
namespace ASPNetExercises.Controllers
{
    public class LoginController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInMgr;
        public LoginController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = userManager;
            _signInMgr = signInManager;
        }
        // GET: Login
        public ActionResult Index()
        {
            if (HttpContext.Session.Get(SessionVars.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            }
            if (Convert.ToString(HttpContext.Session.Get(SessionVars.LoginStatus)) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVars.Message, "most functionality requires you to login!");
            }
            ViewBag.Status = HttpContext.Session.GetString(SessionVars.LoginStatus);
            ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            ViewBag.ReturnUrl = "/Home";
            return View();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/Home");
            }
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInMgr.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString(SessionVars.User, model.Email);
                    HttpContext.Session.SetString(SessionVars.LoginStatus, "logged in as " + model.Email);
                    HttpContext.Session.SetString(SessionVars.Message, "Welcome " + model.Email);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    HttpContext.Session.SetString(SessionVars.Message, "login attempt failed");
                    ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
                    return View("Index");
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToLocal(returnUrl);
        }

        // Account Logoff
        public async Task<IActionResult> LogOff(string returnUrl = null)
        {
            await _signInMgr.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Session.SetString(SessionVars.LoginStatus, "not logged in");
            return Redirect("/Home");
        }
    }
}