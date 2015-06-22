using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;
using System.ComponentModel.DataAnnotations;

namespace MCMD.ViewModel.Administration
{
     public class SpecialityViewModel
    {
         public List<Speciality> SpecialityList { get; set; }
         public int SpecialityID { get; set; }
         public Speciality specialityss { get; set; }


         [Required(ErrorMessage = "Speciality Name is required.")]
         public string SpecialityName { get; set; }
    }
}
