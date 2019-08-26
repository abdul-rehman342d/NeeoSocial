using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        DbCalls db = new DbCalls();
        public JsonResult addComment(int PostID,string commentText)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                if (commentText.Length <= 200)
                {
                    var User = (Models.User)Session["ApplicationUser"];
                    Comment currentComment = new Comment();
                    currentComment.PostID = Convert.ToInt64(PostID);
                    currentComment.UserID = User.UserID;
                    currentComment.commentText = commentText;
                    currentComment.commentTime = DateTime.Now;
                    db.Comment.Add(currentComment);
                    db.SaveChanges();
                    code = 200;
                    Message = "comment Successfully added";
                    return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    code = 400;
                    Message= "Unauthorized changing";
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

    }
}