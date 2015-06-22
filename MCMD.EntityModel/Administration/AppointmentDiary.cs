using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
   public class AppointmentDiary
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int SomeImportantKey { get; set; }
        public System.DateTime DateTimeScheduled { get; set; }
        public int AppointmentLength { get; set; }
        public int StatusENUM { get; set; }
    }
}
