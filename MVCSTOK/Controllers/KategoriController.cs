using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MVCSTOK.Controllers



{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLKATAGORILER.ToList();
            var degerler = db.TBLKATAGORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori ()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult YeniKategori(TBLKATAGORILER p1)
        {
            if(!ModelState.IsValid)
            {

                return View("YeniKategori");
            }
            db.TBLKATAGORILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL (int id)
        {

            var katagori = db.TBLKATAGORILER.Find(id);

            db.TBLKATAGORILER.Remove(katagori);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KatagoriGetir(int id)
        {

            var ktgr = db.TBLKATAGORILER.Find(id);
            return View("KatagoriGetir", ktgr);

        }
        public ActionResult Guncelle(TBLKATAGORILER p1)
        {
            var ktg = db.TBLKATAGORILER.Find(p1.KATAGORIID);
            ktg.KATAGORIAD = p1.KATAGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}