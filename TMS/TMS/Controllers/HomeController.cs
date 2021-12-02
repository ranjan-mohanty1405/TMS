using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Models;
using System.Data;
using System.Data.Entity;
using TMS.Abstract;
using Newtonsoft.Json;
using System.Configuration;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationRepository applicationRepository;

        public HomeController(IApplicationRepository appRepository)
        {
            applicationRepository = appRepository;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var data = applicationRepository.CheckLogin(username, password);

                if (data != null && data.IsDeleted == false)
                {
                    Session["userid"] = data.UserId;
                    Session["Regid"] = data.RegistrationId;
                    Session["username"] = data.Username;
                    Session["usertype"] = data.UserTypeId;
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (data != null && data.IsDeleted == true)
                {
                    TempData["Message"] = "User does not exist";
                    return View();
                }
                else
                {
                    TempData["Message"] = "Username or password is incorrect";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Something went wrong";
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Logout()
        {
            return View();
        }
    }
}