using ShopMyPham.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class KhoController : Controller
    {
        // GET: Admin/Kho
        public ActionResult ListSanPham()
        {
            SanPhamDAO dao = new SanPhamDAO();
            var list = dao.GetAllList();
            return View(list);
        }

        public ActionResult ListLoai()
        {
            LoaiDAO dao = new LoaiDAO();
            var list = dao.GetList();
            return View(list);
        }
        public ActionResult ListNSX()
        {
            ThuongHieuDAO dao = new ThuongHieuDAO();
            var list = dao.GetList();
            return View(list);
        }

    }
}