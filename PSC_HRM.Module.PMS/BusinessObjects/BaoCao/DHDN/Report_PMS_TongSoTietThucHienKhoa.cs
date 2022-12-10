using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
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
    [ModelDefault("Caption", "Báo cáo: Tổng số tiết thực hiện Khoa")]
    public class Report_PMS_TongSoTietThucHienKhoa : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BoPhan _BoPhan;
        private bool _CoHuu;

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit","false")]
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
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [DataSourceProperty("BoPhanList")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
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
        public XPCollection<BoPhan> BoPhanList
        { get; set; }
        void UpdateBoPhanList()
        {
            
            BoPhanList = new XPCollection<BoPhan>(Session, CriteriaOperator.Parse("LoaiBoPhan =1"));
            OnChanged("BoPhanList");
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            CoHuu = true;
            UpdateBoPhanList();
            
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_TongSoTietThucHienKhoa", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThongTinTruong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@DonVi", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                da.SelectCommand.Parameters.AddWithValue("@LoaiNhanVien", CoHuu.GetHashCode());
                da.Fill(DataSource);
            }
        }

        public Report_PMS_TongSoTietThucHienKhoa(Session session) : base(session) { }

    }
}
