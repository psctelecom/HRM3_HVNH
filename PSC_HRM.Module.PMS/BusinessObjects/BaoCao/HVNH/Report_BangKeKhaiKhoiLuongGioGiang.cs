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

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thống kê khối lượng giảng dạy")]
    public class Report_BangKeKhaiKhoiLuongGioGiang : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private LoaiPhanVien _LoaiPhanVien;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn năm học")]
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
                if(value != null)
                {
                    HocKy = null;                  
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Phân viện")]
        [ImmediatePostData]
        public LoaiPhanVien LoaiPhanVien
        {
            get { return _LoaiPhanVien; }
            set
            {
                SetPropertyValue("LoaiPhanVien", ref _LoaiPhanVien, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Loại hình đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }

        public Report_BangKeKhaiKhoiLuongGioGiang(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_BangKeKhaiKhoiLuongGioGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@PhanVien", LoaiPhanVien == null ? Guid.Empty : LoaiPhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BacDaoTao", BacDaoTao == null ? Guid.Empty : BacDaoTao.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HeDaoTao", HeDaoTao == null ? Guid.Empty : HeDaoTao.Oid);
                da.Fill(DataSource);
            }
        }
    }

}
