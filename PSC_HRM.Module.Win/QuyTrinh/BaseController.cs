using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class BaseController : XtraUserControl
    {
        public BaseController()
        {
            InitializeComponent();
        }

        public bool IsValidate()
        {
            bool result = dxValidationProvider1.Validate();
            return result;
        }
    }
}
