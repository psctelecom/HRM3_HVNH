using System;
using System.Collections.Generic;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler;

namespace PSC_HRM.Module.BanLamViec
{
    public class CustomAppointmentFormController : AppointmentFormController
    {
        public NhomCongViec NhomCongViec
        {
            get
            {
                return (NhomCongViec)EditedAppointmentCopy.CustomFields["NhomCongViec"];
            }
            set
            {
                EditedAppointmentCopy.CustomFields["NhomCongViec"] = value;
            }
        }

        NhomCongViec SourceNhomCongViec
        {
            get
            {
                return (NhomCongViec)SourceAppointment.CustomFields["SourceNhomCongViec"];
            }
            set
            {
                SourceAppointment.CustomFields["SourceNhomCongViec"] = value;
            }
        }

        public CustomAppointmentFormController(SchedulerControl control, Appointment apt)
            : base(control, apt)
        { }

        public override bool IsAppointmentChanged()
        {
            if (base.IsAppointmentChanged())
                return true;
            return SourceNhomCongViec != NhomCongViec;
        }
    }
}
