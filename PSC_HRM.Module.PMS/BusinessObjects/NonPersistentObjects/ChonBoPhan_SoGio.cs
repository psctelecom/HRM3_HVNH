using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects
{
    [NonPersistent]
    public class ChonBoPhan_SoGio : BaseObject
    {
        private string _BoPhan;
        private decimal _SoGioDinhMuc;

        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.chkComboxEdit_ChonBoPhan")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Định mức giờ giảng dạy")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ImmediatePostData]
        public decimal SoGioDinhMuc
        {
            get { return _SoGioDinhMuc; }
            set { SetPropertyValue("SoGioDinhMuc", ref _SoGioDinhMuc, value); }
        }
    
        public ChonBoPhan_SoGio(Session session) : base(session) { }
        
    }
}
