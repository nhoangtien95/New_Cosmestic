using ShopMyPham.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class BanHangController : Controller
    {
        // GET: Admin/BanHang
        public ActionResult ListHoaDon()
        {
            DonHangDAO dao = new DonHangDAO();
            var list = dao.GetListHoaDon();
            return View(list);
        }
        public ActionResult ListKhuyenMai()
        {
            return View();
        }
        public ActionResult ListDonDat()
        {
            DonHangDAO dao = new DonHangDAO();
            var list = dao.GetListDonDat();
            return View(list);
        }
    }
}