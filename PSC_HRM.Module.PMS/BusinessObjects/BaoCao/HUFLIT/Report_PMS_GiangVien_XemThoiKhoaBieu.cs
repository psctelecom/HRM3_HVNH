using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
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
    [ModelDefault("Caption", "Báo cáo: XemThoiKhoaBieuGV")]
    public class Report_PMS_GiangVien_XemThoiKhoaBieu : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        private LoaiGiangVienPMS _LoaiGiangVien;

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);              
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    NhanVien = null;
                    updateNV();
                }
            }
        }

        [ModelDefault("Caption", "Nhân Viên")]
        [ImmediatePostData]
        [DataSourceProperty("listnv")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (value != null)
                {
                    BoPhan = NhanVien.BoPhan;
                }
            }
        }
      
        [ModelDefault("Caption", "Loại giảng viên")]
        [ImmediatePostData]
        public LoaiGiangVienPMS LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set
            {
                SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value);              
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> listbp { get; set; }
        
        [Browsable(false)]
        public XPCollection<NhanVien> listnv { get; set; }

        void updateNV()
        {
            if (listnv == null)
            {
                listnv = new XPCollection<NhanVien>(Session);
            }
            if (BoPhan != null)
            {
                listnv.Criteria = CriteriaOperator.Parse("ThongTinTruong = ? and BoPhan=?", ThongTinTruong.Oid, BoPhan.Oid);
            }
            OnChanged("listnv");
        }

        //Thực hiện cập nhật bộ phận
        public void UpdateBoPhan()
        {
            if (listbp == null)
            {
                listbp = new XPCollection<BoPhan>(Session);
            }
            if (ThongTinTruong != null)
            {
                listbp.Criteria = CriteriaOperator.Parse("ThongTinTruong = ?", ThongTinTruong.Oid);
            }
        }

        public Report_PMS_GiangVien_XemThoiKhoaBieu(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            UpdateBoPhan();
            updateNV();
            LoaiGiangVien = Session.FindObject<LoaiGiangVienPMS>(CriteriaOperator.Parse("MaQuanLy=?", 2));          
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_GiangVien_XemThoiKhoaBieu", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc == null ? Guid.Empty : NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);               
                da.SelectCommand.Parameters.AddWithValue("@LoaiGV", LoaiGiangVien == null ? 2:LoaiGiangVien.MaQuanLy);
                da.Fill(DataSource);
            }
        }
    }
}
