using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Models
{
    public class UserViewModel
    {
        
        public int DonHangID { get; set; }
        public List<DonHangChiTiet> dsSanPham { get; set; }
        public System.DateTime NgayDatHang { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public Nullable<int> UserId { get; set; }
        public decimal tongTien()
        {
            decimal tong = 0;
            foreach (var sp in dsSanPham)
            {
                tong = tong + sp.ThanhTien;
            }
            return tong;
        }
    }
}