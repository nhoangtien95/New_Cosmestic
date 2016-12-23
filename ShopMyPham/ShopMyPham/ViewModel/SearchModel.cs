using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopMyPham.Models;
namespace ShopMyPham.ViewModel
{
    public class SearchModel    {
        private readonly ShopMyPhamEntities1 db = new ShopMyPhamEntities1();
        public List<SanPham> Search(string search)
        {
            return db.SanPhams.Where(x => x.TenSanPham.Contains(search)).ToList();
        }
    }
}