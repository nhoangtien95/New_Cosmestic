using ShopMyPham.Areas.Admin.DAO;
using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();

       
        public ActionResult ListUser()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            NhanVienDAO db = new NhanVienDAO();
            var list = db.getList();

            return View(list);
        }
        public ActionResult AddUser(NhanVienModel nhanVien)
        {
            NhanVienDAO db = new NhanVienDAO();
            db.AddNhanVien(nhanVien);
            ModelState.AddModelError("", "Tạo Thành Công");
            return RedirectToAction("ListUser", "User");
        }
    }
}