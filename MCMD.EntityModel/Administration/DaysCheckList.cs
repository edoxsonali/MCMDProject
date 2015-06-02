using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMD.EntityModel.Administration
{
   public  class DaysCheckList
    {
        public int DayscheckId { get; set; }

        public string Days { get; set; }
        public bool DayChecked { get; set; }
    }
}
