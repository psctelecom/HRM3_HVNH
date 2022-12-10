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


namespace PSC_HRM.Module.ThuNhap.Luong
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết Tổng hợp tiền lương tháng")]
    public class Luong_BangTongHopTienLuongThangItem : BaseObject, ISupportController
    {
        private string _CacKhoan;
        private decimal _SoTien;

        [ModelDefault("Caption", "Các khoản")]
        public string CacKhoan
        {
            get
            {
                return _CacKhoan;
            }
            set
            {
                SetPropertyValue("CacKhoan", ref _CacKhoan, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        public Luong_BangTongHopTienLuongThangItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
