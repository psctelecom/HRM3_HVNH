using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.ThuNhap.NgoaiGio
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết lương ngoài giờ")]
    [Appearance("BangLuongNgoaiGio.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangLuongNgoaiGio is not null and ((BangLuongNgoaiGio.KyTinhLuong is not null and BangLuongNgoaiGio.KyTinhLuong.KhoaSo) or BangLuongNgoaiGio.ChungTu is not null)")]
    public class ChiTietLuongNgoaiGio : ThuNhapBaseObject, IBoPhan
    {
        private BangLuongNgoaiGio _BangLuongNgoaiGio;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;
        private string _CongThucTinhSoTienNhan;
        private string _CongThucTinhTNCT;
        private string _CongThucTinhBangChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng lương ngoài giờ")]
        [Association("BangLuongNgoaiGio-ListChiTietLuongNgoaiGio")]
        public BangLuongNgoaiGio BangLuongNgoaiGio
        {
            get
            {
                return _BangLuongNgoaiGio;
            }
            set
            {
                SetPropertyValue("BangLuongNgoaiGio", ref _BangLuongNgoaiGio, value);
            }
        }

        //Chỉ dùng để lập công thức
        //======================================================
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin lương")]
        public NhanVienThongTinLuong ThongTinLuong { get; set; }
        //======================================================

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công ngày thường")]
        public decimal SoCongNgoaiGio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công cuối tuần")]
        public decimal SoCongNgoaiGio1 { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công ngày lễ")]
        public decimal SoCongNgoaiGio2 { get; set; }


        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công ngày thường đêm")]
        public decimal SoCongNgoaiGioSau23Gio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công cuối tuần đêm")]
        public decimal SoCongNgoaiGio1Sau23Gio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Số công ngày lễ đêm")]
        public decimal SoCongNgoaiGio2Sau23Gio { get; set; }

        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        //[Appearance("UnHide_HBU", TargetItems = "LuongTheoNgayCong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'HBU'")]
        [ModelDefault("Caption", "Lương theo ngày công")]
        public decimal LuongTheoNgayCong { get; set; }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập đơn vị")]
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
        [RuleRequiredField("", DefaultContexts.Save, "Chưa nhập cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
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

        [Size(500)]
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

        [Size(-1)]
        [Browsable(false)]
        [ModelDefault("Caption", "Công thức tính số tiền nhận")]
        public string CongThucTinhSoTienNhan
        {
            get
            {
                return _CongThucTinhSoTienNhan;
            }
            set
            {
                SetPropertyValue("CongThucTinhSoTienNhan", ref _CongThucTinhSoTienNhan, value);
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
        [ModelDefault("Caption", "Công thức tính số tiền chịu thuế")]
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

        public ChiTietLuongNgoaiGio(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }
    }

}
