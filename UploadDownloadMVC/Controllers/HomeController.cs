using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UploadDownloadMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Upload() {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file) {
            try {
                if (file.ContentLength > 0) {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }

            } catch (Exception e) {
                    ViewBag.Message = "Upload failed!"; 
                    //return RedirectToAction("Upload");
            }

            return View();
        }
    }
}