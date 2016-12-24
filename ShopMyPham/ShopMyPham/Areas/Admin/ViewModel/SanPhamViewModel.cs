using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.ViewModel
{
    public class SanPhamViewModel
    {
        public IList<SanPham> SP { get; set; }
        public IList<Loai> Loai { get; set; }
        public IList<ThuongHieu> ThuongHieu { get; set; }

        public SanPhamViewModel( IList<SanPham> SP, IList<Loai> Loai, IList<ThuongHieu> ThuongHieu)
        {
            foreach(var item in SP)
            {
            }
        }
    }
}