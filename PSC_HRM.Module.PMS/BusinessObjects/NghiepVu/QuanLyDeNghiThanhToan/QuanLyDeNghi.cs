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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [Appearance("Hide_HeSo_HVNH", TargetItems = "HocKy;KyTinhPMS"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'DNU'")]
    [ModelDefault("Caption", "Quản lý số đề nghị")]
    [DefaultProperty("Caption")]
    public class QuanLyDeNghi : ThongTinChungPMS
    {     

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Năm học  {1}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "");      
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [Association("QuanLyDeNghi-ListChiTietDeNghi")]
        public XPCollection<ChiTietDeNghi> ListChiTietDeNghi
        {
            get
            {
                return GetCollection<ChiTietDeNghi>("ListChiTietDeNghi");
            }
        }
        public QuanLyDeNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }       
    }
}
