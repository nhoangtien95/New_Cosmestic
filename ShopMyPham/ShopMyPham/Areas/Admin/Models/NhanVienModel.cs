using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    [Serializable]
    public class NhanVienModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string Password { get; set; }
        public Nullable<bool> TrangThai { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(30, ErrorMessage = "Vui lòng nhập dưới 30 kí tự ")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới 50 kí tự ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(12, ErrorMessage = "Số điện thoại không hợp lệ !")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string Email { get; set; }
        public Nullable<byte> Level { get; set; }
    }
}