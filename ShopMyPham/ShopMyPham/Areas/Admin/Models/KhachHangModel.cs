using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    [Serializable]
    public class KhachHangModel
    {
        public int ID { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }        
        public Boolean? TrangThai { get; set; }
    }
}