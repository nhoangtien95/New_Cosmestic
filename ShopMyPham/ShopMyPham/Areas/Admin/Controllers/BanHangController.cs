using ShopMyPham.Areas.Admin.DAO;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class BanHangController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Admin/BanHang

            
        public ActionResult ListHoaDon()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            DonHangDAO dao = new DonHangDAO();
            var list = dao.GetListHoaDon();
            return View(list);
        }

        public ActionResult uncheckOut(int id)
        {
            var bill = db.DonHangs.Single(x => x.DonHangID == id);
            bill.Tinhtrang = false;

            db.SaveChanges();
            return RedirectToAction("ListHoaDon");
        }
        public ActionResult ListKhuyenMai()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            return View();
        }

        
        public ActionResult ListDonDat()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            DonHangDAO dao = new DonHangDAO();
            var list = dao.GetListDonDat();
            return View(list);
        }

        
        public ActionResult checkOut(int id)
        {
            var bill = db.DonHangs.Single(x => x.DonHangID == id);
            bill.Tinhtrang = true;

            db.SaveChanges();
            return RedirectToAction("ListDonDat");
        }

        [HttpPost]
        public ActionResult deleteBill(int id)
        {
            var bill = db.DonHangs.Single(x => x.DonHangID == id);

            db.DonHangs.DeleteObject(bill);
            db.SaveChanges();
            return RedirectToAction("ListDonDat");
        }
    }
}