using Microsoft.AspNet.Identity;
using MyPhotoBlog.Infra;
using MyPhotoBlog.Models;
using MyPhotoBlog.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoBlog.Controllers
{
    [Authorize(Roles = "admin")]
    // Yetkilendirilmiş kullanıcılar içinden sadece admin rolü olanlar girebilsin
    public class AdminController : Controller
    {
        string[] extensionList = { ".jpg", ".jpeg", ".png", ".bmp", ".svg", ".tiff", ".jfif" };



        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Photos()
        {
            var categories = _context.Categories.Include("Photos").ToList();
            return View(categories);
        }

        [HttpPost]
        public ActionResult AddOrUpdateCategory(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var category = new Category
                {
                    Name = name,
                    CreatedBy = User.Identity.GetUserId()
                };

                // _context.Categories.Add(category);

                _context.Entry(category).State = System.Data.Entity.EntityState.Added;
                _context.SaveChanges();
            }

            return RedirectToAction("Photos");
        }

        public ActionResult About()
        {
            return View();
        }


        #region Fotoğraf İşlemleri


        public ActionResult AddPhoto()
        {
            ViewBag.Categories = _context.Categories
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.Name)
                .ToList();

            return PartialView();
        }

        [HttpPost]
        public ActionResult AddPhoto(Photo photo, HttpPostedFileBase file)
        {
            //Dosya dolu mu? ve hatta dosya doluysa boyutu 1 den büyük mü

            if (file != null && file.ContentLength > 0)
            {

                // Dosyayı sunuda images klasörüne adını değiştirmeden doğrudan yükle
                //file.SaveAs(Server.MapPath("~/images/" + file.FileName));

                // Artık veritabanına bu kaydı işleyebilirim

                // dosya uzantısını yakala
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                // Eğer izinli lsitesinde yer alıyorsa işlemlere devam edilsin
                if (extensionList.Contains(fileExtension))
                {
                    photo.Path = User.Identity.GetUserId() + "/" + Toolkit.ImageUpload(file, User.Identity.GetUserId(), 1200, "jpeg");


                    #region db ye kayt için imajı byte aray e çevirme

                    Stream fileStream = file.InputStream;
                    var mStreamer = new MemoryStream();
                    mStreamer.SetLength(fileStream.Length);
                    fileStream.Read(mStreamer.GetBuffer(), 0, (int)fileStream.Length);
                    mStreamer.Seek(0, SeekOrigin.Begin);
                                                                      

                    // byte[] e dönüştürülmüş yapıyı propertyye ver
                    photo.Thumb= mStreamer.GetBuffer();

                    #endregion



                    photo.CreatedBy = User.Identity.GetUserId();
                    _context.Entry(photo).State = System.Data.Entity.EntityState.Added;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Home");
        }


        #endregion

       


    }
}