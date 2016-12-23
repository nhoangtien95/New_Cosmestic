using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMyPham.Models
{
    public class QuanTriModel
    {
        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength( 20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength( 20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength( 12, ErrorMessage = "Số điện thoại không hợp lệ !")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string Phone { get; set; }
    }
}