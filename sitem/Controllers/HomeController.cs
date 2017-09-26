using System;
using System.Linq;
using System.Web.Mvc;
using sitem.Models;
using PagedList;
using PagedList.Mvc;



namespace sitem.Controllers
{
    public class HomeController : Controller
    {
        mvcblogDB db = new mvcblogDB();
        private object uyeid;


        // GET: home
        public ActionResult Index(int Page=1)
        {
            var makale = db.Makales.OrderByDescending(m => m.MakaleId).ToPagedList(Page,5);
           
            return View(makale);
        }
        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.Makales.Where(m => m.Baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m=>m.Tarih));
        }
        public ActionResult SonYorumlar()
        {
            return View(db.Yorums.OrderByDescending(y=>y.YorumId).Take(5));
        }
        public ActionResult PopulerMakaleler()
        {
            return View(db.Makales.OrderByDescending(m => m.Okunma).Take(5));
        }
        public ActionResult KategoriMakale(int id)
        {
            var makaleler = db.Makales.Where(m => m.Kategori.KategoriId == id).ToList();
            return View(makaleler);
        }
        public ActionResult makaledetay(int id)
        {
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (makale==null)
            {
                return HttpNotFound();
            }
            return View(makale);

        }
       
        public ActionResult Raporlar()
        {
            return View();
        }
        

        public ActionResult ILetisim()
        {
            return View();
        }
        public ActionResult KategoriPartial()
        {
            return View(db.Kategoris.ToList());
        }
        public JsonResult Yorumyap(string yorum, int Makaleid)
        {
            var kullanici = Session["uyeid"];
            if (yorum != null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            db.Yorums.Add(new Yorum { UyeId = Convert.ToInt32(uyeid), MakaleId = Makaleid, Icerik = yorum, Tarih = DateTime.Now });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
            public ActionResult YorumSil(int id)
        {
            var uyeid=Session["uyeid"];
            var yorum = db.Yorums.Where(y => y.YorumId == id).SingleOrDefault();
            var makale = db.Makales.Where(m => m.MakaleId == id).SingleOrDefault();
            if (yorum.UyeId==Convert.ToInt32(uyeid))
            {
                db.Yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay", "Home", new { id = makale.MakaleId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        public  ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.Makales.Where(m => m.MakaleId == Makaleid).SingleOrDefault();
            makale.Okunma += 1;
            db.SaveChanges();

            return View();
        }


    }
}