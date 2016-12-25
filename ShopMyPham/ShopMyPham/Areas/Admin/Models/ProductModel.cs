using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Models
{
    public class ProductModel
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public string Mota { get; set; }
        public int IDThuongHieu { get; set; }
        public string ThuongHieu { get; set; }
        public SelectList ThuongHieuSelected { get; set; }
        public int IDLoai { get; set; }
        public string Loai { get; set; }
        public SelectList LoaiSelected { get; set; }
        public Nullable<int> SoLanXem { get; set; }
        public System.DateTime NgayThem { get; set; }
        public int IDKhuyenMai { get; set; }
        public string KhuyenMai { get; set; }
        public SelectList KhuyenMaiSelected { get; set; }
        public string SEO { get; set; }
        public int IDHinh { get; set; }
    }
}