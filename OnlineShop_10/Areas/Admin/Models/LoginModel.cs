using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop_10.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập username ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mời nhập password ")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}