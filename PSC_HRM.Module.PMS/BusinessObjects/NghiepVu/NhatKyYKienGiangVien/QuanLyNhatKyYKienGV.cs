using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.PMS.NghiepVu.NhatKyYKienGiangVien
{
    [ModelDefault("Caption", "Quản lý nhật ký ý kiến GV")]
    [DefaultProperty("ThongTin")]
    public class QuanLyNhatKyYKienGV : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private int _Lan;

     
        [ModelDefault("Caption", "Thông tin trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }


        [ModelDefault("Caption", "Lần")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public int Lan
        {
            get { return _Lan; }
            set { SetPropertyValue("Lan", ref _Lan, value); }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", " - Đợt " + Lan.ToString());
            }
        }

        [Aggregated]
        [Association("QuanLyNhatKyYKienGV-ListThongTinTongYKienGV")]
        [ModelDefault("Caption", "Thông tin tổng")]
        public XPCollection<ThongTinTongYKienGV> ListThongTinTongYKienGV
        {
            get
            {
                return GetCollection<ThongTinTongYKienGV>("ListThongTinTongYKienGV");
            }
        }

        public QuanLyNhatKyYKienGV(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}