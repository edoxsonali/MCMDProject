﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Doctor
{
    public class DoctorPersonalInformation
    {
        [Key]
        public int PersonalInfoId { get; set; }

        public int LoginId { get; set; }



      //  [DisplayName("MiddleName")]
        // [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "MiddleName is not valid.")]
      //  [Required(ErrorMessage = "MiddleName is required.")]
      //  [StringLength(50, ErrorMessage = "MiddleName cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }

        [DisplayName("Qualification")]
        // [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Qualification is not valid.")]
        [Required(ErrorMessage = "Qualification is required.")]
       // [StringLength(50, ErrorMessage = "Qualification cannot be longer than 50 characters.")]
        public string Qualification { get; set; }

      
        public string Qualification1 { get; set; }


        [DisplayName("Registration No")]
        [Required(ErrorMessage = "Registration No is required.")]
        public string RegistrationNo { get; set; }

       // [DisplayName("Affiliation")]
        // [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Affiliation is not valid.")]
      //  [Required(ErrorMessage = "Affiliation is required.")]
       // [StringLength(100, ErrorMessage = "Affiliation cannot be longer than 100 characters.")]
        public string Affiliation { get; set; }


        public string AboutMe { get; set; }

        public string ExperienceInYear { get; set; }
        public string ExperienceInMonth { get; set; }

        public string InactiveFlag { get; set; }

        public int CreatedByID { get; set; }


        public System.DateTime CreatedDate { get; set; }

        public int ModifiedByID { get; set; }

        public System.DateTime ModifiedDate { get; set; }

        public string FolderFilePath { get; set; }

        public string UploadType { get; set; }
    }
}
