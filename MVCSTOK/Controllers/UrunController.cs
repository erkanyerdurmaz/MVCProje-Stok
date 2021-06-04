using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATAGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATAGORIAD,
                                                 Value = i.KATAGORIID.ToString()

                                             }


                                           ).ToList();
            ViewBag.dgr = degerler;

            return View();
        }
              [HttpPost]
        public ActionResult UrunEkle(TBLURUNLER p1)
        {
            var ktg = db.TBLKATAGORILER.Where(m => m.KATAGORIID == p1.TBLKATAGORILER.KATAGORIID).FirstOrDefault();
                p1.TBLKATAGORILER = ktg;

            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {

            var urun = db.TBLURUNLER.Find(id);

            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATAGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATAGORIAD,
                                                 Value = i.KATAGORIID.ToString()

                                             }


                                          ).ToList();
            ViewBag.dgr = degerler;

            
            return View("UrunGetir", urun);


        }
        public ActionResult Guncelle(TBLURUNLER p)
        {
            var urun = db.TBLURUNLER.Find(p.UrunID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            urun.STOK = p.STOK;
            urun.FIYAT = p.FIYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATAGORILER.Where(m => m.KATAGORIID == p.TBLKATAGORILER.KATAGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATAGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public ActionResult SaveImages()
        {

            return View();

        }
        [HttpPost]
        public ActionResult SaveImages(string hiddenId,HttpPostedFileBase UploadedImage)
        {
            if(UploadedImage.ContentLength>0)
            {
                string ImageFilename = hiddenId + ".jpg";
                string FolderPath = Path.Combine(Server.MapPath("~/SaveImages"), ImageFilename);
                UploadedImage.SaveAs(FolderPath);
            }

            ViewBag.Message = hiddenId + ".jpg isimli resim başarrıyla yüklendi.";
            return View();

        }
    }
    }

