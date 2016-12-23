using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class NhanVienDAO
    {
        ShopMyPhamEntities1 db = null;
        public NhanVienDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public List<NhanVienModel> getList()
        {
            var list = db.QuanTris.Where(x => x.Level == 2).ToList();
            List<NhanVienModel> l = new List<NhanVienModel>();
            NhanVienModel user = null;
            foreach (QuanTri result in list)
            {
                if (result.TrangThai == true)
                {
                    user = new NhanVienModel();
                    user.ID = result.ID;
                    user.Username = result.Username;
                    user.Ten = result.Ten;
                    user.Sdt = result.Sdt;
                    user.DiaChi = result.DiaChi;
                    user.Email = result.Email;
                    l.Add(user);
                }
            }
            return l;
        }
        public bool AddNhanVien( NhanVienModel result)
        {
            QuanTri user = new QuanTri();
            user.ID = result.ID;
            user.Username = result.Username;
            user.Ten = result.Ten;
            user.Sdt = result.Sdt;
            user.DiaChi = result.DiaChi;
            user.Email = result.Email;
            user.TrangThai = true;
            db.QuanTris.AddObject(user);
            db.SaveChanges();
            return true;
        }
    }
}