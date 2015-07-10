using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMD.EntityModel.Doctor;
using MCMD.EntityModel.Administration;

namespace MCMD.ViewModel.doctor
{
    public class ClinicProfileViewModel
    {
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
    }
}
