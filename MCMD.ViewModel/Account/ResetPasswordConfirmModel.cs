using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCMD.ViewModel.Account
{
    public class ResetPasswordConfirmModel
    {
        public string EmailId { get; set; }
        public string Token { get; set; }
      

        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        [Required(ErrorMessage = "Please Enter Your New Password")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please Enter Your Confirm Password")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("NewPassword", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
