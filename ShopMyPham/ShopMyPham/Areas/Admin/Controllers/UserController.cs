using ShopMyPham.Areas.Admin.DAO;
using ShopMyPham.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        public ActionResult ListUser()
        {
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