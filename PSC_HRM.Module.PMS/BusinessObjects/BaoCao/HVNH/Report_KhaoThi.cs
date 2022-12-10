using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
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
    [ModelDefault("Caption", "Báo cáo: Khảo thí")]
    public class Report_KhaoThi : StoreProcedureReport
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _BoPhan;     
        private NhanVien _NhanVien;
        private LoaiGiangVienEnum _LoaiGiangVienEnum = LoaiGiangVienEnum.TatCa;
        private CoSoGiangDay _CoSoGiangDay;


        [ImmediatePostData]
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
                if (value != null)
                {
                    HocKy = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
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

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum LoaiGiangVienEnum
        {
            get { return _LoaiGiangVienEnum; }
            set { SetPropertyValue("LoaiGiangVienEnum", ref _LoaiGiangVienEnum, value); }
        }

        [ModelDefault("Caption", "Loại phân viện")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }

        [Browsable(false)]
        public XPCollection<NhanVien> NhanVienList
        { get; set; }

        public Report_KhaoThi(Session session) : base(session) { }

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
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_KhaoThi", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy",  HocKy==null ? Guid.Empty : HocKy.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGiangVienEnum", LoaiGiangVienEnum.GetHashCode());
                da.SelectCommand.Parameters.AddWithValue("@CoSoGiangDay", CoSoGiangDay == null ? Guid.Empty : CoSoGiangDay.Oid);
                da.Fill(DataSource);
            }
        }

    }
}
