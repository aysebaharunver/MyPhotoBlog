using MyPhotoBlog.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPhotoBlog.Controllers
{

    // Localization / Globalization
    public class HomeController : Controller
    {
        ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var categories = _context.Categories
                .Include("Photos")//Photo tablosunu joinledim
                .Where(c => c.IsDeleted == false && c.Photos.Count > 0)//filtreledim
                .ToList();

            return View(categories);
        }

        public ActionResult About()
        {

            


            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult GetByCategory(int id = 0)
        {
            if (id > 0)
            {
                var filtered = _context.Photos.Include("Category")
                    .Where(p => p.CategoryId == id && !p.IsDeleted)
                    .ToList();

                if (filtered.Count>0) return View(filtered);
            }
            return RedirectToAction("Index");
        }


      
    }
}