using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Doctor
{
    public class DoctorClinicInformation
    {
        [Key]

        public int ClinicInfoId { get; set; }

        public int LoginId { get; set; }

        [DisplayName("ClinicName")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicName is required.")]
        [StringLength(50, ErrorMessage = "ClinicName cannot be longer than 50 characters.")]
        public string ClinicName { get; set; }

        [DisplayName("ClinicAddress")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicAddress is required.")]
        [StringLength(50, ErrorMessage = "ClinicAddress cannot be longer than 100 characters.")]
        public string ClinicAddress { get; set; }

        [DisplayName("ClinicPhoneNo")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicPhoneNo is required.")]
        public string ClinicPhoneNo { get; set; }

        [DisplayName("ClinicFees")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicFees is required.")]
        public int ClinicFees { get; set; }

        [DisplayName("ClinicTime From")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicTime From is required.")]
        public DateTime ClinicTimeFrom { get; set; }

        [DisplayName("ClinicTime To ")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicTime To is required.")]
        public DateTime ClinicTimeTo { get; set; }

        [DisplayName("Country")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [DisplayName("State")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [DisplayName("City")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [DisplayName("ZipCode")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ZipCode is required.")]
        public int ZipCode { get; set; }

        [DisplayName("ClinicServices")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "ClinicServices is required.")]
        public string ClinicServices { get; set; }

        [DisplayName("Awards And Recognization")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "Award sAnd Recognization is required.")]
        public string AwardsAndRecognization { get; set; }

        [DisplayName("About Clinic")]
        //[RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "ClinicName is not valid.")]
        [Required(ErrorMessage = "About Clinic is required.")]
        public string AboutClinic { get; set; }

        public string InactiveFlag { get; set; }
        public int CreatedByID { get; set; }


        public System.DateTime CreatedDate { get; set; }

        public int ModifiedByID { get; set; }

        public System.DateTime ModifiedDate { get; set; }

    }
}
