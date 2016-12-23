using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class ThongKeDAO
    {
        ShopMyPhamEntities1 db = null;
        public ThongKeDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public int? tongView()
        {
            int? tong = 0;
            foreach(SanPham sp in db.SanPhams.ToList())
            {
                tong = tong + sp.SoLanXem;
            }
            return tong;
        }
        public decimal tongDoanhThu()
        {
            decimal tong = 0;
            DonHangDAO dao = new DonHangDAO();

            foreach(DonDatModel dd in dao.GetListHoaDon())
            {
                if(dd.NgayDatHang.Month.Equals(DateTime.Now.Month) && dd.NgayDatHang.Year.Equals(DateTime.Now.Year))
                    tong = tong + dd.tongTien();
            }
            return tong/1000000;
        }
        public decimal DTTT(int thang)
        {
            decimal tong = 0;
            DonHangDAO dao = new DonHangDAO();

            foreach (DonDatModel dd in dao.GetListHoaDon())
            {
                if (dd.NgayDatHang.Month.Equals(thang) && dd.NgayDatHang.Year.Equals(DateTime.Now.Year))
                    tong = tong + dd.tongTien();
            }
            return tong;
        }
        public int tongDonDat()
        {
            int tong = 0;
            foreach(DonHang dd in db.DonHangs.ToList())
            {
                if (dd.NgayDatHang.Month.Equals(DateTime.Now.Month) && dd.NgayDatHang.Year.Equals(DateTime.Now.Year))
                    tong++;
            }
            return tong;
        }

        public int luotBan(int idsp)
        {
            int tong = 0;
            List<DonHangChiTiet> list = new List<DonHangChiTiet>();
            foreach(DonHang dd in db.DonHangs.ToList())
            {
                if(dd.NgayDatHang.Month.Equals(DateTime.Now.Month) && dd.NgayDatHang.Year.Equals(DateTime.Now.Year))
                    foreach(DonHangChiTiet dhct in db.DonHangChiTiets.ToList())
                    {
                        if (dhct.DonHangID.Equals(dd.DonHangID))
                            list.Add(dhct);
                    }
            }
            foreach(DonHangChiTiet ct in list)
            {         
                if (ct.SanPhamID.Equals(idsp))
                    tong = tong + ct.SoLuong;
            }
            return tong;
        }

        public SanPhamDatModel getSanPhamBanChay()
        {           
            int max=0;
            foreach(SanPham sp in db.SanPhams)
            {
                if(luotBan(sp.SanPhamID) > max)
                {
                    max = luotBan(sp.SanPhamID);
                }
            }
            foreach (SanPham sp in db.SanPhams)
            {
                if (luotBan(sp.SanPhamID) == max)
                {
                    SanPhamDatModel spd = new SanPhamDatModel();
                    spd.MaSanPham = sp.MaSanPham;
                    spd.SanPhamID = sp.SanPhamID;
                    spd.SoLuong = max;
                    return spd;
                }
            }
            return null;
        }
       
    }
}