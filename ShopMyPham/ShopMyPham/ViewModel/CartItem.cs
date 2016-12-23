using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;

namespace ShopMyPham.ViewModel
{
    public class CartItem
    {
        public int SanPhamID { get; set; }
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        public SanPhamHinh SanPhamHinh { get; set; }
        public CartItem()
        {

        }
        public CartItem(int SanPhamID, SanPham SanPham, SanPhamHinh SanPhamHinh, int SoLuong)
        {
            this.SanPhamID = SanPhamID;
            this.SanPham = SanPham;
            this.SanPhamHinh = SanPhamHinh;
            this.SoLuong = SoLuong;
        }
    }
}