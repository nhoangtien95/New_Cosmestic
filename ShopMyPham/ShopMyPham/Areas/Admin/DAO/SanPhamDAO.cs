using ShopMyPham.Areas.Admin.Models;
using ShopMyPham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.DAO
{
    public class SanPhamDAO
    {
        ShopMyPhamEntities1 db = null;
        public SanPhamDAO()
        {
            db = new ShopMyPhamEntities1();
        }
        public List<SanPhamModel> GetAllList()
        {
            var list = db.SanPhams.ToList();
            List<SanPhamModel> l = new List<SanPhamModel>();
            ThuongHieuDAO thuongHieuDAO = new ThuongHieuDAO();
            LoaiDAO loaiDAO = new LoaiDAO();
            KhuyenMaiDAO khuyenMaiDAO = new KhuyenMaiDAO();
            SanPhamModel sanPham = null;
            foreach (SanPham result in list)
            {
                sanPham = new SanPhamModel();
                sanPham.SanPhamID = result.SanPhamID;
                sanPham.MaSanPham = result.MaSanPham;
                sanPham.TenSanPham = result.TenSanPham;
                sanPham.GiaBan = result.GiaBan;
                sanPham.TrangThai = result.TrangThai;
                sanPham.Mota = result.Mota;
                sanPham.ThuongHieu = thuongHieuDAO.GetByID(result.IDThuongHieu).TenTH;
                sanPham.Loai = loaiDAO.GetByID(result.IDLoai).Ten;
                sanPham.SoLanXem = result.SoLanXem;
                sanPham.NgayThem = result.NgayThem;
                sanPham.IDKhuyenMai = khuyenMaiDAO.GetByID(result.IDKhuyenMai).KhuyenMaiID;
                sanPham.KhuyenMai = khuyenMaiDAO.GetByID(result.IDKhuyenMai).Ten;
                sanPham.SEO = result.SEO;
                sanPham.SanPhamHinhs = result.SanPhamHinhs;
                l.Add(sanPham);
            }
            return l;
        }
        public SanPham GetByID(int id)
        {
            var result = db.SanPhams.SingleOrDefault(x => x.SanPhamID == id);
            return result;
        }
    }
}