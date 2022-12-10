using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao
{

    [ModelDefault("Caption", "Quản lý tổng hợp giờ giảng")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "NamHoc", "Bảng quản lý tổng hợp giờ giảng đã tồn tại")]
    public class QuanLyGioGiang : BaseObject
    {
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "");
            }
        }
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Trường")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [Aggregated]
        [Association("QuanLyGioGiang-ListNhanVien_GioGiang")]
        [ModelDefault("Caption", "Chi tiết giờ giảng")]
        public XPCollection<NhanVien_GioGiang> ListNhanVien_GioGiang
        {
            get
            {
                return GetCollection<NhanVien_GioGiang>("ListNhanVien_GioGiang");
            }
        }
        public QuanLyGioGiang(Session session)
            : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            // Place here your initialization code.
        }
    }

}