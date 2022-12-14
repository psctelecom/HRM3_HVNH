using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [ImageName("BO_ChuyenKhoan")]
    [ModelDefault("Caption", "Chi tiết chuyển khoản cán bộ - tạm giữ lương")]
    [RuleCombinationOfPropertiesIsUnique("ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong.Unique", DefaultContexts.Save, "ChungTu;NhanVien")]
    public class ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong : TruongBaseObject, IBoPhan
    {
        private ChungTu_TamGiuLuong _ChungTu_TamGiuLuong;
        private ChungTu _ChungTu;
        private KyTinhLuong _KyTinhLuong;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private NganHang _NganHang;
        private string _SoTaiKhoan;
        private decimal _ThueTNCN;
        private decimal _ThucNhan;
        private string _SoTaiKhoanChuyen;
        private NganHang _NganHangChuyen;
    
        [Browsable(false)]
        [Association("ChungTu_TamGiuLuong-ChiTietNhanVien")]
        public ChungTu_TamGiuLuong ChungTu_TamGiuLuong
        {
            get
            {
                return _ChungTu_TamGiuLuong;
            }
            set
            {
                SetPropertyValue("ChungTu_TamGiuLuong", ref _ChungTu_TamGiuLuong, value);
            }
        }

        [ModelDefault("Caption", "Chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ChungTu ChungTu
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
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
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

        [ModelDefault("Caption", "Ngân hàng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoTaiKhoan
        {
            get
            {
                return _SoTaiKhoan;
            }
            set
            {
                SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang NganHangChuyen
        {
            get
            {
                return _NganHangChuyen;
            }
            set
            {
                SetPropertyValue("NganHangChuyen", ref _NganHangChuyen, value);
            }
        }
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoTaiKhoanChuyen
        {
            get
            {
                return _SoTaiKhoanChuyen;
            }
            set
            {
                SetPropertyValue("SoTaiKhoanChuyen", ref _SoTaiKhoanChuyen, value);
            }
        }
        public ChuyenKhoanLuongNhanVienChiTiet_TamGiuLuong(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            { 
                //Cập nhật lại đã chi lại lương trong chứng từ chuyển khoản lương nhân viên
                CriteriaOperator criteria = CriteriaOperator.Parse("ChuyenKhoanLuongNhanVien=? and NhanVien=?", this.ChungTu.Oid,this.NhanVien.Oid);
                ChuyenKhoanLuongNhanVienChiTiet chuyenKhoanLNVChiTiet = Session.FindObject<ChuyenKhoanLuongNhanVienChiTiet>(criteria);
                if (chuyenKhoanLNVChiTiet != null)
                {
                    chuyenKhoanLNVChiTiet.ChiLaiLuong = true;
                }
            }
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            //
            if (!IsSaving)
            {
                //Cập nhật lại đã chi lại lương trong chứng từ chuyển khoản lương nhân viên
                CriteriaOperator criteria = CriteriaOperator.Parse("ChuyenKhoanLuongNhanVien=? and NhanVien=?", this.ChungTu.Oid, this.NhanVien.Oid);
                ChuyenKhoanLuongNhanVienChiTiet chuyenKhoanLNVChiTiet = Session.FindObject<ChuyenKhoanLuongNhanVienChiTiet>(criteria);
                if (chuyenKhoanLNVChiTiet != null)
                {
                    chuyenKhoanLNVChiTiet.ChiLaiLuong = false;
                }
            }
        }
    }
}
