using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class UserController : Controller
    {
        DbCalls db = new DbCalls();
        // GET: Dashboard
        public JsonResult UserDeatile()
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var currentUser = (from k in db.User.Where(u => u.UserID == User.UserID).ToList()
                                   select new
                                   {
                                       k.UserID,
                                       k.name,
                                       k.email
                                   }).FirstOrDefault();
                var currentUserProfile = (from k in db.UserMedia.Where(u => u.UserID == User.UserID && u.type==1).ToList()
                select new
                {
                    k.ImageUrl
                }).LastOrDefault();

                int numberOffriend = db.Friend.Where(u => u.UserID1 == User.UserID || u.UserID2 == User.UserID).ToList().Count;
                int numberOfPendingReq = db.FriendRequest.Where(u => u.toReq== User.UserID).ToList().Count;
                int totalFollowers = numberOffriend + numberOfPendingReq;


                code = 200;
                Message = "User Detail available";
                return Json(new { code, Message, currentUser, currentUserProfile, totalFollowers }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 401;
                Message = "login first";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UserDeatileForOther()
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                if (Session["ViewdUser"] != null)
                {
                    Int64 userID = Convert.ToInt64(Session["ViewdUser"]);
                    var currentUser = (from k in db.User.Where(u => u.UserID == userID).ToList()
                                       select new
                                       {
                                           k.UserID,
                                           k.name,
                                           k.email
                                       }).FirstOrDefault();
                    var currentUserProfile = (from k in db.UserMedia.Where(u => u.UserID == userID && u.type == 1).ToList()
                                              select new
                                              {
                                                  k.ImageUrl
                                              }).LastOrDefault();

                    int numberOffriend = db.Friend.Where(u => u.UserID1 == userID || u.UserID2 == userID).ToList().Count;
                    int numberOfPendingReq = db.FriendRequest.Where(u => u.toReq == userID).ToList().Count;
                    int totalFollowers = numberOffriend + numberOfPendingReq;


                    code = 200;
                    Message = "User Detail available";
                    return Json(new { code, Message, currentUser, currentUserProfile, totalFollowers }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    code = 400;
                    Message = "Error";
                    return Json(new { code, Message}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                code = 401;
                Message = "login first";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult viewUser(int UserID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                if (User.UserID == UserID)
                {
                    code = 200;
                    Message = "Same User";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["ViewdUser"] = UserID;
                    code = 200;
                    Message = "User Set";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                code = 401;
                Message = "login first";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}