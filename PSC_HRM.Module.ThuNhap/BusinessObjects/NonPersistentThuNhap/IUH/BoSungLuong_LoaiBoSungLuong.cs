using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using System.Data;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn loại bổ sung lương")]
    public class BoSungLuong_LoaiBoSungLuong : BaseObject
    {
        LoaiBoSungLuongEnum _LoaiBoSungLuong;

        [ModelDefault("Caption", "Loại bổ sung lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public LoaiBoSungLuongEnum LoaiBoSungLuong
        {
            get
            {
                return _LoaiBoSungLuong;
            }
            set
            {
                SetPropertyValue("LoaiBoSungLuong", ref _LoaiBoSungLuong, value);
            }
        }
        public BoSungLuong_LoaiBoSungLuong(Session session)
            : base(session)
        { }

    }

}
