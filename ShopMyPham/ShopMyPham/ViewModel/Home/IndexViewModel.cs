using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;

namespace ShopMyPham.ViewModel
{
    public class IndexViewModel
    {
        public IList<Loai> Loai { get; set; }
        public IList<SanPham> SP_img { get; set; }
        public IndexViewModel(IList<Loai> Loai, IList<SanPham> SP_img)
        {
            this.Loai = Loai;
            this.SP_img = SP_img;
        }
        public IndexViewModel()
        {

        }
    }
}