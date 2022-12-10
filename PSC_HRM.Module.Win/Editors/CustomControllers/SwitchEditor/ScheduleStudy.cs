using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PSC_HRM.Module.Win.CustomControllers.SwitchEditor
{
    internal class ScheduleStudy
    {
        [Browsable(false)]
        public string ID { get; set; }

        [DisplayName("Tên lớp học phần")]
        public string Alias { get; set; }

        [DisplayName("Tên lớp")]
        public string Name { get; set; }

        public override string ToString()
        {
            return ID;
        }
    }
}
