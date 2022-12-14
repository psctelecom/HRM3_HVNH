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
    [ModelDefault("Caption", "Chi tiết chuyển khoản cán bộ")]
    [RuleCombinationOfPropertiesIsUnique("ChuyenKhoanLuongNhanVienChiTiet.Unique", DefaultContexts.Save, "ChuyenKhoanLuongNhanVien;NhanVien")]
    public class ChuyenKhoanLuongNhanVienChiTiet : TruongBaseObject, IBoPhan
    {
        private ChuyenKhoanLuongNhanVien _ChuyenKhoanLuongNhanVien;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private NganHang _NganHang;
        private string _SoTaiKhoan;
        private decimal _ThuNhap;
        private decimal _KhauTru;
        private decimal _ThucNhan;
        private string _SoTaiKhoanChuyen;
        private NganHang _NganHangChuyen;
        private bool _TamGiuLuong;
        private bool _ChiLaiLuong;
    
        [Browsable(false)]
        [Association("ChuyenKhoanLuongNhanVien-ChiTietNhanVien")]
        public ChuyenKhoanLuongNhanVien ChuyenKhoanLuongNhanVien
        {
            get
            {
                return _ChuyenKhoanLuongNhanVien;
            }
            set
            {
                SetPropertyValue("ChuyenKhoanLuongNhanVien", ref _ChuyenKhoanLuongNhanVien, value);
            }
        }

        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Ngân hàng")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL' and MaTruong != 'NEU'")]
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
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL' and MaTruong != 'NEU'")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Thu nhập")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThuNhap
        {
            get
            {
                return _ThuNhap;
            }
            set
            {
                SetPropertyValue("ThuNhap", ref _ThuNhap, value);
                if (!IsLoading && value > 0)
                    SetThucNhan();
            }
        }

        [ModelDefault("Caption", "Khấu trừ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal KhauTru
        {
            get
            {
                return _KhauTru;
            }
            set
            {
                SetPropertyValue("KhauTru", ref _KhauTru, value);
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

        [Appearance("TamGiuLuong_Hide", TargetItems = "TamGiuLuong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH' or MaTruong = 'LUH' or MaTruong = 'DLU'")]
        [ModelDefault("Caption", "Tạm giữ lương")]
        public bool TamGiuLuong
        {
            get
            {
                return _TamGiuLuong;
            }
            set
            {
                SetPropertyValue("TamGiuLuong", ref _TamGiuLuong, value);
            }
        }

        [Browsable(false)]
        public bool ChiLaiLuong
        {
            get
            {
                return _ChiLaiLuong;
            }
            set
            {
                SetPropertyValue("ChiLaiLuong", ref _ChiLaiLuong, value);
            }
        }
        [Browsable(false)]
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
        public ChuyenKhoanLuongNhanVienChiTiet(Session session) : base(session) { }

        private void SetThucNhan()
        {
            ThucNhan = ThuNhap - KhauTru;
            if (ThucNhan < 0)
                ThucNhan = 0;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong = fasle"));

            NVList.Criteria = go;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            MaTruong = TruongConfig.MaTruong;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            MaTruong = TruongConfig.MaTruong;
        }
    }
}
