using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sitem.Models;



namespace sitem.Controllers
{
    public class adminController : Controller
    {
        mvcblogDB db = new mvcblogDB();
        // GET: admin
        public ActionResult Index()
        {
            ViewBag.MakaleSayisi = db.Makales.Count();
            ViewBag.YorumSayisi = db.Yorums.Count();
            ViewBag.KategoriSayisi = db.Kategoris.Count();
            ViewBag.UyeSayisi = db.Uyes.Count();
            return View();
        }
    }
}