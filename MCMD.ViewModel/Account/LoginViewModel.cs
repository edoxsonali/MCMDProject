using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MCMD.ViewModel.Account
{
   public class LoginViewModel
    {

        [Required(ErrorMessage="*Email is Required")]
       public string EmailId { get; set; }

        [Required(ErrorMessage = "*Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
