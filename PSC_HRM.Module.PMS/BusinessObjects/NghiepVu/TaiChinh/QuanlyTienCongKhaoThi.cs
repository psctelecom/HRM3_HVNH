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

    [ModelDefault("Caption", "Quản lý tiền công khảo thí")]
    [DefaultProperty("Caption")]
    public class QuanlyTienCongKhaoThi : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private DateTime _NgayLuu;
       
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [ImmediatePostData]
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

        [ModelDefault("Caption", "Ngày")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayLuu
        {
            get { return _NgayLuu; }
            set
            {
                SetPropertyValue("NgayLuu", ref _NgayLuu, value);
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1} {2}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", NgayLuu != null ? " - Ngày " + NgayLuu.ToString("dd/MM/yyyy") : "");
            }
        }

        [Aggregated]
        [Association("QuanlyTienCongKhaoThi-ListChiTietTienCongKhaoThi")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietTienCongKhaoThi> ListChiTietTienCongKhaoThi
        {
            get
            {
                return GetCollection<ChiTietTienCongKhaoThi>("ListChiTietTienCongKhaoThi");
            }
        }
        public QuanlyTienCongKhaoThi(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            NgayLuu = DateTime.Now;
        }        
    }
}
