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
            var categories = new List<ProductModel>();

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
            var categories = new List<ProductModel>();

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
            var categories = new List<ProductModel>();

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
            if(Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
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
            var checkSP = db.SanPhams.ToList();
            foreach(var item in checkSP)
            {
                if ( model.MaSanPham == item.MaSanPham)
                {
                    ModelState.AddModelError("", "Mã sản phẩm đã tồn tại !");
                }
            }
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
            int count = 1;
            foreach (var item in files)
            {
                var fileName = Path.GetFileName(item.FileName);
                var ext = Path.GetExtension(item.FileName);

                if (ktHinh.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string productImage = name + ext;
                    var maSP = db.SanPhams.Single(x => x.MaSanPham == model.MaSanPham).SanPhamID;
                    

                    var img = new SanPhamHinh()
                    {
                        TenHinh = name + ext,
                        NgayThem = DateTime.UtcNow,
                        SoLanXem = 0,
                        SanPhamID = maSP,
                        ThuTuHienThi = Convert.ToByte(count)
                    };
                    db.SanPhamHinhs.AddObject(img);
                    

                    db.SaveChanges();

                    item.SaveAs(Server.MapPath("~/Photos/SP/" + productImage));
                    count++;
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

        [HttpPost]
        public ActionResult editSanPhamImg(ProductModel model, HttpPostedFileBase imgFile)
        {
            var ktHinh = new[] { ".png", ".jpg", ".jpeg" };
            var fileName = Path.GetFileName(imgFile.FileName);
            var ext = Path.GetExtension(imgFile.FileName);
            var img = db.SanPhamHinhs.Single(x => x.SanPhamHinhID == model.IDHinh);

            img.NgayThem = DateTime.UtcNow;
            img.SoLanXem = 0;

            if (ktHinh.Contains(ext))
            {
                string imgPath = Request.MapPath("~/Photos/SP/" + img.TenHinh);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }

                string name = Path.GetFileNameWithoutExtension(fileName);
                string productImage = name + ext;

                img.TenHinh = name + ext;
                imgFile.SaveAs(Server.MapPath("~/Photos/SP/" + productImage));

            }



            db.ObjectStateManager.ChangeObjectState(img, System.Data.Entity.EntityState.Modified);
            db.SaveChanges();
            //    

            return RedirectToAction("ListSanPham");
        }

        [HttpPost]
        public ActionResult deleteSanPham(int id)
        {
            var product = db.SanPhams.Single(x => x.SanPhamID == id);
            var productImg = db.SanPhamHinhs.Where(x => x.SanPhamID == id).ToList();

            foreach (var img in productImg)
            {
                string imgPath = Request.MapPath("~/Photos/SP/" + img.TenHinh);
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }

                db.SanPhamHinhs.DeleteObject(img);
            }

            db.SanPhams.DeleteObject(product);
            db.SaveChanges();

            return RedirectToAction("ListSanPham");
        }

        
        public ActionResult ListLoai()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            ViewBag.list = db.Loais.Where(x => x.ChungLoaiID == null).ToList();
            ViewBag.selectLoai = new SelectList(getChungLoaiID(), "chungloaiID", "tenLoai");

            return View();
        }

        public List<LoaiModel> getChungLoaiID()
        {
            var loai = db.Loais.Where(x => x.ChungLoaiID == null).ToList();
            var categories = new List<LoaiModel>();
            
            foreach (var item in loai)
            {
                categories.Add(new LoaiModel()
                {
                    chungloaiID = item.ID,
                    tenLoai = item.Ten
                });
            }


            return categories;
        }

        [HttpPost]
        public ActionResult addLoai(LoaiModel model)
        {
            var count = db.Loais.ToList();
            var loai = new Loai()
            {
                ID = count.Count() + 1,
                Ten = model.tenLoai,
                ChungLoaiID = model.chungloaiID,
                SEO = model.SEO
            };           

            db.Loais.AddObject(loai);
            db.SaveChanges();

            return RedirectToAction("ListLoai");
        }

        [HttpPost]
        public ActionResult editLoai(LoaiModel model)
        {            
            var loai = db.Loais.Single(x => x.ID == model.IDLoai);
            loai.Ten = model.tenLoai;
            loai.ChungLoaiID = model.chungloaiID;
            loai.SEO = model.SEO;
                        

            db.ObjectStateManager.ChangeObjectState(loai, System.Data.Entity.EntityState.Modified);
            db.SaveChanges();

            return RedirectToAction("ListLoai");
        }

        [HttpPost]
        public ActionResult deleteLoai(int id)
        {
            var loai = db.Loais.Single(x => x.ID == id);
           
            db.Loais.DeleteObject(loai);
            db.SaveChanges();

            return RedirectToAction("ListLoai");
        }

        
        public ActionResult ListNSX()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            ViewBag.list = db.ThuongHieux.ToList();
            return View();
        }

        [HttpPost]
        [Route("statusTHChange/{idTH}")]
        public JsonResult statusTHChange(int idTH)
        {
            var th = db.ThuongHieux.Single(x => x.ID == idTH);
            if (th.TrangThai == 1)
            {
                th.TrangThai = 0;
            }
            else if (th.TrangThai == 0)
            {
                th.TrangThai = 1;
            }

            db.SaveChanges();
            return Json(new
            {
                status = th.TrangThai
            });
        }

        [HttpPost]
        public ActionResult addThuongHieu(ThuongHieuModel model, HttpPostedFileBase files)
        {
            var th = new ThuongHieu()
            {
                TenTH = model.tenTH,
                SEO = model.SEO,
                TrangThai = 1
            };

            var ktHinh = new[] { ".png", ".jpg", ".jpeg" };

            var fileName = Path.GetFileName(files.FileName);
            var ext = Path.GetExtension(files.FileName);

            if (ktHinh.Contains(ext))
            {
                string name = Path.GetFileNameWithoutExtension(fileName);
                string thImage = name + ext;

                th.HinhTH = name + ext;


                files.SaveAs(Server.MapPath("~/Photos/ThuongHieu/" + thImage));
            }

            db.ThuongHieux.AddObject(th);
            db.SaveChanges();

            return RedirectToAction("ListNSX");
        }

        [HttpPost]
        public ActionResult editThuongHieu(ThuongHieuModel model, HttpPostedFileBase files)
        {
            var th = db.ThuongHieux.Single(x => x.ID == model.IDTH);
            th.TenTH = model.tenTH;
            th.SEO = model.SEO;

            if (files != null)
            {
                var ktHinh = new[] { ".png", ".jpg", ".jpeg" };

                var fileName = Path.GetFileName(files.FileName);
                var ext = Path.GetExtension(files.FileName);

                if (ktHinh.Contains(ext))
                {
                    string imgTHPath = Request.MapPath("~/Photos/ThuongHieu/" + th.HinhTH);
                    if (System.IO.File.Exists(imgTHPath))
                    {
                        System.IO.File.Delete(imgTHPath);
                    }

                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string thImage = name + ext;

                    th.HinhTH = name + ext;



                    files.SaveAs(Server.MapPath("~/Photos/ThuongHieu/" + thImage));
                }
            }


            db.ObjectStateManager.ChangeObjectState(th, System.Data.Entity.EntityState.Modified);
            db.SaveChanges();

            return RedirectToAction("ListNSX");
        }

        [HttpPost]
        public ActionResult deleteThuongHieu(int id)
        {
            var th = db.ThuongHieux.Single(x => x.ID == id);
            string imgTHPath = Request.MapPath("~/Photos/ThuongHieu/" + th.HinhTH);
            if (System.IO.File.Exists(imgTHPath))
            {
                System.IO.File.Delete(imgTHPath);
            }

            db.ThuongHieux.DeleteObject(th);
            db.SaveChanges();

            return RedirectToAction("ListNSX");
        }

    }
}