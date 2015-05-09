using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Doctor
{
    public class Help
    {
        public int HelpId {get;set;} 
	    public int docId {get;set;}
	    public string DocName {get;set;}
	    public string Subject {get;set;}
        public string Description { get; set; }
    }
}
