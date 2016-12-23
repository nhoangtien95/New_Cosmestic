using ShopMyPham.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        public ActionResult ListKhachHang()
        {
            KhachHangDAO dao = new KhachHangDAO();
            var list = dao.getList();
            return View(list);
        }
    }
}