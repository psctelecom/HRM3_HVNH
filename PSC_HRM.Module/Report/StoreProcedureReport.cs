using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;


namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    public abstract class StoreProcedureReport : BaseObject
    {
        private DateTime _NgayLapBaoCao;
        private string _NguoiLap;
        private string _TrinhDoNguoiLap;
        private string _HieuTruong;
        private string _KeToanTruong;
        private string _TrinhDoHieuTruong;


        public static StoreProcedureReport Param;

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "HeaderAndFooter")]
        public DataSet HeaderAndFooter { get; set; }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "MainData")]
        public DataSet DataSource { get; set; }

        [ModelDefault("Caption", "Ngày lập báo cáo")]
        public DateTime NgayLapBaoCao
        {
            get
            {
                return _NgayLapBaoCao;
            }
            set
            {
                SetPropertyValue("NgayLapBaoCao", ref _NgayLapBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Người lập")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string NguoiLap
        {
            get
            {
                return _NguoiLap;
            }
            set
            {
                SetPropertyValue("NguoiLap", ref _NguoiLap, value);
            }
        }
        [ModelDefault("Caption", "Trình độ chuyên môn người lập")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string TrinhDoNguoiLap
        {
            get
            {
                return _TrinhDoNguoiLap;
            }
            set
            {
                SetPropertyValue("TrinhDoNguoiLap", ref _TrinhDoNguoiLap, value);
            }
        }
        [ModelDefault("Caption", "Trình độ chuyên môn hiệu trưởng")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string TrinhDoHieuTruong
        {
            get
            {
                return _TrinhDoHieuTruong;
            }
            set
            {
                SetPropertyValue("TrinhDoHieuTruong", ref _TrinhDoHieuTruong, value);
            }
        }
        [ModelDefault("Caption", "Hiệu trưởng")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string HieuTruong
        {
            get
            {
                return _HieuTruong;
            }
            set
            {
                SetPropertyValue("HieuTruong", ref _HieuTruong, value);
            }
        }

        [ModelDefault("Caption", "Kế toán trưởng")]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        public string KeToanTruong
        {
            get
            {
                return _KeToanTruong;
            }
            set
            {
                SetPropertyValue("KeToanTruong", ref _KeToanTruong, value);
            }
        }

        public StoreProcedureReport(Session session) : base(session) { }

        public abstract SqlCommand CreateCommand();

        public virtual void FillDataSource()
        {
            SqlCommand cmd = CreateCommand();
            DataSource = DataProvider.GetDataSet(cmd);
        }

        private void GetDataHeaderAndFooter()
        {
            HeaderAndFooter = new DataSet();
            //
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ThongTinNhanVien", HamDungChung.CurrentUser().ThongTinNhanVien != null ? HamDungChung.CurrentUser().ThongTinNhanVien.Oid : Guid.Empty);
                param[1] = new SqlParameter("@OidNguoiSuDung", HamDungChung.CurrentUser().Oid);

                SqlCommand cmdHeaderAndFooter = DataProvider.GetCommand("spd_ReportHeaderAndFooter", System.Data.CommandType.StoredProcedure, param);

                using (SqlDataAdapter da = new SqlDataAdapter(cmdHeaderAndFooter))
                {
                    da.SelectCommand.Connection = (SqlConnection)Session.Connection;
                    //
                    da.Fill(HeaderAndFooter);
                }
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //Khởi tạo dataset
            DataSource = new DataSet();
            //Lấy ngày hiện tại
            NgayLapBaoCao = HamDungChung.GetServerTime();
            //Lấy dữ liệu header và footer
            GetDataHeaderAndFooter();
            ThongTinNhanVien ThongTin = HamDungChung.CurrentUser().ThongTinNhanVien;
            if (ThongTin != null)
            {
                NguoiLap = ThongTin.HoTen;
                if (ThongTin.NhanVienTrinhDo != null && ThongTin.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                    TrinhDoNguoiLap = ThongTin.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon;
            }
            NguoiLap = HamDungChung.CurrentUser().ThongTinNhanVien != null ? HamDungChung.CurrentUser().ThongTinNhanVien.HoTen : "......................................................";
            string HocHam = "";
            string TrinhDo = "";
            ThongTinNhanVien tt = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.MaQuanLy = 'HT'"));


            ThongTinNhanVien ttKeToanTruong = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu.MaQuanLy = 'KTT'"));


            if(ttKeToanTruong!=null)
            {
                KeToanTruong = ttKeToanTruong.HoTen;
            }
            if (tt != null)
            {
                if (tt.NhanVienTrinhDo != null && tt.NhanVienTrinhDo.HocHam != null)
                {
                    HocHam = tt.NhanVienTrinhDo.HocHam.MaQuanLy + ".";
                }
                if (tt.NhanVienTrinhDo != null && tt.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                {
                    TrinhDo = tt.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy + ". ";
                }
                TrinhDoHieuTruong = HocHam + TrinhDo;
                HieuTruong = HocHam + TrinhDo + tt.HoTen;
            }
        }
    }
}
