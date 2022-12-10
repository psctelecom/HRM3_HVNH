using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Quản lý thanh toán bổ sung")]
    [DefaultProperty("Caption")]
    [Appearance("NEU_Hide", TargetItems = "HocKy;KyTinhPMS", Visibility = ViewItemVisibility.Hide)]
    public class QuanLyThanhToanBoSung : ThongTinChungPMS
    {

        [Aggregated]
        [Association("QuanLyThanhToanBoSung-listChiTiet")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietThanhToanBoSung> listChiTiet
        {
            get
            {
                return GetCollection<ChiTietThanhToanBoSung>("listChiTiet");
            }
        }

        public QuanLyThanhToanBoSung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }       
    }
}
