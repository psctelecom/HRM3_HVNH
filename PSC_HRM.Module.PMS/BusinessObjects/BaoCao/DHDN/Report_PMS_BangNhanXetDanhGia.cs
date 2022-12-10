using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
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
    [ModelDefault("Caption", "Báo cáo: Bảng nhận xét đánh giá")]
    public class Report_PMS_BangNhanXetDanhGia : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private BoPhanView _BoPhan;
        private NhanVienView _NhanVien;
        private NamHoc _NamHoc;
        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
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

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn Bộ phận")]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public BoPhanView BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("NhanVien", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                        updateNV();
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("listnv", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVienView NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (NhanVien != null && !IsLoading)
                    NhanVienFull = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid =?", NhanVien.OidNhanVien.ToString()));
            }
        }
        private NhanVien _NhanVienFull;
        [Browsable(false)]
        public NhanVien NhanVienFull
        {
            get { return _NhanVienFull; }
            set { SetPropertyValue("NhanVienFull", ref _NhanVienFull, value); }
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
        [Browsable(false)]
        public XPCollection<BoPhanView> listbp { get; set; }
        [Browsable(false)]
        public XPCollection<NhanVienView> listnv { get; set; }


        public Report_PMS_BangNhanXetDanhGia(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listbp = HamDungChung.getBoPhan(Session);
            OnChanged("listbp");
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        void updateNV()
        {
            listnv = HamDungChung.getNhanVien(Session, BoPhan.OidBoPhan);
            OnChanged("listnv");
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_BangNhanXetDanhGia", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVienFull.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);  
                da.Fill(DataSource);
            }
        }
    }
}
