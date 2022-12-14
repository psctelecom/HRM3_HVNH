using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    //Chú ý: lập chứng từ chuyển khoản cho từng ngân hàng riêng
    [DefaultClassOptions]
    [DefaultProperty("SoChungTu")]
    [ImageName("BO_ChuyenKhoan")]
    [ModelDefault("Caption", "Chứng từ chuyển khoản")]
    [Appearance("ChuyenKhoanLuongNhanVien.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ThanhToan = True")]
    public class ChuyenKhoanLuongNhanVien : ChungTu
    {
        private string _SoTaiKhoanChuyen;
        private NganHang _NganHang;
        private bool _ThanhToan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngân hàng nhận")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'NEU'")]
        [Appearance("NganHangNhan.Hide", TargetItems = "NganHang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
            set
            {
                SetPropertyValue("NganHang", ref _NganHang, value);
                if (!IsLoading && value != null)
                    GetSoTaiKhoan();
            }
        }

        [ModelDefault("Caption", "Số tài khoản chuyển")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'NEU'")]
        [Appearance("SoTaiKhoanChuyen.Hide", TargetItems = "SoTaiKhoanChuyen", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
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

        [Browsable(false)]
        public bool ThanhToan
        {
            get
            {
                return _ThanhToan;
            }
            set
            {
                SetPropertyValue("ThanhToan", ref _ThanhToan, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("ChuyenKhoanLuongNhanVien-ChiTietNhanVien")]
        public XPCollection<ChuyenKhoanLuongNhanVienChiTiet> ChiTietList
        {
            get
            {
                return GetCollection<ChuyenKhoanLuongNhanVienChiTiet>("ChiTietList");
            }
        }

        public ChuyenKhoanLuongNhanVien(Session session) : base(session) { }

        private void GetSoTaiKhoan()
        {
            TaiKhoanNganHang taiKhoan = Session.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("ThongTinTruong=? and NganHang=?", ThongTinTruong, NganHang));
            if (taiKhoan != null)
                SoTaiKhoanChuyen = taiKhoan.SoTaiKhoan;
            else
                SoTaiKhoanChuyen = string.Empty;
        }

        protected override void AfterThongTinTruongChanged()
        {
            foreach (TaiKhoanNganHang item in ThongTinTruong.ListTaiKhoanNganHang)
            {
                if (item.TaiKhoanChinh)
                {
                    NganHang = item.NganHang;
                    SoTaiKhoanChuyen = item.SoTaiKhoan;
                }
            }
        }
       
    }

}
