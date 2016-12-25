using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopMyPham.Areas.Admin.Models
{
    public class LoaiModel
    {
        public int IDLoai { set; get; }
        public string tenLoai { get; set; }
        public int chungloaiID {get;set;}
        public SelectList chungloaiIDSelected { get; set; }
        public string SEO { get; set; }
    }
}