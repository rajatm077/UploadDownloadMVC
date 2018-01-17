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
        static string uploadmessage = String.Empty;

        public ActionResult Index() {
            List<string> files = Downloads();

            if (uploadmessage.Length > 0) {
                ViewBag.UploadMessage = uploadmessage;
            }

            return View(files);
        }

        [HttpPost]
        public ActionResult Upload(List<HttpPostedFileBase> files) {
            uploadmessage = string.Empty;
            bool success = true;

            foreach (var file in files) {
                try {
                    if (file.ContentLength > 0) {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                        file.SaveAs(path);
                    }

                } catch {                    
                    success = false;

                } finally {
                    if (success) {
                        uploadmessage = "File uploaded successfully";
                    } else {
                        uploadmessage = "Upload failed!";
                    }
                }
            }

            return RedirectToAction("Index");
             
        }


        public List<string> Downloads() {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/App_Data/Uploads"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
            List<string> files = new List<string>();
            foreach (var file in fileNames) {
                files.Add(file.Name);
            }
            return files;
        }

        public FileResult DownloadFile(string fileName) {
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Rtf, fileName);
        }
    }
}