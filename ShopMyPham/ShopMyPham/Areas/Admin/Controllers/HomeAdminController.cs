using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Profile()
        {
            var user = Session["user"] as ShopMyPham.Models.QuanTri;
            var ac = db.QuanTris.Where(x => x.ID == user.ID).FirstOrDefault();
            ViewBag.user = ac;
            return View();
        }

        
        public ActionResult adminEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult adminEdit(AdminEditModel model)
        {
                var ac = db.QuanTris.Where(x => x.ID == model.id).FirstOrDefault();
                ac.Sdt = model.Sdt;
                ac.DiaChi = model.DiaChi;
                ac.Email = model.Email;

                db.ObjectStateManager.ChangeObjectState(ac, System.Data.Entity.EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Profile");
            
        }
    }
}