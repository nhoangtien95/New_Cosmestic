using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopMyPham.Models;

namespace ShopMyPham.Controllers
{
    public class ClientController : Controller
    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        // GET: Client
        [Route("user")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(QuanTriModel model)
        {
            var user = db.QuanTris.SingleOrDefault(x => x.Username.Equals(model.Username));
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng !");
            }
            else
            {
                if (user.Password.Equals(model.Password))
                {
                    if (user.Level == 1)
                    {
                        Session["user"] = new QuanTri { ID = user.ID, Username = model.Username, Ten = user.Ten, DiaChi = user.DiaChi, Sdt = user.Sdt, Email = user.Email };
                        return RedirectToAction("Index", "Home", Session["user"]);
                    }
                    else if (user.Level == 2)
                    {
                        Session["user"] = new QuanTri { ID = user.ID, Username = model.Username, Ten = user.Ten, DiaChi = user.DiaChi, Sdt = user.Sdt, Email = user.Email };
                        return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                    }
                }
                else ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
           // return View();
        }
        public ActionResult Register(QuanTriModel model)
        {
            if (ModelState.IsValid)
            {
                QuanTri qt = new QuanTri()
                {
                    Username = model.Username,
                    Password = model.Password,
                    TrangThai = false,
                    Sdt = model.Phone
                };
                if (ModelState.IsValid)
                {
                    db.QuanTris.AddObject(qt);
                    db.SaveChanges();
                    return RedirectToAction("Finish");
                }
            }

            return View("Index");
        }


        #region Finish

        /// <summary>
        ///     Giao diện đăng ký mới
        /// </summary>        /// 
        /// <returns>Chuyển trang đăng nhập</returns>
        /// 
        [Route("hoan-tat-dang-ky")]
        public ActionResult Finish()
        {
            return View();
        }
        #endregion

    }
}