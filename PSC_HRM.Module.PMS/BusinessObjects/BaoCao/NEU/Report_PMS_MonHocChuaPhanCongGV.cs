using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.BaoCao.UFM
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng kê khai môn học chưa phân công giảng viên")]
    public class Report_PMS_MonHocChuaPhanCongGV : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _BoPhan;
        private Bac_HeDaoTao _Bac_HeDaoTao;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                    if (NamHoc != null)
                        HocKy = Session.FindObject<HocKy>(CriteriaOperator.Parse("MaQuanLy =? and NamHoc =?", "HK01", NamHoc.Oid));
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        #region
        private BoPhan _KhoaVien;
        [ImmediatePostData]
        [ModelDefault("Caption", "Khoa")]
        [DataSourceCriteria("LoaiBoPhan = 4")]
        //[VisibleInDetailView(false)]
        public BoPhan KhoaVien
        {
            get { return _KhoaVien; }
            set
            {
                SetPropertyValue("KhoaVien", ref _KhoaVien, value);
                if (!IsLoading && value != null)
                {
                    LoadListBP();
                }
            }
        }
        #endregion

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        [DataSourceProperty("listBoPhan")]
        //[VisibleInDetailView(false)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan != null && KhoaVien == null)
                        KhoaVien = BoPhan.BoPhanCha;
                }
            }
        }

        [ModelDefault("Caption", "Bậc hệ đâo tạo")]
        [ImmediatePostData]
        //[VisibleInDetailView(false)]
        public Bac_HeDaoTao Bac_HeDaoTao
        {
            get { return _Bac_HeDaoTao; }
            set
            {
                SetPropertyValue("Bac_HeDaoTao", ref _Bac_HeDaoTao, value);
            }
        }

        [Browsable(false)]
        private XPCollection<BoPhan> listBoPhan
        {
            get;
            set;
        }
        void LoadListBP()
        {
            listBoPhan.Reload();
            if (KhoaVien != null)
                listBoPhan = new XPCollection<BoPhan>(Session, CriteriaOperator.Parse("BoPhanCha =?", KhoaVien.Oid));
            else
                listBoPhan = new XPCollection<BoPhan>(Session);
            OnChanged("listBoPhan");
        }
        public Report_PMS_MonHocChuaPhanCongGV(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listBoPhan = new XPCollection<BoPhan>(Session, false);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_MonHocChuaPhanCongGV", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@KhoaVien", KhoaVien == null ? Guid.Empty : KhoaVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BacHeDaoTao", Bac_HeDaoTao == null ? Guid.Empty : Bac_HeDaoTao.Oid);
                da.Fill(DataSource);
            }
        }
    }
}
