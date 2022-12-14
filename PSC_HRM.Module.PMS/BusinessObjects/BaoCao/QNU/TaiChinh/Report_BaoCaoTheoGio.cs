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
using PSC_HRM.Module.PMS.Enum;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Báo cáo thanh toán thù lao (Theo loại giờ)")]
    public class Report_BaoCaoTheoGio : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private KyTinhPMS _KyTinhPMS;

        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private LoaiGioThanhToanEnum _LoaiGioThanhToan;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }
        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                {
                    if (NamHoc != null)
                        updateKyPMS();
                    else
                        KyTinhPMSList.Reload();
                }
            }
        }
        [ModelDefault("Caption", "Đợt tính thù lao")]
        [DataSourceProperty("KyTinhPMSList")]
        public KyTinhPMS KyTinhPMS
        {
            get
            {
                return _KyTinhPMS;
            }
            set
            {
                SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value);
            }
        }
        #region Kỳ tính
        [Browsable(false)]
        [ModelDefault("Caption", "Kỳ PMS List")]
        public XPCollection<KyTinhPMS> KyTinhPMSList
        {
            get;
            set;
        }
        void updateKyPMS()
        {
            if (NamHoc != null)
            {
                KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            }
            else
                KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            OnChanged("KyTinhPMSList");
        }
        #endregion
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Cá nhân")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }


        [ModelDefault("Caption", "Loại giờ")]
        public LoaiGioThanhToanEnum LoaiGioThanhToan
        {
            get { return _LoaiGioThanhToan; }
            set { SetPropertyValue("LoaiGioThanhToan", ref _LoaiGioThanhToan, value); }
        }
        public Report_BaoCaoTheoGio(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_TaiChinh_LayDuLieuThanhToan_TheoLoaiGio", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@KyTinhPms", KyTinhPMS.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);

                da.SelectCommand.Parameters.AddWithValue("@LoaiGioThanhToan", LoaiGioThanhToan);
                da.Fill(DataSource);
            }
        }
    }

}