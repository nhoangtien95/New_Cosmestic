using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    [Serializable]
    public class SanPhamModel
    {
        public int SanPhamID { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public Nullable<byte> TrangThai { get; set; }
        public string Mota { get; set; }
        public string ThuongHieu { get; set; }
        public string Loai { get; set; }
        public Nullable<int> SoLanXem { get; set; }
        public System.DateTime NgayThem { get; set; }
        public Nullable<byte> Promotion { get; set; }
        public string IDKhuyenMai { get; set; }
        public string SEO { get; set; }

        public virtual ICollection<SanPhamHinh> SanPhamHinhs { get; set; }
    }
}