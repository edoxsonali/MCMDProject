using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;

namespace MCMD.ViewModel.Administration
{
     public class SpecialityViewModel
    {
         public List<Speciality> SpecialityList { get; set; }
         public int SpecialityID { get; set; }
         public Speciality specialitys { get; set; }

         public string SpecialityName { get; set; }
    }
}
