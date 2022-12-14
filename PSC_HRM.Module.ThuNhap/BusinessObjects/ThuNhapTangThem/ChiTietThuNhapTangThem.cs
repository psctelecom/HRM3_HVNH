using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.ThuNhapTangThem
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết thu nhập tăng thêm")]
    [Appearance("ChiTietThuNhapTangThem.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangThuNhapTangThem is not null and ((BangThuNhapTangThem.KyTinhLuong is not null and BangThuNhapTangThem.KyTinhLuong.KhoaSo) or BangThuNhapTangThem.ChungTu is not null)")]
    public class ChiTietThuNhapTangThem : ThuNhapBaseObject, IBoPhan
    {
        private int _XepLoaiLaoDong;
        private string _GhiChu;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _CongThucTinhSoTien;
        private string _CongThucTinhTNCT;
        private string _DienGiai;
        private string _MaChiTiet;
        private BangThuNhapTangThem _BangThuNhapTangThem;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private ChotThongTinTinhLuong.ThongTinTinhLuong _ThongTinTinhLuong;
        private string _CongThucTinhBangChu;

        public ChiTietThuNhapTangThem(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thu nhập tăng thêm")]
        [Association("BangThuNhapTangThem-ListChiTietThuNhapTangThem")]
        public BangThuNhapTangThem BangThuNhapTangThem
        {
            get
            {
                return _BangThuNhapTangThem;
            }
            set
            {
                SetPropertyValue("BangThuNhapTangThem", ref _BangThuNhapTangThem, value);
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ThongTinTinhLuong = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong=? and ThongTinNhanVien=?", BangThuNhapTangThem.KyTinhLuong.BangChotThongTinTinhLuong, ThongTinNhanVien));
                }
            }
        }

        [ModelDefault("Caption", "Xếp loại lao động (%)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int XepLoaiLaoDong
        {
            get
            {
                return _XepLoaiLaoDong;
            }
            set
            {
                SetPropertyValue("XepLoaiLaoDong", ref _XepLoaiLaoDong, value);
            }
        }

        [ModelDefault("Caption", "Mã chi tiết")]
        public string MaChiTiet
        {
            get
            {
                return _MaChiTiet;
            }
            set
            {
                SetPropertyValue("MaChiTiet", ref _MaChiTiet, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [Browsable(false)]
        public ThongTinTinhLuong ThongTinTinhLuong
        {
            get
            {
                return _ThongTinTinhLuong;
            }
            set
            {
                SetPropertyValue("ThongTinTinhLuong", ref _ThongTinTinhLuong, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        public string CongThucTinhSoTien
        {
            get
            {
                return _CongThucTinhSoTien;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTien", ref _CongThucTinhSoTien, value);
            }
        }

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính bằng chữ")]
        public string CongThucTinhBangChu
        {
            get
            {
                return _CongThucTinhBangChu;
            }
            set
            {
                SetPropertyValue("CongThucTinhBangChu", ref _CongThucTinhBangChu, value);
            }
        }   

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính thu nhập chịu thuế")]
        public string CongThucTinhTNCT
        {
            get
            {
                return _CongThucTinhTNCT;
            }
            set
            {
                SetPropertyValue("CongThucTinhTNCT", ref _CongThucTinhTNCT, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong = false"));

            NVList.Criteria = go;
        }
    }
}
