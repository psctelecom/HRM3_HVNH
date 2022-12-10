using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Text;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Report;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using PSC_HRM.Module.DanhGiaKPI;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Kết quả đánh giá KPI cá nhân")]
    public class Report_DanhGiaKPI_KetQuaDanhGiaCaNhan : StoreProcedureReport
    {
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuanLyDanhGiaKPI _QuanLyDanhGiaKPI;
        private VongDanhGia _VongDanhGia;

        public Report_DanhGiaKPI_KetQuaDanhGiaCaNhan(Session session) : base(session) { }


        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("BoPhanList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Quản lý đánh giá KPI")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public QuanLyDanhGiaKPI QuanLyDanhGiaKPI
        {
            get
            {
                return _QuanLyDanhGiaKPI;
            }
            set
            {
                SetPropertyValue("QuanLyDanhGiaKPI", ref _QuanLyDanhGiaKPI, value);
            }
        }

        [ModelDefault("Caption", "Vòng đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public VongDanhGia VongDanhGia
        {
            get
            {
                return _VongDanhGia;
            }
            set
            {
                SetPropertyValue("VongDanhGia", ref _VongDanhGia, value);
            }
        }

        [Browsable(false)]
        public XPCollection<BoPhan> BoPhanList { get; set; }

        private void CriteriaBoPhanList()
        {
            if (BoPhanList == null)
                BoPhanList = new XPCollection<BoPhan>(Session);
            GroupOperator go = new GroupOperator();
            go.Operands.Add(new InOperator("Oid", HamDungChung.GetCriteriaBoPhan()));

            BoPhanList.Criteria = go;
        }
        
        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }
        
        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CriteriaBoPhanList();
            UpdateNVList();
        }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("[dbo].[spd_Web1_DanhGiaKPI_LayKetQuaDanhGia_ByBoPhan]");
            cm.CommandType = System.Data.CommandType.StoredProcedure;

            cm.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
            cm.Parameters.AddWithValue("@QuanLyDanhGiaKPI", QuanLyDanhGiaKPI.Oid);
            cm.Parameters.AddWithValue("@VongDanhGia", VongDanhGia.Oid);
            cm.Parameters.AddWithValue("@ThongTinNhanVien", ThongTinNhanVien.Oid);

            return cm;
        }
    }

}
