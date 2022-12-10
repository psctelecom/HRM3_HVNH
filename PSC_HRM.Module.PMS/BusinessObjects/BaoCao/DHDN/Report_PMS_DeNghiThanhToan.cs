using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Đề nghị thanh toán tiền giảng")]
    public class Report_PMS_DeNghiThanhToan : StoreProcedureReport
    {
        private QuanLyDeNghi _QuanLyDeNghi;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private ChiTietKhoiLuongGiangDay _ChiTietKhoiLuongGiangDay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý đề nghị")]
        public QuanLyDeNghi QuanLyDeNghi
        {
            get
            {
                return _QuanLyDeNghi;
            }
            set
            {
                SetPropertyValue("QuanLyDeNghi", ref _QuanLyDeNghi, value);
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

        [ModelDefault("Caption", "chi tiết klgv")]
        [Browsable(false)]
        public ChiTietKhoiLuongGiangDay ChiTietKhoiLuongGiangDay
        {
            get { return _ChiTietKhoiLuongGiangDay; }
            set { SetPropertyValue("ChiTietKhoiLuongGiangDay", ref _ChiTietKhoiLuongGiangDay, value); }
        }
        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList
        { get; set; }

        public Report_PMS_DeNghiThanhToan(Session session) : base(session) { }

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
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_DeNghiThanhToan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangQuanLy", (object)(QuanLyDeNghi == null ? Guid.Empty : QuanLyDeNghi.Oid));
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@ChiTietKhoiLuong", ChiTietKhoiLuongGiangDay == null ? Guid.Empty : ChiTietKhoiLuongGiangDay.Oid);               
                da.Fill(DataSource);
            }
        }
    }
}
