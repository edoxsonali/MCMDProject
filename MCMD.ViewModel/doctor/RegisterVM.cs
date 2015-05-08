using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;
using System.ComponentModel.DataAnnotations;

namespace MCMD.ViewModel.doctor
{
    public class RegisterVM
    {
        public List<Speciality> Specialitys { get; set; }

        public List<Title> Titles { get; set; }
        public Register Registers { get; set; }

        public int ID { get; set; }
        public int TitleId { get; set; }
    }
}
