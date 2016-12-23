using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class DonHangDAO
    {
        ShopMyPhamEntities1 db = null;
        public DonHangDAO()
        {
            db = new ShopMyPhamEntities1();
        }

        public List<DonDatModel> GetListHoaDon()
        {
            DonHangChiTietDAO dao = new DonHangChiTietDAO();
            List<DonDatModel> list = new List<DonDatModel>();            
            DonDatModel hd = null;
            foreach(DonHang dh in db.DonHangs.ToList())
            {
                if (dh.Tinhtrang.Equals(true))
                {
                    hd = new DonDatModel();
                    hd.DonHangID = dh.DonHangID;
                    hd.dsSanPham = dao.GetListSP(hd.DonHangID);
                    hd.TenKhachHang = dh.TenKhachHang;
                    hd.DiaChi = dh.DiaChi;
                    hd.GhiChu = dh.GhiChu;
                    hd.NgayDatHang = dh.NgayDatHang;
                    hd.SoDienThoai = dh.SoDienThoai;
                    hd.UserId = dh.UserId;
                    list.Add(hd);
                }
            }
            
            return list;
        }
        public List<DonDatModel> GetListDonDat()
        {
            DonHangChiTietDAO dao = new DonHangChiTietDAO();
            List<DonDatModel> list = new List<DonDatModel>();
            DonDatModel hd = null;
            foreach (DonHang dh in db.DonHangs.ToList())
            {
                if (dh.Tinhtrang.Equals(false))
                {
                    hd = new DonDatModel();
                    hd.DonHangID = dh.DonHangID;
                    hd.dsSanPham = dao.GetListSP(hd.DonHangID);
                    hd.TenKhachHang = dh.TenKhachHang;
                    hd.DiaChi = dh.DiaChi;
                    hd.GhiChu = dh.GhiChu;
                    hd.NgayDatHang = dh.NgayDatHang;
                    hd.SoDienThoai = dh.SoDienThoai;
                    hd.UserId = dh.UserId;
                    list.Add(hd);
                }
            }

            return list;
        }
    }
}