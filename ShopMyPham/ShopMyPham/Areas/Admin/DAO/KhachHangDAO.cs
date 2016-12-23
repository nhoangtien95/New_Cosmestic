using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class KhachHangDAO
    {
        ShopMyPhamEntities1 db = null;
        public KhachHangDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public List<KhachHangModel> getList()
        {
            var list = db.QuanTris.Where(x => x.Level == 1).ToList();
            List<KhachHangModel> l = new List<KhachHangModel>();
            KhachHangModel user = null;
            foreach (QuanTri result in list)
            {
                user = new KhachHangModel();
                user.ID = result.ID;
                user.Username = result.Username;
                user.Ten = result.Ten;
                user.Sdt = result.Sdt;
                user.DiaChi = result.DiaChi;
                user.Email = result.Email;
                user.TrangThai = result.TrangThai;
                l.Add(user);
            }
            return l;
        }
    }
}