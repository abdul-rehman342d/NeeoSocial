using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class AuthenticationController : Controller
    {
        DbCalls db = new DbCalls();

        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        //Operations
        [HttpPost]
        public JsonResult UserRegisteration(string name, string email, string password, string dateOfBirth, int gender,string city,string country)
        {
            string Message;
            int code;
            if (name != "" && email != "" && password != "" && !(password.Length<=5) && dateOfBirth != "" && (gender!=1 || gender!=0) && city!="" && (country!="" && country.Length<=3  ) )
            {
                var emailExist = db.User.Where(u => u.email == email).FirstOrDefault();
                if (emailExist == null)
                {
                    User currentUser = new User();
                    currentUser.name = name;
                    currentUser.email = email;
                    currentUser.password = password;
                    currentUser.gender = gender;

                    var split = dateOfBirth.Split('-');
                    string date = split[0];
                    string Month = split[1];
                    string years = split[2];
                    dateOfBirth = years + "-" + Month + "-" + date;
                    currentUser.dateOfBirth = Convert.ToDateTime(dateOfBirth);
                    currentUser.city = city;
                    currentUser.country = country;
                    currentUser.isVerified = false;
                    db.User.Add(currentUser);
                    db.SaveChanges();
                    code = 200;
                    Message = "User registered";
                    Session["ApplicationUser"] = currentUser;
                    Session.Timeout = 525600;
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Message = "Email Already Exist";
                    code = 101;
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Message = "Unauthorized Changes";
                code = 400;
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult isUserValid(string email, string password)
        {
                string Message;
                int code;
                if (db.User.Any())
                {
                    User user = db.User.Where(u => u.email == email && u.password == password).FirstOrDefault();
                    if (user != null)
                    {
                        Session["ApplicationUser"] = user;
                        Message = "successfully logged in";
                        code = 200;
                        Session["ApplicationUser"] = user;
                        Session.Timeout = 525600;
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        code = 400;
                        Message = "Incorrect Email or Password";
                        return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    code = 400;
                    Message = "No User Exist";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
            
        }

        //Logout
        [HttpPost]
        public JsonResult logout()
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                code = 200;
                Message = "Successfully logged out";
                Session.Remove("ApplicationUser");
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 400;
                Message = "Login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}