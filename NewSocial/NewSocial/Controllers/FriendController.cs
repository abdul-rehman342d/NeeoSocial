using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class FriendController : Controller
    {
        DbCalls db = new DbCalls();
        // GET: Friend
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult addFriend(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && FriendID!=0)
            {
                var User = (Models.User)Session["ApplicationUser"];

                var check = db.FriendRequest.Where(c => c.fromReq == User.UserID && c.toReq == FriendID).FirstOrDefault();
                var confirmCheck = db.FriendRequest.Where(c => c.fromReq == FriendID && c.toReq == User.UserID).FirstOrDefault();
                var isAlreadyFriend = db.Friend.Where(c => (c.UserID1 == FriendID && c.UserID2 == User.UserID) || (c.UserID1 == User.UserID && c.UserID2 == FriendID)).FirstOrDefault();
                if (check != null)
                {
                    code = 200;
                    Message = "Request Already Sent";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                if (confirmCheck == null && isAlreadyFriend==null)
                {
                    FriendRequest friendReq = new FriendRequest();
                    friendReq.fromReq = User.UserID;
                    friendReq.toReq = FriendID;
                    db.FriendRequest.Add(friendReq);
                    db.SaveChanges();
                    code = 200;
                    Message = "Request sent";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    code = 400;
                    Message = "Unautherized Changing";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult cancelSentRequest(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && FriendID != 0)
            {
                var User = (Models.User)Session["ApplicationUser"];

                var checkOfReq = db.FriendRequest.Where(c => c.fromReq == User.UserID && c.toReq == FriendID).FirstOrDefault();
                if (checkOfReq != null)
                {
                    db.FriendRequest.Remove(checkOfReq);
                    db.SaveChanges();
                    code = 200;
                    Message = "Request Cancelled";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    code = 400;
                    Message = "Unauthorized changing";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult checkRequestStatus(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && FriendID != 0)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var check=db.FriendRequest.Where(c => c.fromReq == User.UserID && c.toReq == FriendID).FirstOrDefault();
                var Confirmcheck = db.FriendRequest.Where(c => c.fromReq == FriendID && c.toReq == User.UserID).FirstOrDefault();
                var isAlreadyFriend= db.Friend.Where(c => (c.UserID1 == FriendID && c.UserID2 == User.UserID) || (c.UserID1 == User.UserID && c.UserID2 == FriendID)).FirstOrDefault();
                if (check != null)
                {
                    code = 200;
                    Message = "Request Already Sent";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                if (Confirmcheck != null)
                {
                    code = 200;
                    Message = "Request Available";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                if (isAlreadyFriend != null)
                {
                    code = 200;
                    Message = "Already Friend";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }

                else
                {
                    code = 200;
                    Message = "Add friend";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult confirmFriend(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && FriendID != 0)
            {
                var User = (Models.User)Session["ApplicationUser"];

                var checkOfReq = db.FriendRequest.Where(c => c.fromReq == FriendID && c.toReq == User.UserID).FirstOrDefault();
                var isAlreadyFriend = db.Friend.Where(c => (c.UserID1 == FriendID && c.UserID2 == User.UserID) || (c.UserID1 == User.UserID   && c.UserID2 == FriendID)).FirstOrDefault();
                if (checkOfReq != null && isAlreadyFriend == null)
                {
                    Friend newFriend = new Friend();
                    newFriend.UserID1 = User.UserID;
                    newFriend.UserID2 = FriendID;
                    db.Friend.Add(newFriend);
                    db.SaveChanges();

                    db.FriendRequest.Remove(checkOfReq);
                    db.SaveChanges();
                    code = 200;
                    Message = "Friend added";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    code = 401;
                    Message = "Unautherized Changing";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult rejectRequest(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && FriendID != 0)
            {
                var User = (Models.User)Session["ApplicationUser"];

                var checkOfReq = db.FriendRequest.Where(c => c.fromReq == FriendID && c.toReq ==User.UserID).FirstOrDefault();
                if (checkOfReq != null)
                {
                    db.FriendRequest.Remove(checkOfReq);
                    db.SaveChanges();
                    code = 200;
                    Message = "Request Rejected";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    code = 401;
                    Message = "Unauthorized changing";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult pendingFriendRequests()
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var friendRequests = (from k in db.FriendRequest.Where(u => u.toReq == User.UserID).ToList()
                                      select new
                                      {
                                          k.FriendRequestID,
                                          User = (from j in db.User.Where(j => j.UserID == k.fromReq).ToList()
                                                  select new
                                                  {
                                                      j.UserID,
                                                      j.name,
                                                      userProfile = (from x in db.UserMedia.Where(u => u.UserID == j.UserID && u.type == 1).ToList()
                                                                     select new
                                                                     {
                                                                         x.ImageUrl
                                                                     }).LastOrDefault(),
                                                  }).ToList().FirstOrDefault(),
                                     }).ToList().OrderByDescending(u => u.FriendRequestID);
            code = 200;
                Message = "success";
                return Json(new { code, Message, friendRequests }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult currentUserFriendList()
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var friendList = (from k in db.Friend.Where(u => u.UserID1 == User.UserID || u.UserID2 == User.UserID).ToList()
                                      select new
                                      {
                                          User1 = (from j in db.User.Where(j => j.UserID == k.UserID1).ToList()
                                                  select new
                                                  {
                                                      j.UserID,
                                                      j.name,
                                                      userProfile = (from x in db.UserMedia.Where(u => u.UserID == j.UserID && u.type == 1).ToList()
                                                                     select new
                                                                     {
                                                                         x.ImageUrl
                                                                     }).LastOrDefault(),
                                                  }).ToList().FirstOrDefault(),
                                          User2 = (from j in db.User.Where(j => j.UserID == k.UserID2).ToList()
                                                   select new
                                                   {
                                                       j.UserID,
                                                       j.name,
                                                        userProfile = (from x in db.UserMedia.Where(u => u.UserID == j.UserID && u.type == 1).ToList()
                                                                       select new
                                                                       {
                                                                           x.ImageUrl
                                                                       }).LastOrDefault(),
                                                   }).ToList().FirstOrDefault(),


                                      }).ToList();
                code = 200;
                Message = "success";
                return Json(new { code, Message, friendList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult unfriend(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var friendForUnfriend = db.Friend.Where(u => (u.UserID1 == User.UserID && u.UserID2 == FriendID) || (u.UserID1 == FriendID && u.UserID2 == User.UserID)).FirstOrDefault();
                if (friendForUnfriend != null)
                {
                    db.Friend.Remove(friendForUnfriend);
                    db.SaveChanges();
                    code = 200;
                    Message = "success";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);

                }
                else {
                    code = 400;
                    Message = "Unauthorized changing";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult selectedUserFriendList(Int64 FriendID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                var friendList = (from k in db.Friend.Where(u => u.UserID1 == FriendID || u.UserID2 == FriendID).ToList()
                                  select new
                                  {
                                      User1 = (from j in db.User.Where(j => j.UserID == k.UserID1).ToList()
                                               select new
                                               {
                                                   j.UserID,
                                                   j.name,
                                                   userProfile = (from x in db.UserMedia.Where(u => u.UserID == j.UserID && u.type == 1).ToList()
                                                                  select new
                                                                  {
                                                                      x.ImageUrl
                                                                  }).LastOrDefault(),
                                               }).ToList().FirstOrDefault(),
                                      User2 = (from j in db.User.Where(j => j.UserID == k.UserID2).ToList()
                                               select new
                                               {
                                                   j.UserID,
                                                   j.name,
                                                   userProfile = (from x in db.UserMedia.Where(u => u.UserID == j.UserID && u.type == 1).ToList()
                                                                  select new
                                                                  {
                                                                      x.ImageUrl
                                                                  }).LastOrDefault(),
                                               }).ToList().FirstOrDefault(),


                                  }).ToList();
                code = 200;
                Message = "success";
                return Json(new { code, Message, friendList }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 401;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult viewUserFrnd(int UserID)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                
                    Session["ViewdUser"] = UserID;
                    code = 200;
                    Message = "User Set";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                
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