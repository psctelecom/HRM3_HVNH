using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;
using System.Data;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số chức danh nhân viên")]
    [DefaultProperty("NhanVien")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;NhanVien", "Hệ số cho nhân viên đã tồn tại.")]
    public class HeSo_ChucDanhNhanVien : BaseObject
    {
        #region  key
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSo_ChucDanhNhanVien")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        #endregion

        private NhanVien _NhanVien;
        private decimal _HeSo_ChucDanh;
        private decimal _HeSo_ChucDanhMoi;
        private string _GhiChu;
        private string _GhiChuNguoiDung;

        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoChucDanh_NhanVien", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [ModelDefault("AllowEdit", "false")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Ghi chú người dùng")]
        public string GhiChuNguoiDung
        {
            get { return _GhiChuNguoiDung; }
            set { SetPropertyValue("GhiChuNguoiDung", ref _GhiChuNguoiDung, value); }
        }

        public HeSo_ChucDanhNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (QuanLyHeSo != null)
            {
                string text = QuanLyHeSo.NamHoc.TenNamHoc + "/" + QuanLyHeSo.HocKy.MaQuanLy;
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Oid", this.Oid);
                param[1] = new SqlParameter("@Text", text);

                DataProvider.ExecuteNonQuery("spd_PMS_GhiChu_HeSoChucDanhNhanVien ", CommandType.StoredProcedure, param);
            }
        }
    }
}