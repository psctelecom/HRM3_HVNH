using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL
{
    [DefaultClassOptions]
    [NonPersistent]
    [ModelDefault("Caption","Dữ liệu lập công thức")]
    public class CotChonChauHinhQiuDoi : BaseObject
    {

        public CotChonChauHinhQiuDoi(Session session)
            : base(session)
        {
        }
    }
}