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
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            return View();
        }

        
        public ActionResult Profile()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            var user = Session["user"] as ShopMyPham.Models.QuanTri;
            var ac = db.QuanTris.Where(x => x.ID == user.ID).FirstOrDefault();
            ViewBag.user = ac;
            return View();
        }

        
        public ActionResult changePassword()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult changePassword(AdminEditModel model)
        {
            var user = db.QuanTris.Single(x => x.ID == model.id);

            if (user. Password == model.password)
            {
                if(model.newpassword == model.passwordComfirm)
                {
                    user.Password = model.passwordComfirm;

                    db.ObjectStateManager.ChangeObjectState(user, System.Data.Entity.EntityState.Modified);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không trùng khớp !!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Mật khẩu hiện tại không đúng !!");
                return View();
            }

            return RedirectToAction("Profile");
        }

        
        public ActionResult adminEdit()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
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

        public ActionResult logOut()
        {
            Session["user"] = null;

            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}