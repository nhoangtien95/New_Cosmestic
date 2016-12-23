using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class LoaiDAO
    {
        ShopMyPhamEntities1 db = null;
        public LoaiDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public Loai GetByID(int? id)
        {
            var result = db.Loais.SingleOrDefault(x => x.ID == id);
            return result;
        }
        public List<Loai> GetList()
        {
            return db.Loais.ToList();
        }
    }
}