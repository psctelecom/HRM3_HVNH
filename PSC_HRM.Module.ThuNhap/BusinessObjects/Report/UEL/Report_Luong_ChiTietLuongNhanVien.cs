using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using PSC_HRM.Module.Report;
using System.ComponentModel;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Report
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Chi tiết lương nhân viên")]
    [Appearance("Report_Luong_ChiTietLuongNhanVien.TatCaDonVi", TargetItems = "BoPhan;TatCaNhanVien;NhanVien", Enabled = false, Criteria = "TatCaDonVi")]
    [Appearance("Report_Luong_ChiTietLuongNhanVien.TatCaNhanVien", TargetItems = "NhanVien", Enabled = false, Criteria = "TatCaNhanVien")]

    public class Report_Luong_ChiTietLuongNhanVien : StoreProcedureReport, IBoPhan
    {
        private bool _TatCaNhanVien;
        private bool _TatCaDonVi;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _NhanVien;
        private ChungTu.ChungTu _ChungTu;

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả đơn vị")]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
                if (!IsLoading && value)
                {
                    BoPhan = null;
                    TatCaNhanVien = true;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaDonVi")]
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
                    NhanVien = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả nhân viên")]
        public bool TatCaNhanVien
        {
            get
            {
                return _TatCaNhanVien;
            }
            set
            {
                SetPropertyValue("TatCaNhanVien", ref _TatCaNhanVien, value);
                if (!IsLoading && value)
                    NhanVien = null;
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaDonVi && !TatCaNhanVien")]
        public ThongTinNhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        public Report_Luong_ChiTietLuongNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TatCaDonVi = true;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);

            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            NVList.Criteria = go;
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_Luong_ChiTietLuongNhanVien", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@ChungTu", ChungTu.Oid);
                if (!TatCaDonVi && TatCaNhanVien)
                    da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);
                else
                    da.SelectCommand.Parameters.AddWithValue("@BoPhan", DBNull.Value);
                if (!TatCaDonVi && !TatCaNhanVien)
                    da.SelectCommand.Parameters.AddWithValue("@ThongTinNhanVien", NhanVien.Oid);
                else
                    da.SelectCommand.Parameters.AddWithValue("@ThongTinNhanVien", DBNull.Value);
                da.Fill(DataSource);
            }
        }

    }

}
