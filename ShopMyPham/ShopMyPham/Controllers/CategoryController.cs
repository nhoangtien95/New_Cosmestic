using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;

namespace ShopMyPham.Controllers
{
    public class CategoryController : Controller
    {
            private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();


        // GET: Category
        #region Index

        /// <summary>
        ///     Hiển thị thông tin chi tiết sản phẩm
        /// </summary>
        /// <param name="id">ID sản phẩm</param>
        /// <param name="name">Tên SEO của sản phẩm</param>
        /// <returns>Thông tin chi tiết sản phẩm</returns>
        [Route("san-pham/{name}-{id}")]
        public ActionResult Index(int id = 0, string name = "")
        {
            var viewModel = db.SanPhams.SingleOrDefault(x => x.SanPhamID == id);
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            viewModel.SoLanXem++;
            db.SaveChanges();

            var productDetails = db.SanPhams.Include("SanPhamHinhs").SingleOrDefault(x => x.SanPhamID == id);
            ViewBag.Brand = db.ThuongHieux.Where(x => x.ID == productDetails.IDThuongHieu).ToList();

          
            //Sản phẩm liên quan
            var presentProduct = db.SanPhams.Where(x => x.SanPhamID == viewModel.SanPhamID); 
            ViewBag.relatedProduct = db.SanPhams.Where(x => x.IDLoai == viewModel.IDLoai || x.IDThuongHieu == viewModel.IDThuongHieu).Except(presentProduct).ToList() ;
            return View(productDetails);
        }
        #endregion


        public ActionResult BrandCategory(int id)
        {
            var viewModel = db.ThuongHieux.SingleOrDefault(x => x.ID == id);
            if (id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.productCategories = db.SanPhams.Include("SanPhamHinhs").OrderBy(x => x.TenSanPham).Where(x => x.IDThuongHieu == viewModel.ID).ToList();
            return View();
        }

        public ActionResult MenuCategory(int id)
        {
            var viewModel = db.Loais.SingleOrDefault(x => x.ID == id);
            if ( id <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            if (viewModel.ChungLoaiID == null)
            {
                //var MainMenu = db.Loais.Where(x => x.ChungLoaiID == viewModel.ID);
                //ViewBag.productCategories = db.SanPhams.Include("SanPhamHinhs").OrderBy(x => x.TenSanPham).Where(x => x.IDLoai == viewModel.ID ).ToList();


                var findId = db.Loais.Where(result => result.ChungLoaiID == id).Select(result => result.ID);
                ViewBag.productCategories = db.SanPhams.Where(x => findId.Contains((int)x.IDLoai)).ToList();
            }
            else
            {
                ViewBag.productCategories = db.SanPhams.Include("SanPhamHinhs").OrderBy(x => x.TenSanPham).Where(x => x.IDLoai == viewModel.ID).ToList();
            }
            return View();
        }
    }
}