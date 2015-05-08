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
       
        public string EmailId { get; set; }

    
        public string Password { get; set; }

        
        public bool RememberMe { get; set; }
    }
}
