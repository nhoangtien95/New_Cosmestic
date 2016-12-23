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
    public class KhachHangController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Admin/KhachHang
        public ActionResult ListKhachHang()
        {
            //KhachHangDAO dao = new KhachHangDAO();
            //var list = dao.getList();

            ViewBag.user = db.QuanTris.Where(x => x.Level == 1).ToList();
            return View();
        }

        [HttpPost]
        public JsonResult statusChange(int id)
        {
            var user = db.QuanTris.Single(x => x.ID == id);
            if (user.TrangThai == true)
            {
                user.TrangThai = false;
            }
            else if (user.TrangThai == false)
            {
                user.TrangThai = true;
            }

            db.SaveChanges();
            return Json(new
            {
                status = user.TrangThai
            });
        }

        [HttpPost]
        public ActionResult customerEdit(CustomerEditModel model)
        {
            var user = db.QuanTris.Single(x => x.ID == model.id);
            user.Password = model.pass;
            user.DiaChi = model.diachi;
            user.Sdt = model.Sdt;
            user.Email = model.email;

            db.ObjectStateManager.ChangeObjectState(user, System.Data.Entity.EntityState.Modified);
            db.SaveChanges();

            return RedirectToAction("ListKhachHang");
        }

        [HttpPost]
        public ActionResult customerDelete(int id)
        {
            var user = db.QuanTris.Single(x => x.ID == id);
            db.QuanTris.DeleteObject(user);
            db.SaveChanges();

            return RedirectToAction("ListKhachHang");
        }

        public ActionResult addCustomer(NhanVienModel customer)
        {
            var user = new QuanTri()
            {
                Username = customer.Username,
                Password = customer.Password,
                Ten = customer.Ten,
                DiaChi = customer.DiaChi,
                Sdt = customer.Sdt,
                Email = customer.Email,
                TrangThai = true,
                Level = 1
            };
            db.QuanTris.AddObject(user);
            db.SaveChanges();

            return RedirectToAction("ListKhachHang");
        }
    }
}