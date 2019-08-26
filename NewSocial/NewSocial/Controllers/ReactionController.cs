using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewSocial.Controllers
{
    public class ReactionController : Controller
    {
        DbCalls db = new DbCalls();
        public JsonResult addReaction(Reaction currentReaction)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && (currentReaction.reactionType==1 || currentReaction.reactionType==0))
            {
                var User = (Models.User)Session["ApplicationUser"];
                var isReactionExist=db.Reaction.Where(c => c.UserID == User.UserID && c.PostID == currentReaction.PostID).FirstOrDefault();
                if (isReactionExist == null)
                {
                    currentReaction.reactionTime = DateTime.Now;
                    currentReaction.UserID = User.UserID;
                    db.Reaction.Add(currentReaction);
                    db.SaveChanges();
                }
                else if (isReactionExist != null && currentReaction.PostID == isReactionExist.PostID && currentReaction.UserID == isReactionExist.UserID && currentReaction.reactionType == isReactionExist.reactionType)
                {
                    db.Reaction.RemoveRange(db.Reaction.Where(c => c.UserID == User.UserID && c.PostID == currentReaction.PostID));
                    db.SaveChanges();

                }
                else
                {
                    db.Reaction.RemoveRange(db.Reaction.Where(c => c.UserID == User.UserID && c.PostID == currentReaction.PostID));
                    db.SaveChanges();
                    currentReaction.reactionTime = DateTime.Now;
                    currentReaction.UserID = User.UserID;
                    db.Reaction.Add(currentReaction);
                    db.SaveChanges();
                }
                code = 200;
                Message = "Reaction Successfully added";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 400;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult addSubReaction(SubReaction currentReaction)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null && (currentReaction.reactionType == 1 || currentReaction.reactionType == 0))
            {
                var User = (Models.User)Session["ApplicationUser"];
                var isReactionExist = db.SubReaction.Where(c => c.UserID == User.UserID && c.CommentID == currentReaction.CommentID).FirstOrDefault();
                if (isReactionExist == null)
                {
                    currentReaction.reactionTime = DateTime.Now;
                    currentReaction.UserID = User.UserID;
                    db.SubReaction.Add(currentReaction);
                    db.SaveChanges();
                }
                else if (isReactionExist != null && currentReaction.CommentID == isReactionExist.CommentID && User.UserID == isReactionExist.UserID && currentReaction.reactionType == isReactionExist.reactionType)
                {
                    db.SubReaction.RemoveRange(db.SubReaction.Where(c => c.UserID == User.UserID && c.CommentID == currentReaction.CommentID));
                    db.SaveChanges();

                }
                else
                {
                    db.SubReaction.RemoveRange(db.SubReaction.Where(c => c.UserID == User.UserID && c.CommentID == currentReaction.CommentID));
                    db.SaveChanges();
                    currentReaction.reactionTime = DateTime.Now;
                    currentReaction.UserID = User.UserID;
                    db.SubReaction.Add(currentReaction);
                    db.SaveChanges();
                }
                code = 200;
                Message = "Sub Reaction Successfully added";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 400;
                Message = "login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}