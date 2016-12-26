using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Models
{
    public class UserSPDatModel
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
    }
}