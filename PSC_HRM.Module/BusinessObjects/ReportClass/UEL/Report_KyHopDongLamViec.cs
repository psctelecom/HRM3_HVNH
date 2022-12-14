using System;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [VisibleInReports(true)]
    [ModelDefault("Caption", "Soạn hợp đồng làm việc")]
    public class Report_KyHopDongLamViec : StoreProcedureReport, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _NhanVien;
        private ChucVu _ChucVu;
        private ThongTinNhanVien _NguoiKy;

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn cán bộ")]
        [DataSourceProperty("BoPhan.ThongTinNhanVienList")]
        [ImmediatePostData]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ người ký")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn chức vụ người ký")]
        [ImmediatePostData]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);

                if(!IsLoading)
                {
                    NguoiKy = null;
                    UpdateNVList();
                }
            }
        }

        [ModelDefault("Caption", "Người ký")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn người ký")]
        [DataSourceProperty("NVList")]
        [ImmediatePostData]
        public ThongTinNhanVien NguoiKy
        {
            get
            {
                return _NguoiKy;
            }
            set
            {
                SetPropertyValue("NguoiKy", ref _NguoiKy, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        public Report_KyHopDongLamViec(Session session) : base(session) { }

        public override System.Data.SqlClient.SqlCommand CreateCommand()
        {
            return null;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ChucVu = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu=?", "Hiệu Trưởng"));
        }
    }

}
