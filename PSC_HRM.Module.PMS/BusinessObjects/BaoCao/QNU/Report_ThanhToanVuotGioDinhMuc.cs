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

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán vượt giờ định mức")]
    public class Report_ThanhToanVuotGioDinhMuc : StoreProcedureReport
    {
        private BangChotThuLao _BangChotThuLao;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private LoaiNhanSu _LoaiNhanSu;
        private LoaiNhanVienEnum _LoaiNhanVienEnum;

        private string _BoPhanNew;

        [ModelDefault("Caption", "Loại phí")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.chkComboxEdit_BoPhan")]
        [ImmediatePostData]
        [Browsable(false)]
        public string BoPhanNew
        {
            get { return _BoPhanNew; }
            set
            {
                SetPropertyValue("BoPhanNew", ref _BoPhanNew, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Bảng chốt")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn bảng chốt")]
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
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Cá nhân")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trinh độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }

        [ModelDefault("Caption", "Loại nhân sự")]
        [Browsable(false)]
        public LoaiNhanSu LoaiNhanSu
        {
            get { return _LoaiNhanSu; }
            set { SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value); }
        }
        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanVienEnum LoaiNhanVienEnum
        {
            get { return _LoaiNhanVienEnum; }
            set { SetPropertyValue("LoaiNhanVienEnum", ref _LoaiNhanVienEnum, value); }
        }
        public Report_ThanhToanVuotGioDinhMuc(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CriteriaOperator f = CriteriaOperator.Parse("NamHoc =?",HamDungChung.GetCurrentNamHoc(Session));
            XPCollection<BangChotThuLao> listBangChot = new XPCollection<NghiepVu.BangChotThuLao>(Session,f);
            if(listBangChot!=null&&listBangChot.Count>0)
            {
                SortingCollection sortNV = new SortingCollection();
                sortNV.Add(new SortProperty("NgayChot", DevExpress.Xpo.DB.SortingDirection.Descending));
                listBangChot.Sorting = sortNV;
                BangChotThuLao = listBangChot[0];
            }
        }

        public override void FillDataSource()
        {
            string i = BoPhanNew;
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_BangChotThanhToanVuotGio", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangChot", BangChotThuLao.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocHam", HocHam == null ? Guid.Empty : HocHam.Oid);
                da.SelectCommand.Parameters.AddWithValue("@TrinhDoChuyenMon", TrinhDoChuyenMon == null ? Guid.Empty : TrinhDoChuyenMon.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiNhanSu", LoaiNhanSu != null ? LoaiNhanSu.Oid : Guid.Empty);

                da.SelectCommand.Parameters.AddWithValue("@LoaiNhanVien", LoaiNhanVienEnum);
                da.Fill(DataSource);
            }
        }
    }

}
