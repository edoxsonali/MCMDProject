using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MCMD.EntityModel.Doctor
{
    public class Speciality
    {
        [Key]
        public int SpecialityID { get; set; }


       //[DisplayName("Speciality")]
       //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Speciality is not valid.")]
       //[Required(ErrorMessage = "Speciality is required.")]
       //[StringLength(50, ErrorMessage = "Speciality cannot be longer than 50 characters.")]
       public string SpecialityName { get; set; }

       public string InactiveFlag { get; set; }
       public System.DateTime ModifiedDate { get; set; }

      
    }
}
