using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp kê khai giờ giảng")]
    public class Report_TongHopGioGiang : StoreProcedureReport
    {
        private QuanLyGioGiang _QuanLyGioGiang;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private LoaiPhanVien _LoaiPhanVien;
        private LoaiGiangVienEnum _LoaiGiangVienEnum = LoaiGiangVienEnum.TatCa;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý giờ giảng")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn bảng quản lý giờ giảng")]
        public QuanLyGioGiang QuanLyGioGiang
        {
            get
            {
                return _QuanLyGioGiang;
            }
            set
            {
                SetPropertyValue("QuanLyGioGiang", ref _QuanLyGioGiang, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                        UpdateNhanVienList();
            }
        }

        [ModelDefault("Caption", "Cá nhân")]
        [DataSourceProperty("NhanVienList")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }


        [ModelDefault("Caption", "Loại phân viện")]
        public LoaiPhanVien LoaiPhanVien
        {
            get { return _LoaiPhanVien; }
            set { SetPropertyValue("LoaiPhanVien", ref _LoaiPhanVien, value); }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum LoaiGiangVienEnum
        {
            get { return _LoaiGiangVienEnum; }
            set { SetPropertyValue("LoaiGiangVienEnum", ref _LoaiGiangVienEnum, value); }
        }

        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList
        { get; set; }

        public Report_TongHopGioGiang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public void UpdateNhanVienList()
        {
            if(NhanVienList != null)
            {
                NhanVienList.Reload();
            }
            else
            {
                NhanVienList = new XPCollection<NhanVien>(Session, false);
            }
            CriteriaOperator filter = CriteriaOperator.Parse("BoPhan = ?", BoPhan.Oid);
            NhanVienList = new XPCollection<NhanVien>(Session, filter);
            OnChanged("NhanVienList");
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_TongHop_KeKhaiGioGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyGioGiang", QuanLyGioGiang.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiPhanVien", LoaiPhanVien == null ? Guid.Empty : LoaiPhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGiangVienEnum", LoaiGiangVienEnum.GetHashCode());
                da.Fill(DataSource);
            }
        }
    }

}
