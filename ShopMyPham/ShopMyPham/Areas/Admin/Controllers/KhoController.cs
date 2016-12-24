using ShopMyPham.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;
using ShopMyPham.Areas.Admin.Models;

namespace ShopMyPham.Areas.Admin.Controllers
{
    public class KhoController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Admin/Kho

        public List<ProductModel> getThuongHieu()
        {
            var th = db.ThuongHieux.Where(x => x.TrangThai == 1).ToList();
            var categories = new List<ProductModel>
            {                
                new ProductModel {IDThuongHieu = 0, ThuongHieu = "All"}                
            };

            foreach(var item in th)
            {
                categories.Add(new ProductModel()
                {
                    IDThuongHieu = item.ID,
                    ThuongHieu = item.TenTH
                });
            }


            return categories;
        }
        public List<ProductModel> getLoai()
        {
            var loai = db.Loais.ToList();
            var categories = new List<ProductModel>
            {
                new ProductModel {IDLoai = 0, Loai = "All"}
            };

            foreach (var item in loai)
            {
                categories.Add(new ProductModel()
                {
                    IDLoai = item.ID,
                    Loai = item.Ten
                });
            }


            return categories;
        }
        public List<ProductModel> getKhuyenMai()
        {
            var th = db.KhuyenMais.ToList();
            var categories = new List<ProductModel>
            {
                new ProductModel {IDKhuyenMai = 0, KhuyenMai = "All"}
            };

            foreach (var item in th)
            {
                categories.Add(new ProductModel()
                {
                    IDKhuyenMai = item.KhuyenMaiID,
                    KhuyenMai = item.Ten
                });
            }


            return categories;
        }
        public ActionResult ListSanPham()
        {
            //SanPhamDAO dao = new SanPhamDAO();
            //var list = dao.GetAllList();  
            ViewBag.th = new SelectList(getThuongHieu(), "IDThuongHieu", "ThuongHieu");
            ViewBag.loai = new SelectList(getLoai(), "IDLoai", "Loai");
            ViewBag.km = new SelectList(getKhuyenMai(), "IDKhuyenMai", "KhuyenMai");



            SanPhamModel sanPham = null;
            List<SanPhamModel> list = new List<SanPhamModel>();
            var products = db.SanPhams.Include("SanPhamHinhs").ToList();
            foreach (SanPham result in products)
            {
                sanPham = new SanPhamModel();
                sanPham.SanPhamID = result.SanPhamID;
                sanPham.MaSanPham = result.MaSanPham;
                sanPham.TenSanPham = result.TenSanPham;
                sanPham.GiaBan = result.GiaBan;
                sanPham.TrangThai = result.TrangThai;
                sanPham.Mota = result.Mota;
                sanPham.ThuongHieu = db.ThuongHieux.Single(x => x.ID == result.IDThuongHieu).TenTH;
                sanPham.Loai =  db.Loais.Single(x => x.ID == result.IDLoai).Ten;
                sanPham.SoLanXem = result.SoLanXem;
                sanPham.NgayThem = result.NgayThem;
                sanPham.IDKhuyenMai = db.KhuyenMais.Single(x => x.KhuyenMaiID == result.IDKhuyenMai).Ten;
                sanPham.SEO = result.SEO;
                sanPham.SanPhamHinhs = result.SanPhamHinhs;
                list.Add(sanPham);
            }
            ViewBag.list = list;


            return View();
        }

        [HttpPost]
        public ActionResult addSanPham(ProductModel model)
        {
            return RedirectToAction("ListSanPham");
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