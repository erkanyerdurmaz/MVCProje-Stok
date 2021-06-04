using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCSTOK.Models.Entity;
namespace MVCSTOK.Controllers
{
    public class GuvenlikController : Controller
    {
        // GET: Guvenlik
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLMUSTERILER m)
        {
            var bilgiler = db.TBLMUSTERILER.FirstOrDefault(x => x.MUSTERIAD == m.MUSTERIAD && x.MUSTERISOYAD == m.MUSTERISOYAD);
            if(bilgiler !=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MUSTERIAD, false);
                return RedirectToAction("Index", "Kategori");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult CıkısYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Guvenlik");
        }

        }
}