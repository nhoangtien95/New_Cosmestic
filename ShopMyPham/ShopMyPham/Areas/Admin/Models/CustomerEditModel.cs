﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopMyPham.Areas.Admin.Models
{
    public class CustomerEditModel
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string pass { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(50, ErrorMessage = "Vui lòng nhập dưới 50 kí tự ")]
        public string diachi { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(12, ErrorMessage = "Số điện thoại không hợp lệ !")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Số điện thoại không hợp lệ !")]
        public string Sdt { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống !")]
        [StringLength(20, ErrorMessage = "Vui lòng nhập dưới 20 kí tự ")]
        public string email { get; set; }
    }
}