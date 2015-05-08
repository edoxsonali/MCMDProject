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
    public class ForgetPasswordModel
    {
        [DisplayName("Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Please Enter Your Registered Email")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string EmailId { get; set; }
    }
}
