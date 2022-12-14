using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Tạm ứng thu nhập tăng thêm (quý)")]
    public class Report_ThuNhapTangThem_BangTinhTamUngThuNhapTangThemTheoQuy : StoreProcedureReport
    {
        private PhanLoaiCanBoEnum _PhanLoai;
        private int _Nam;
        private QuyEnum _Quy;

        [ModelDefault("Caption", "Quý")]
        public QuyEnum Quy
        {
            get
            {
                return _Quy;
            }
            set
            {
                SetPropertyValue("Quy", ref _Quy, value);
            }
        }

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public PhanLoaiCanBoEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        public Report_ThuNhapTangThem_BangTinhTamUngThuNhapTangThemTheoQuy (Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Quy = QuyEnum.QuyI;
            DateTime current = HamDungChung.GetServerTime();
            Nam = current.Year;
        }

        public override SqlCommand CreateCommand()
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Quy", Quy);
            param[1] = new SqlParameter("@Nam", Nam);
            param[2] = new SqlParameter("@PhanLoai", PhanLoai);

            SqlCommand cmd = DataProvider.GetCommand("spd_Report_ThuNhapTangThem_TamUngThuNhapTangThemTheoQuy", 
                System.Data.CommandType.StoredProcedure,
                param);
            return cmd;
        }
    }

}
