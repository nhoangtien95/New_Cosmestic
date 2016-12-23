using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class ThuongHieuDAO
    {
        ShopMyPhamEntities1 db = null;
        public ThuongHieuDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public ThuongHieu GetByID(int? id)
        {
            var result = db.ThuongHieux.SingleOrDefault(x => x.ID == id);
            return result;
        }
        public List<ThuongHieu> GetList()
        {
            return db.ThuongHieux.ToList();
        }
    }
}