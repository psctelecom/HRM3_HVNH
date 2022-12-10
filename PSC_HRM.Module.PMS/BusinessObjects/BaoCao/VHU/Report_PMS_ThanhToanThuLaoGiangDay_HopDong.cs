using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Hợp đồng thù lao giảng dạy")]
    public class Report_PMS_ThanhToanThuLaoGiangDay_HopDong : StoreProcedureReport
    {
        //private ThongTinTruong _ThongTinTruong;
        //private NamHoc _NamHoc;
        //private HocKy _HocKy;
        //private BoPhan _DonVi;
        //private LoaiNhanVien _LoaiNhanVien;

        //[ModelDefault("Caption", "Trường")]
        //[ModelDefault("AllowEdit", "false")]
        //public ThongTinTruong ThongTinTruong
        //{
        //    get { return _ThongTinTruong; }
        //    set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        //}
        //[ModelDefault("Caption", "Năm học")]
        //[DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        //public NamHoc NamHoc
        //{
        //    get { return _NamHoc; }
        //    set
        //    {
        //        SetPropertyValue("NamHoc", ref _NamHoc, value);
        //        if (value != null)
        //        {
        //            HocKy = null;
        //        }
        //    }
        //}
        //[ModelDefault("Caption", "Học kỳ")]
        //[DataSourceProperty("NamHoc.ListHocKy")]
        //public HocKy HocKy
        //{
        //    get { return _HocKy; }
        //    set { SetPropertyValue("HocKy", ref _HocKy, value); }
        //}

        //[ModelDefault("Caption", "Đơn vị")]
        //public BoPhan DonVi
        //{
        //    get { return _DonVi; }
        //    set { SetPropertyValue("DonVi", ref _DonVi, value); }
        //}

        //[ModelDefault("Caption", "Loại giảng viên")]
        //public LoaiNhanVien LoaiNhanVien
        //{
        //    get { return _LoaiNhanVien; }
        //    set { SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value); }
        //}

        private string _SQL;
        [ModelDefault("Caption", "Dữ liệu in")]
        [Size(-1)]
        public string SQL
        {
            get { return _SQL; }
            set { SetPropertyValue("SQL", ref _SQL, value); }
        }

        public Report_PMS_ThanhToanThuLaoGiangDay_HopDong(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThanhToanThuLaoGiangDay_ChayStore_HopDong", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Sql", SQL);
                da.SelectCommand.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().Oid);
                da.Fill(DataSource);
            }
            //using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_ThanhToanThuLaoGiangDay_CT_Tong", (SqlConnection)Session.Connection))
            //{
            //    //da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //    //da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
            //    da.Fill(DataSource);
            //}
        }
        
    }
}
