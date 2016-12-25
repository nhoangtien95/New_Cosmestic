using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;
using ShopMyPham.ViewModel;


namespace ShopMyPham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();

        #region Index

        /// <summary>
        ///     Hiển thị các sản phẩm ở trang chủ
        /// </summary>
        [Route("home")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        #region Cart

        /// <summary>
        ///     Hiển thị giỏ hàng ở layout
        /// </summary>
        public PartialViewResult _HeaderTopPartial()
        {
            ViewBag.Cart = GetCart();
            var cart = GetCart();
            return PartialView(cart);
        }
        #endregion


        #region MainMenu-Partial

        /// <summary>
        ///     Hiển thị menu cha
        /// </summary>
        /// <returns>Thông tin của menu cha</returns>
        [ChildActionOnly]
        public PartialViewResult _MainMenuPartial()
        {
            ViewBag.Son = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 1 ).ToList();
            ViewBag.Eye = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 2).ToList();
            ViewBag.Face = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 3).ToList();
            ViewBag.Tool = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 4).ToList();
            ViewBag.Nails = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 5).ToList();
            ViewBag.Skin = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null && x.ID == 6).ToList();
            ViewBag.ThuongHieu = db.ThuongHieux.OrderBy(x => x.TenTH).Where(x => x.TrangThai == 1).ToList();
            var item = new ViewModel.IndexViewModel();
            return PartialView(item);
        }
        #endregion




        #region ChildMenu-Partial

        /// <summary>
        ///     Hiển thị các menu con
        /// </summary>
        /// <param name="ParentID">ID của menu cha</param>
        /// <returns>Thông tin các menu con</returns>
        [ChildActionOnly]
        public PartialViewResult _ChildrenMenuPartial(int ParentID)
        {
            var subMenu = db.Loais.OrderBy(x => x.Ten).Where(x => x.ChungLoaiID == ParentID).ToList();
            return PartialView(subMenu);
        }
        #endregion

        #region GetCart
        // GET: Cart
        /// <summary>
        ///     Create new shopping cart if session cart is null
        ///     or else return the item in the shopping cart
        /// </summary>
        /// <returns>New Shopping cart</returns>
        private CartModel GetCart()
        {
            var cart = Session["Cart"] as CartModel;
            if (cart == null)
            {
                cart = new CartModel();
                Session["Cart"] = cart;
            }

            return cart;
        }

        #endregion

        /// <summary>
        ///     Load menu when user buy something
        /// </summary>
        /// <returns>Ajax call to partial menu</returns>
        [Route("load-cart")]
        public PartialViewResult _CartLoad()
        {
            ViewBag.Cart = GetCart();
            var cart = GetCart();

            return PartialView(cart);
        }


        #region Search-Partial

        /// <summary>
        ///     Hiển thị danh mục tìm kiếm
        /// </summary>
        /// <returns>Thông tin tất cả danh mục</returns>
        [ChildActionOnly]
        public PartialViewResult _ChildrenSearchPartial()
        {
            var search = db.Loais.OrderBy(x => x.ID).Where(x => x.ChungLoaiID == null).ToList();
            return PartialView(search);
        }
        #endregion



        #region Category-Partial

        /// <summary>
        ///     Hiển thị sản phẩm bán chạy, sản phẩm mới
        /// </summary>
        /// <returns>Thông tin phẩm bán chạy, sản phẩm mới</returns>
        [ChildActionOnly]
        public PartialViewResult _CategoryPartial()
        {
            ///currentDate thời gian cách đây 14 ngày
            DateTime currenteDate = DateTime.UtcNow.AddDays(-14);

            //New Arrival Products
            ViewBag.newArrival = db.SanPhams.Include("SanPhamHinhs").Where(x => x.TrangThai == 1).OrderBy(x => x.NgayThem).Take(12).ToList();

            //Most View Products
            ViewBag.mostView = db.SanPhams.Include("SanPhamHinhs").Where(x => x.NgayThem >= currenteDate && x.TrangThai == 1).OrderBy(x => x.SoLanXem).Take(12).ToList();

            //Sale countdown
            ViewBag.countdown = db.SanPhams.Include("SanPhamHinhs").Where(x => x.IDKhuyenMai == 2).Take(4).ToList();
            ViewBag.time = db.KhuyenMais.Where(x => x.KhuyenMaiID == 2).Select(x => x.NgayKetThuc).ToList();

            return PartialView();
        }
        #endregion




        #region Promotion-Partial

        /// <summary>
        ///     Hiển thị thông tin khuyến mãi, sản phẩm đề xuất
        /// </summary>
        /// <permission cref=""
        /// <returns>Thông tin khuyến mãi, sản phẩm đề xuất</returns>
        [ChildActionOnly]
        public PartialViewResult _PromotionPartial()
        {
            ///currentDate thời gian cách đây 7 ngày
            DateTime currentDate = DateTime.UtcNow.AddDays(-7);

            //New Arrival Products
            ViewBag.newArrival = db.SanPhams.Include("SanPhamHinhs").OrderByDescending(x => x.NgayThem).Where(x => x.NgayThem >= currentDate).Take(6).ToList();

            //Featured Products
            ViewBag.featuredProducts = db.SanPhams.Include("SanPhamHinhs").OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            
            return PartialView();
        }
        #endregion




        #region Brand-Partial

        /// <summary>
        ///     Hiển thị hình ảnh các Brand
        /// </summary>
        /// <returns>Hình ảnh các brand đang có</returns>
        [ChildActionOnly]
        public PartialViewResult _BrandPartial()
        {
            var banner = db.ThuongHieux.Where(x => x.TrangThai == 1).ToList();
            return PartialView(banner);
        }
        #endregion

        #region AutoComplete

        /// <summary>
        ///     Tìm kiếm autocomplete
        /// </summary>
        /// <returns>5 kết quả tìm kiếm</returns>
        public ActionResult Autocomplete(string term)
        {
            List<SanPham> list = new List<SanPham>();
            list = db.SanPhams.Include("SanPhamHinhs").Where(x => x.TenSanPham.Contains(term)).ToList();

            return Json(list.Select(x => new { TenSanPham = x.TenSanPham, TenHinh = x.SanPhamHinhs.FirstOrDefault().TenHinh }), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Search

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("tim-kiem/result")]
        public ActionResult Search(string result)
        {
            ViewBag.searchProducts = db.SanPhams.Include("SanPhamHinhs").Where(x => x.TenSanPham.Contains(result)).ToList();
          
            ViewBag.Title = "Kết quả tìm kiếm " + result;
            var countResult = db.SanPhams.Where(x => x.TenSanPham.Contains(result)).Count();
            ViewBag.Count = "Có " + countResult + " kết quả tương ứng";
            return View();
        }

        #endregion

        [Route("thuong-hieu")]
        public ActionResult allBrand()
        {
            ViewBag.ThuongHieu = db.ThuongHieux.OrderBy(x => x.TenTH).Where(x => x.TrangThai == 1).ToList();
            return View();
        }

        [Route("lien-he")]
        public ActionResult Contact()
        {         

            return View();
        }

        #region UserInfo

        /// <summary>
        ///     Hiển thị thông tin người dùng
        /// </summary>
        public ActionResult userInfo()
        {
             if(Session["user"] == null)
            {
                return RedirectToAction("Index", "Client");
            }
            return View();
        }
        #endregion
    }
}