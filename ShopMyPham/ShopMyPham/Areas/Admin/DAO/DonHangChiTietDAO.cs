using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class DonHangChiTietDAO
    {
        ShopMyPhamEntities1 db = null;
        public DonHangChiTietDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public List<DonHangChiTiet> GetByID(int id)
        {
            List<DonHangChiTiet> list = new List<DonHangChiTiet>();
            var result = db.DonHangChiTiets.ToList();
            foreach (DonHangChiTiet dh in result)
                if (dh.DonHangID == id)
                    list.Add(dh);
            return list;
        }
        public List<SanPhamDatModel> GetListSP(int id)
        {
            List<SanPhamDatModel> list = new List<SanPhamDatModel>();
            SanPhamDAO dao = new SanPhamDAO();
            List<DonHangChiTiet> dsdh = GetByID(id);
            SanPham sp = null;
            SanPhamDatModel spd = null;
            foreach(DonHangChiTiet dh in dsdh)
            {
                sp = dao.GetByID(dh.SanPhamID);
                spd = new SanPhamDatModel();
                spd.MaSanPham = sp.MaSanPham;
                spd.SanPhamID = sp.SanPhamID;
                spd.SoLuong = dh.SoLuong;
                spd.DonGia = dh.DonGia;
                spd.ThanhTien = dh.ThanhTien;
                list.Add(spd);
            }
            return list;
        }
    }
}