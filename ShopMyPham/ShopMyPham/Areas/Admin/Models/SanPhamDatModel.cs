using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    [Serializable]
    public class SanPhamDatModel
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }        
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
    }
}