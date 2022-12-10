using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    public class Curriculum
    {
        [DisplayName("Tên môn")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
