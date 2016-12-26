using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;
using ShopMyPham.ViewModel;
namespace ShopMyPham.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Cart
        [Route("Cart")]
        public ActionResult Index()
        {
            var cart = Session["Cart"] as CartModel;

            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Client");
            }

            if (cart == null || cart.Items.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Cart = GetCart();
            return View(cart);
        }


        /// <summary>
        ///     Create new shopping cart
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

        #region AddToCart

        /// <summary>
        ///     Add product to cart
        /// </summary>
        /// <param name="id">Compare product id with resource</param>
        /// <param name="quantity">Get product quantity</param>
        /// <returns>Info of cart</returns>
        [Route("gio-hang/them-san-pham/{id}/{quantity}")]
        [HttpPost]
        public RedirectToRouteResult AddToCart(int id, int quantity = 1)
        {
            var product = db.SanPhams.Include("SanPhamHinhs").SingleOrDefault(x => x.SanPhamID == id);
            var cart = GetCart();
            if (product.IDKhuyenMai == 2)
            {
                product.GiaBan = product.GiaBan - (product.GiaBan * Convert.ToDecimal(0.2));
            }
            else if (product.IDKhuyenMai == 3)
            {
                product.GiaBan = product.GiaBan - (product.GiaBan * Convert.ToDecimal(0.1));
            }
            
            if (quantity > 10)
            {
                quantity = 10;
            }
            else if (quantity < 1)
            {
                quantity = 1;
            }
                var cartItem = new CartItem(id, product, product.SanPhamHinhs.FirstOrDefault(), quantity);
            cart.Add(cartItem);
            return RedirectToAction("Index");
        }

        #endregion

        #region UpdateCart

        /// <summary>
        ///     Update cart
        /// </summary>
        /// <param name="id">Compare product id with resource</param>
        /// <param name="quantity">Get product quantity</param>
        /// <returns>Info of updated cart</returns>
        [Route("gio-hang/cap-nhat-san-pham/{id}/{quantity}")]
        [HttpPost]
        public RedirectToRouteResult UpdateCart(int id, int quantity = 1)
        {
            var cart = GetCart();
            if (quantity > 10)
            {
                quantity = 10;
            }
            else if (quantity < 1)
            {
                quantity = 1;
            }

            cart.Update(id, quantity);
            return RedirectToAction("Index");
        }

        #endregion

        #region DeleteCart

        /// <summary>
        ///     Delete product from cart
        /// </summary>
        /// <param name="id">Compare product id with resource</param>
        /// <returns>Info of cart</returns>
        [Route("gio-hang/xoa-san-pham/{id}")]
        [HttpPost]
        public RedirectToRouteResult DeleteCart(int id)
        {
            var cart = GetCart();
            cart.Remove(id);
            return RedirectToAction("Index");
        }

        #endregion


        #region _CartDetails

        /// <summary>
        ///     Load cart after action
        /// </summary>
        /// <returns>Info of cart</returns>
        [Route("gio-hang")]
        public PartialViewResult _CartDetails()
        {
            var cart = GetCart();
            ViewBag.Cart = GetCart();

            return PartialView(cart);
        }
        #endregion

        #region CheckOutStep1

        /// <summary>
        ///     Save cart info
        /// </summary>
        /// <returns>Info of cart</returns>
        /// 
        public ActionResult CheckOutStep1()
        {
            var cart = GetCart();
            var user = Session["user"] as QuanTri;

            if (user.DiaChi == null)
            {
                return RedirectToAction("userInfo", "Home");
            }

            DonHang dh = new DonHang()
            {
                NgayDatHang = DateTime.UtcNow,
                UserId = user.ID,
                TenKhachHang = user.Ten,
                DiaChi = user.DiaChi,
                SoDienThoai = user.Sdt,
                Tinhtrang = false
            };
            db.DonHangs.AddObject(dh);
            db.SaveChanges();

            foreach (var item in cart.Items)
            {
                DonHangChiTiet detail = new DonHangChiTiet()
                {
                    DonHangID = dh.DonHangID,
                    SanPhamID = item.SanPhamID,
                    SoLuong = item.SoLuong,
                    DonGia = item.SanPham.GiaBan,
                    ThanhTien = (item.SoLuong * item.SanPham.GiaBan)
                };
                db.DonHangChiTiets.AddObject(detail);
                db.SaveChanges();
            }
            cart.Clear();
            return RedirectToAction("thankYou");
        }
        #endregion

        public ActionResult thankYou()
        {
            return View();
        }

    }
}