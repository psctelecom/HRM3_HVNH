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
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Thống kê bình quân khối lượng giảng dạy")]
    public class Report_BinhQuanKhoiLuongGiangDay : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BoPhanView _BoPhanView;
        private BoPhan _BoPhan;
        private LoaiGiangVienEnum _LoaiGiangVien;

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhanView BoPhanView
        {
            get { return _BoPhanView; }
            set
            {
                SetPropertyValue("BoPhanView", ref _BoPhanView, value);
                if (!IsLoading)
                    if (BoPhanView != null)
                        BoPhan = Session.FindObject<BoPhan>(CriteriaOperator.Parse("Oid =?", BoPhanView.OidBoPhan));
                    else
                        BoPhan = null;

            }
        }
        [Browsable(false)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption","Loại giảng viên")]
        public LoaiGiangVienEnum LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }
        [Browsable(false)]
        public XPCollection<BoPhanView> listbp { get; set; }
        public Report_BinhQuanKhoiLuongGiangDay(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            listbp = HamDungChung.getBoPhan(Session);
            OnChanged("listbp");
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_TinhBinhQuanKhoiLuongGiangDay", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ThongTinTruong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGiangVien", LoaiGiangVien.GetHashCode());
                da.Fill(DataSource);
            }
        }
    }
}
