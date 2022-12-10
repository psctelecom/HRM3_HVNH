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
    [ModelDefault("Caption", "Báo cáo: Bảng kê khai chuyên cá nhân")]
    public class Report_ThuLaoGiangDay_ChuyenCaNhan : StoreProcedureReport
    {
        private BangChotThuLao _BangChotThuLao;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private bool _CoHuu;

        [ImmediatePostData]
        [ModelDefault("Caption", "Bảng chốt thù lao")]
        public BangChotThuLao BangChotThuLao
        {
            get
            {
                return _BangChotThuLao;
            }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Cơ hữu")]
        public bool CoHuu
        {
            get
            {
                return _CoHuu;
            }
            set
            {
                SetPropertyValue("CoHuu", ref _CoHuu, value);
            }
        }

        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList
        { get; set; }

        public Report_ThuLaoGiangDay_ChuyenCaNhan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CoHuu = true;
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
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_ThuLaoGiangDay_ChuyenCaNhan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangChotThuLao", BangChotThuLao == null ? Guid.Empty : BangChotThuLao.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiNhanVien", CoHuu.GetHashCode());
                da.Fill(DataSource);
            }
        }
    }
}
