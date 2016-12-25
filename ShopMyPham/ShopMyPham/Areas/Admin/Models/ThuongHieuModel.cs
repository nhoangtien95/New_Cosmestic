using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    public class ThuongHieuModel
    {
        public int IDTH { get; set; }
        public string tenTH { get; set; }

        public int IDhinhTH { get; set; }
        public string tenhinhTH { get; set; }
        public string SEO { get; set; }
    }
}