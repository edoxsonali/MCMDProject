using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.ViewModel.doctor
{
    public class HomeDocSearchViewModel
    {
        public List<UserLogin> getUser { get; set; }
        public List<UserLoginSpeciality> getUserlogSpecty { get; set; }
        public List<Speciality> GetSpecialitylist { get; set; }
        public int SpecialityID { get; set; }
        //public string SpecialityName { get; set; }

        public List<City> GetCitys { get; set; }
        public int CityId { get; set; }
        public int CityName { get; set; }
        public List<DoctorClinicInformation> GetDoctorClinicInformation { get; set; }

        // public string ClinicName { get; set; }

        public List<GetViewDoctor> getDoctor { get; set; }
        public List<GetAllData> getSearchDoc { get; set; }
        public int LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserPhone { get; set; }
        public List<Role> getRoles { get; set; }
        public int RoleId { get; set; }

        //****************ClinicTime ViewModel************************//

        public string ClinicName { get; set; }
        public string ClinicAddress { get; set; }
        public string SpecialityName { get; set; }
        public string AboutClinic { get; set; }
        public int ClinicFees { get; set; }
        public string ClinicServices { get; set; }
        public string FolderFilePath { get; set; }
        public string ClinicPhoneNo { get; set; }

        public string latitude { get; set; }
        public string longitude { get; set; }

        public List<GetService> getservices { get; set; }
        public List<Media> getclinicmedias { get; set; }
        public List<Media> getclinicvideos { get; set; }
        //first seating time
        public string StartTimefs1 { get; set; }
        public string EndTimefs1 { get; set; }
        public string StartTimefs2 { get; set; }
        public string EndTimefs2 { get; set; }
        public string StartTimefs3 { get; set; }
        public string EndTimefs3 { get; set; }
        public string StartTimefs4 { get; set; }
        public string EndTimefs4 { get; set; }
        public string StartTimefs5 { get; set; }
        public string EndTimefs5 { get; set; }
        public string StartTimefs6 { get; set; }
        public string EndTimefs6 { get; set; }
        public string StartTimefs7 { get; set; }
        public string EndTimefs7 { get; set; }

        //Second seating time
        public string StartTimess1 { get; set; }
        public string EndTimess1 { get; set; }
        public string StartTimess2 { get; set; }
        public string EndTimess2 { get; set; }
        public string StartTimess3 { get; set; }
        public string EndTimess3 { get; set; }
        public string StartTimess4 { get; set; }
        public string EndTimess4 { get; set; }
        public string StartTimess5 { get; set; }
        public string EndTimess5 { get; set; }
        public string StartTimess6 { get; set; }
        public string EndTimess6 { get; set; }
        public string StartTimess7 { get; set; }
        public string EndTimess7 { get; set; }

        //third seating time
        public string StartTimets1 { get; set; }
        public string EndTimets1 { get; set; }
        public string StartTimets2 { get; set; }
        public string EndTimets2 { get; set; }
        public string StartTimets3 { get; set; }
        public string EndTimets3 { get; set; }
        public string StartTimets4 { get; set; }
        public string EndTimets4 { get; set; }
        public string StartTimets5 { get; set; }
        public string EndTimets5 { get; set; }
        public string StartTimets6 { get; set; }
        public string EndTimets6 { get; set; }
        public string StartTimets7 { get; set; }
        public string EndTimets7 { get; set; }




        //****************************doctor Profile View Mode;*********************************//
        //    public string FirstName { get; set; }

        public string MiddleName { get; set; }

        //  public string LastName { get; set; }

        public string Qualification { get; set; }

        // public string SpecialityName { get; set; }

        public string ExperienceInYear { get; set; }
        public string ExperienceInMonth { get; set; }

        //  public string ClinicAddress { get; set; }
        public string AboutMe { get; set; }
        public string AwardsAndRecognization { get; set; }
        public string Affiliation { get; set; }
        //    public string ClinicServices { get; set; }
        //public string AboutClinic { get; set; }
        //      public string ClinicName { get; set; }
        //      public int ClinicFees { get; set; }
        public string RegistrationNo { get; set; }
        //  public string FolderFilePath { get; set; }


        public List<Media> getmedias { get; set; }
        //   public List<GetService> getservices { get; set; }
        public List<GetAwards> getallawards { get; set; }
        public List<GetAffiliations> getallaffiliations { get; set; }
        public List<GetRegistrations> getallregistrations { get; set; }

        ////first seating time
        //public string StartTimefs1 { get; set; }
        //public string EndTimefs1 { get; set; }
        //public string StartTimefs2 { get; set; }
        //public string EndTimefs2 { get; set; }
        //public string StartTimefs3 { get; set; }
        //public string EndTimefs3 { get; set; }
        //public string StartTimefs4 { get; set; }
        //public string EndTimefs4 { get; set; }
        //public string StartTimefs5 { get; set; }
        //public string EndTimefs5 { get; set; }
        //public string StartTimefs6 { get; set; }
        //public string EndTimefs6 { get; set; }
        //public string StartTimefs7 { get; set; }
        //public string EndTimefs7 { get; set; }

        ////Second seating time
        //public string StartTimess1 { get; set; }
        //public string EndTimess1 { get; set; }
        //public string StartTimess2 { get; set; }
        //public string EndTimess2 { get; set; }
        //public string StartTimess3 { get; set; }
        //public string EndTimess3 { get; set; }
        //public string StartTimess4 { get; set; }
        //public string EndTimess4 { get; set; }
        //public string StartTimess5 { get; set; }
        //public string EndTimess5 { get; set; }
        //public string StartTimess6 { get; set; }
        //public string EndTimess6 { get; set; }
        //public string StartTimess7 { get; set; }
        //public string EndTimess7 { get; set; }

        ////third seating time
        //public string StartTimets1 { get; set; }
        //public string EndTimets1 { get; set; }
        //public string StartTimets2 { get; set; }
        //public string EndTimets2 { get; set; }
        //public string StartTimets3 { get; set; }
        //public string EndTimets3 { get; set; }
        //public string StartTimets4 { get; set; }
        //public string EndTimets4 { get; set; }
        //public string StartTimets5 { get; set; }
        //public string EndTimets5 { get; set; }
        //public string StartTimets6 { get; set; }
        //public string EndTimets6 { get; set; }
        //public string StartTimets7 { get; set; }
        //public string EndTimets7 { get; set; }
    }
}
