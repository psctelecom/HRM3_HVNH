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
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.Thuong
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Chi tiết thưởng cán bộ")]
    [Appearance("ChiTietThuongNhanVien.Khoa", TargetItems = "*", Enabled = false,
        Criteria = "BangThuongNhanVien is not null and ((BangThuongNhanVien.KyTinhLuong is not null and BangThuongNhanVien.KyTinhLuong.KhoaSo) or BangThuongNhanVien.ChungTu is not null)")]
    public class ChiTietThuongNhanVien : ThuNhapBaseObject, IBoPhan
    {
        private BangThuongNhanVien _BangThuongNhanVien;
        private DateTime _NgayThuong;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private string _GhiChu;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _CongThucTinhSoTienNhan;
        private string _CongThucTinhThueTNCN;
        private ChotThongTinTinhLuong.ThongTinTinhLuong _ThongTinTinhLuong;
        private string _CongThucTinhBangChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thưởng cán bộ")]
        [Association("BangThuongNhanVien-ListChiTietThuongNhanVien")]
        public BangThuongNhanVien BangThuongNhanVien
        {
            get
            {
                return _BangThuongNhanVien;
            }
            set
            {
                SetPropertyValue("BangThuongNhanVien", ref _BangThuongNhanVien, value);
            }
        }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong { get; set; }

        //Chỉ dùng để lập công thức
        [NonPersistent]
        [VisibleInListView(false)]
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Thông tin lương")]
        public NhanVienThongTinLuong ThongTinLuong { get; set; }

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
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ThongTinTinhLuong = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong=? and ThongTinNhanVien=?", BangThuongNhanVien.KyTinhLuong.BangChotThongTinTinhLuong, NhanVien));
                }
            }
        }

        [ModelDefault("Caption", "Ngày thưởng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayThuong
        {
            get
            {
                return _NgayThuong;
            }
            set
            {
                SetPropertyValue("NgayThuong", ref _NgayThuong, value);
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
        [ModelDefault("Caption", "Công thức tính TNCT")]
        public string CongThucTinhThueTNCN
        {
            get
            {
                return _CongThucTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("CongThucTinhThueTNCN", ref _CongThucTinhThueTNCN, value);
            }
        }

        public ChiTietThuongNhanVien(Session session) : base(session) { }                

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayThuong = HamDungChung.GetServerTime();
        }

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
