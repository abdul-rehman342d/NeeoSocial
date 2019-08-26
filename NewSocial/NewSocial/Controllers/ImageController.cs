using NewSocial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace NewSocial.Controllers
{
    public class ImageController : Controller
    {
        DbCalls db = new DbCalls();
        // GET: Iamge
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveProfile(string image)
        {
            string Message;
            int code;
            if (Session["ApplicationUser"] != null)
            {
                var User = (Models.User)Session["ApplicationUser"];
                code = 200;
                Message = "Saved";


                byte[] contents = convertIntoByte(image);
                string subpath = "~/images/userProfiles/";
                string fileName = User.UserID+"_Profile_" + Guid.NewGuid() + ".jpg";
                var uploadPath = HttpContext.Server.MapPath(subpath);
                var path = Path.Combine(uploadPath, Path.GetFileName(fileName));

                System.IO.File.WriteAllBytes(path, contents);



                UserMedia currentUerMedia = new UserMedia();
                currentUerMedia.UserID = User.UserID;
                currentUerMedia.type = 1;
                currentUerMedia.ImageUrl = "/images/userProfiles/" + fileName;
                db.UserMedia.Add(currentUerMedia);
                db.SaveChanges();
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                code = 401;
                Message = "Login First";
                return Json(new { code, Message }, JsonRequestBehavior.AllowGet);
            }

        }

        //public void UploadIamge(string img)
        //{
        //    byte[] contents = convertIntoByte(img);
        //    string subpath = "~/images/userProfiles";
        //    string fileName = "user" + Guid.NewGuid()+".jpg";
        //    var uploadPath = HttpContext.Server.MapPath(subpath);
        //    var path = Path.Combine(uploadPath, Path.GetFileName(fileName));
        //    System.IO.File.WriteAllBytes(path, contents);

        //}



        
        public class FileDetails
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }
       
        [HttpPost]

        public JsonResult UploadImages()
        {
            try
            {
                var httpContext = System.Web.HttpContext.Current;
                List<string> images = new List<string>();
                // Check for any uploaded file  
                if (httpContext.Request.Files.Count > 0)
                {
                    //Loop through uploaded files  
                    for (int i = 0; i < httpContext.Request.Files.Count; i++)
                    {
                        HttpPostedFile httpPostedFile = httpContext.Request.Files[i];
                        if (httpPostedFile != null /*&& httpPostedFile.ContentType==*/)
                        {
                            // Construct file save path
                            string newfileName = httpPostedFile.FileName.Remove(httpPostedFile.FileName.Length - 4, 4) + "_u_" + Guid.NewGuid() + ".jpg";
                            string subpath = "/images/userPosts/";
                            var uploadPath = HttpContext.Server.MapPath("~"+subpath);
                            var fileSavePath = Path.Combine(uploadPath, newfileName);
                            var returnpath = subpath + newfileName;
                            // Save the uploaded file  
                            httpPostedFile.SaveAs(fileSavePath);

                            FileDetails currentImage = new FileDetails();
                            currentImage.Name = httpPostedFile.FileName;
                            currentImage.Path = returnpath;
                            images.Add("../.."+returnpath);
                        }
                    }
                    return Json(new { HttpStatusCode.OK, images }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { HttpStatusCode.LengthRequired }, JsonRequestBehavior.AllowGet);
                
            }
            catch (ApplicationException applicationException)
            {
                return Json(new { HttpStatusCode.InternalServerError }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
               return Json(new { HttpStatusCode.InternalServerError}, JsonRequestBehavior.AllowGet);
                
            }
        }


        public byte[] convertIntoByte(string byte_array)
        {
            byte[] bytes = System.Convert.FromBase64String((byte_array.Split(',') as string[])[1]);
            return bytes;
        }

    }
}