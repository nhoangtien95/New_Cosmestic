using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;

namespace ShopMyPham.ViewModel
{
    public class PromotionPartialViewModel
    {
        public IList<SanPhamHinh> newArrival { get; set; }
        public IList<SanPham> SEOname { get; set; }

        public PromotionPartialViewModel(IList<SanPhamHinh> newArrival, IList<SanPham> SEOname)
        {
            this.newArrival = newArrival;
            this.SEOname = SEOname;
        }

        public PromotionPartialViewModel()
        {
            //Default
            newArrival = new List<SanPhamHinh>();
            SEOname = new List<SanPham>();
        }
    }
}