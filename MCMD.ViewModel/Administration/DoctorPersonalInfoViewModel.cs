using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;


namespace MCMD.ViewModel.Administration
{
    public class DoctorPersonalInfoViewModel
    {
        public DoctorPersonalInformation _doctorPerInfo { get; set; }
        public UserLogin userlogin { get; set; }
        public Speciality _speciality { get; set; }
        public List<Speciality> SpecialityList { get; set; }

        public List<UserLogin> UserLogins { get; set; }
        public List<UserLoginSpeciality> UserLoginSpeciality { get; set; }
        public int SpecialityID { get; set; }
        public int PersonalInfoId { get; set; }
        public int LoginId { get; set; }
        public int LoginSpecialityId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SpecialitiID { get; set; }
        public string EmailID { get; set; }
        public string UserPhone { get; set; }
        public string Qualification { get; set; }



        public int RegistrationNo { get; set; }
        public string Affiliation { get; set; }
        public string AboutMe { get; set; }

        public string AboutExperience { get; set; }
        public string InactiveFlag { get; set; }

        public int CreatedByID { get; set; }


        public System.DateTime CreatedDate { get; set; }

        public int ModifiedByID { get; set; }

        public System.DateTime ModifiedDate { get; set; }


    }
}
