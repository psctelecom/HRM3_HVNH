using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp;

namespace PSC_HRM.Module.Win.QuyTrinh
{
    public partial class TheoDoiBaseController : BaseController
    {
        public XafApplication app;
        public IObjectSpace obs;

        public TheoDoiBaseController()
        {
            InitializeComponent();
        }

        public TheoDoiBaseController(XafApplication app, IObjectSpace obs)
        {
            InitializeComponent();
            this.app = app;
            this.obs = obs;
        }

        protected virtual void GetDuLieu()
        { }
    }
}
