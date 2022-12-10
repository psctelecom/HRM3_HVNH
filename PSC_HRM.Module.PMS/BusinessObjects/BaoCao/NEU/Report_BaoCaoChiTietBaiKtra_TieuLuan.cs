using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.PMS.Enum;
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
    [ModelDefault("Caption", "Báo cáo: Bảng tổng hợp chi tiết tiền bài ktra, tiểu luận")]
    public class Report_BaoCaoChiTietBaiKtra_TieuLuan : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        //private BoPhan _BoPhan;
        //private NhanVien _NhanVien;


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
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        //private BoPhan _KhoaVien;
        //[ImmediatePostData]
        //[ModelDefault("Caption", "Khoa")]
        //[DataSourceCriteria("LoaiBoPhan = 4")]
        ////[VisibleInDetailView(false)]
        //public BoPhan KhoaVien
        //{
        //    get { return _KhoaVien; }
        //    set
        //    {
        //        SetPropertyValue("KhoaVien", ref _KhoaVien, value);
        //        if (!IsLoading && value != null)
        //        {
        //            LoadListBP();
        //        }
        //    }
        //}
        #endregion
        //[ModelDefault("Caption", "Đơn vị")]
        //[ImmediatePostData]
        //[DataSourceProperty("listBoPhan")]
        ////[VisibleInDetailView(false)]
        //public BoPhan BoPhan
        //{
        //    get { return _BoPhan; }
        //    set
        //    {
        //        SetPropertyValue("BoPhan", ref _BoPhan, value);
        //        if (!IsLoading && value != null)
        //        {
        //            LoadListNV();
        //            if (BoPhan != null && KhoaVien == null)
        //                KhoaVien = BoPhan.BoPhanCha;
        //        }
        //    }
        //}
     
        //[ModelDefault("Caption", "Giảng viên")]
        //[DataSourceProperty("listNhanVien")]
        ////[RuleRequiredField(DefaultContexts.Save)]
        //public NhanVien NhanVien
        //{
        //    get { return _NhanVien; }
        //    set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        //}
        
        //[Browsable(false)]
        //private XPCollection<NhanVien> listNhanVien
        //{
        //    get;
        //    set;
        //}
        //[Browsable(false)]
        //private XPCollection<BoPhan> listBoPhan
        //{
        //    get;
        //    set;
        //}
        //void LoadListNV()
        //{
        //    listNhanVien.Reload();
        //    if (BoPhan != null)
        //        listNhanVien = new XPCollection<NhanVien>(Session, CriteriaOperator.Parse("BoPhan =?", BoPhan.Oid));
        //    else
        //        listNhanVien = new XPCollection<HoSo.NhanVien>(Session);
        //    OnChanged("listNhanVien");
        //}
        //void LoadListBP()
        //{
        //    listBoPhan.Reload();
        //    if (KhoaVien != null)
        //        listBoPhan = new XPCollection<BoPhan>(Session, CriteriaOperator.Parse("BoPhanCha =?", KhoaVien.Oid));
        //    else
        //        listBoPhan = new XPCollection<BoPhan>(Session);
        //    OnChanged("listBoPhan");
        //}
        public Report_BaoCaoChiTietBaiKtra_TieuLuan(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            //listNhanVien = new XPCollection<HoSo.NhanVien>(Session, false);
            //listBoPhan = new XPCollection<BoPhan>(Session, false);
            //LoadListNV();
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_BaoCaoChiTietBaiKTra_TieuLuan", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.Fill(DataSource);
            }
        }
    }
}
