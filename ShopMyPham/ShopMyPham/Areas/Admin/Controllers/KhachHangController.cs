using ShopMyPham.Areas.Admin.DAO;
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
            KhachHangDAO dao = new KhachHangDAO();
            var list = dao.getList();

            return View(list);
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
    }
}