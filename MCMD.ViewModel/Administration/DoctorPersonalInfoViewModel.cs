using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Administration;
using MCMD.EntityModel.Doctor;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


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

        [DisplayName("SpecialitiID")]
        [Required(ErrorMessage = "Speciality is required.")]
        public int SpecialityID { get; set; }
        public int PersonalInfoId { get; set; }
        public int LoginId { get; set; }
        public int LoginSpecialityId { get; set; }

        [DisplayName("First Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "First Name is not valid.")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        //[DisplayName("MiddleName")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Middle Name is not valid.")]
        //[Required(ErrorMessage = "Middle Name is required.")]
        //[StringLength(50, ErrorMessage = "Middle Name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Last Name is not valid.")]
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DisplayName("SpecialitiID")]
        [Required(ErrorMessage = "Speciality is required.")]
        public string SpecialitiID { get; set; }

        [DisplayName("EmailID")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string EmailID { get; set; }

        [DisplayName("UserPhone")]
        [Required(ErrorMessage = "Phone is required.")]
        public string UserPhone { get; set; }

        [DisplayName("Qualification")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Qualification is not valid.")]
        [Required(ErrorMessage = "Qualification is required.")]
      //  [StringLength(50, ErrorMessage = "Qualification cannot be longer than 50 characters.")]
        public string Qualification { get; set; }

       
        public string Qualification1 { get; set; }


        [DisplayName("RegistrationNo")]
        [Required(ErrorMessage = "RegistrationNo is required.")]
        public string RegistrationNo { get; set; }

        public string Affiliation { get; set; }
        public string AboutMe { get; set; }

        public string AboutExperience { get; set; }
        public string InactiveFlag { get; set; }

        public int CreatedByID { get; set; }


        public System.DateTime CreatedDate { get; set; }

        public int ModifiedByID { get; set; }

        public System.DateTime ModifiedDate { get; set; }
        public string FolderFilePath { get; set; }

        public string UploadType { get; set; }


    }
}
