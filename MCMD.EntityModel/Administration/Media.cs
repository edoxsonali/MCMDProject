using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MCMD.EntityModel.Administration
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
	    public int LoginId {get;set;}
	    public string FolderFilePath {get;set;}
        public string UploadType { get; set; }
        public string InactiveFlag { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedByID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
      
    }
}
