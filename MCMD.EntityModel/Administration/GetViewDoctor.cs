using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MCMD.EntityModel.Administration
{
   public class GetViewDoctor
    {
       
        public int LoginId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string EmailID { get; set; }

        public int EmployeeId { get; set; }
        public string UserPhone { get; set; }
        //for Speciality Name
        public string SpecialityName { get; set; }

       // public int MembershipId { get; set; }
        //for getting Role Name 
        public string RoleName { get; set; }
        //for getting Clinic Name
        public string ClinicName { get; set; }

        public string MembershipType { get; set; }

        //DoctorPersonalInformation
        public string MiddleName { get; set; }
        public string Qualification { get; set; }
        public string RegistrationNo { get; set; }
        public string Affiliation { get; set; }
        public string AboutMe { get; set; }
        public string ExperienceInYear { get; set; }
        public string ExperienceInMonth { get; set; }
        public string InactiveFlag { get; set; }

        //Doctor Clinic Information
        //public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicPhoneNo { get; set; }
        public int ClinicFees { get; set; }
        public int City { get; set; }
        public int? ZipCode { get; set; }
        public string ClinicServices { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string AboutClinic { get; set; }
    }
}
