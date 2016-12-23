using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class KhuyenMaiDAO
    {
        ShopMyPhamEntities1 db = null;
        public KhuyenMaiDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public KhuyenMai GetByID(int? id)
        {
            var result = db.KhuyenMais.SingleOrDefault(x => x.KhuyenMaiID == id);
            return result;
        }
    }
}