using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
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
    [ModelDefault("Caption", "Báo cáo: Hợp dồng giảng dạy")]
    public class Report_PMS_HopDongGiangDay : StoreProcedureReport
    {
        private HopDong_ThanhLy_ThinhGiang _QuanLy;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý")]
        public HopDong_ThanhLy_ThinhGiang QuanLy
        {
            get
            {
                return _QuanLy;
            }
            set
            {
                SetPropertyValue("QuanLy", ref _QuanLy, value);
            }
        }


        [ModelDefault("Caption", "Đơn vị")]
        [Browsable(false)]
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
        [Browsable(false)]
        [DataSourceProperty("NhanVienList")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList
        { get; set; }

        public Report_PMS_HopDongGiangDay(Session session) : base(session) { }
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
            if (NhanVienList != null)
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
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_ThanhLy_HopDongThinhGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLy", QuanLy == null ? Guid.Empty : QuanLy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }

    }
}
