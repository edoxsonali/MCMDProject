using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MCMD.EntityModel.Administration
{
    public class ShortDoctorReg
    {
        [Key]
        public int DocRegId { get; set; }

        [DisplayName("Title")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Title is not valid.")]
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(30, ErrorMessage = "Title cannot be longer than 30 characters.")]
        public string Title { get; set; }

        [DisplayName("First Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "First Name is not valid.")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Last Name is not valid.")]
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DisplayName("Speciality")]
        [RegularExpression(@"^([a-zA-Z]+(([\s|\x27|\.][a-zA-Z]+)*([\.][\s][a-zA-Z]+)*)*[\s]*)$", ErrorMessage = "Speciality is not valid.")]
        [Required(ErrorMessage = "Speciality is required.")]
        [StringLength(50, ErrorMessage = "Speciality cannot be longer than 50 characters.")]
        public string Speciality { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string EmailId { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Phone is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public int Phone { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [System.ComponentModel.DataAnnotations.CompareAttribute("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedById { get; set; }
    }
}
