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

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán thù lao đợt 3")]
    public class Report_BangThanhToanDot3 : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private KyTinhPMS _KyTinhPMS;

        private BoPhan _BoPhan;
        private string _TimKiem;
        private NhanVien _NhanVien;
        //private HocHam _HocHam;
        //private TrinhDoChuyenMon _TrinhDoChuyenMon;
        //private LoaiNhanSu _LoaiNhanSu;
        //private LoaiNhanVienEnum _LoaiNhanVienEnum;


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
                if(!IsLoading)
                {
                    if (NamHoc != null)
                        KyTinhPMS = Session.FindObject<KyTinhPMS>(CriteriaOperator.Parse("NamHoc =? and Dot =3", NamHoc.Oid));
                }
            }
        }
        [ModelDefault("Caption", "Đợt tính thù lao")]
        [ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Tìm kiếm")]
        public string TimKiem
        {
            get { return _TimKiem; }
            set
            {
                SetPropertyValue("TimKiem", ref _TimKiem, value);
                if (!IsLoading)
                {
                    if (!string.IsNullOrEmpty(TimKiem))
                    {
                        NhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("(MaQuanLy like ? or CMND like ?)", "%" + TimKiem + "%", "%" + TimKiem + "%"));
                    }
                }
            }
        }
        [ModelDefault("Caption", "Cá nhân")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading)
                    if (NhanVien != null)
                        BoPhan = NhanVien.BoPhan;
            }
        }
        public Report_BangThanhToanDot3(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_TaiChinh_LayDuLieuBangChot_Dot3", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@KyTinhPms", KyTinhPMS.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
