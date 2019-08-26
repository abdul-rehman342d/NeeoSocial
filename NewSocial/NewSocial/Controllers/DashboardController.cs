using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult home()
        {
            return View();
        }
        public ActionResult profile()
        {
            return View();
        }
        public ActionResult otherUser()
        {
            return View();
        }
        public ActionResult FriendRequests()
        {
            return View();
        }
        public ActionResult Friends()
        {
            return View();
        }
        public ActionResult FriendOfOtherUser()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AboutOfOther()
        {
            return View();
        }

    }
}