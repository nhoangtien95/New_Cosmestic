using ShopMyPham.Areas.Admin.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;
using ShopMyPham.Areas.Admin.Models;
using System.IO;

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

            foreach (var item in th)
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
                sanPham.IDThuongHieu = db.ThuongHieux.Single(x => x.ID == result.IDThuongHieu).ID;
                sanPham.ThuongHieu = db.ThuongHieux.Single(x => x.ID == result.IDThuongHieu).TenTH;
                sanPham.IDLoai = db.Loais.Single(x => x.ID == result.IDLoai).ID;
                sanPham.Loai = db.Loais.Single(x => x.ID == result.IDLoai).Ten;
                sanPham.SoLanXem = result.SoLanXem;
                sanPham.NgayThem = result.NgayThem;
                sanPham.IDKhuyenMai = db.KhuyenMais.Single(x => x.KhuyenMaiID == result.IDKhuyenMai).KhuyenMaiID;
                sanPham.KhuyenMai = db.KhuyenMais.Single(x => x.KhuyenMaiID == result.IDKhuyenMai).Ten;
                sanPham.SEO = result.SEO;
                sanPham.SanPhamHinhs = result.SanPhamHinhs;
                list.Add(sanPham);
            }
            ViewBag.list = list;


            return View();
        }

        [HttpPost]
        public ActionResult addSanPham(ProductModel model, IEnumerable<HttpPostedFileBase> files)
        {
            var sp = new SanPham()
            {
                MaSanPham = model.MaSanPham,
                TenSanPham = model.TenSanPham,
                GiaBan = model.GiaBan,
                Mota = model.Mota,
                IDThuongHieu = model.IDThuongHieu,
                IDLoai = model.IDLoai,
                IDKhuyenMai = model.IDKhuyenMai,
                TrangThai = 1,
                SoLanXem = 0,
                NgayThem = DateTime.UtcNow,
                SEO = model.SEO
            };
            db.SanPhams.AddObject(sp);
            db.SaveChanges();

            //
            var ktHinh = new[] { ".png", ".jpg", ".jpeg" };
            foreach (var item in files)
            {
                var fileName = Path.GetFileName(item.FileName);
                var ext = Path.GetExtension(item.FileName);

                if (ktHinh.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string productImage = name + ext;
                    var maSP = db.SanPhams.Single(x => x.MaSanPham == model.MaSanPham).SanPhamID;
                    byte count = 1;

                    var img = new SanPhamHinh()
                    {
                        TenHinh = name + ext,
                        NgayThem = DateTime.UtcNow,
                        SoLanXem = 0,
                        SanPhamID = maSP,
                        ThuTuHienThi = count                        
                    };
                    db.SanPhamHinhs.AddObject(img);
                    count++;

                    db.SaveChanges();

                    item.SaveAs(Server.MapPath("~/Photos/SP/" + productImage));
                }
            }
            

            return RedirectToAction("ListSanPham");
        }

        [HttpPost]
        public ActionResult editSanPham(ProductModel model, IEnumerable<HttpPostedFileBase> files)
        {
            var sp = db.SanPhams.Single(x => x.SanPhamID == model.SanPhamID);
            sp.MaSanPham = model.MaSanPham;
            sp.TenSanPham = model.TenSanPham;
            sp.GiaBan = model.GiaBan;
            sp.Mota = model.Mota;
            sp.IDThuongHieu = model.IDThuongHieu;
            sp.IDLoai = model.IDLoai;
            sp.IDKhuyenMai = model.IDKhuyenMai;
            sp.SEO = model.SEO;

            db.ObjectStateManager.ChangeObjectState(sp, System.Data.Entity.EntityState.Modified);
            db.SaveChanges();
                        //    

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