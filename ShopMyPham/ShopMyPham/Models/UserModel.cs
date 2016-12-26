using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopMyPham.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string password { get; set; }
        public string passwordNew { get; set; }
        public string passwordNewComfirm { get; set; }
        public string ten { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
    }
}