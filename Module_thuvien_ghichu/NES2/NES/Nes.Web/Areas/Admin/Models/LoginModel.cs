using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nes.Web.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "AdminLoginUsernameText", ResourceType = typeof(Resources.NesResource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "AdminLoginPasswordText", ResourceType = typeof(Resources.NesResource))]
        public string Password { get; set; }

        [Display(Name = "AdminLoginRemembermeText",ResourceType= typeof(Resources.NesResource))]
        public bool RememberMe { get; set; }
    }
}