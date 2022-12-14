using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.ChamCong;

namespace PSC_HRM.Module.ThuNhap.Luong
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Lương cán bộ")]
    [Appearance("LuongNhanVien.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "BangLuongNhanVien is not null and ((BangLuongNhanVien.KyTinhLuong is not null and BangLuongNhanVien.KyTinhLuong.KhoaSo) or BangLuongNhanVien.ChungTu is not null)")]
    public class LuongNhanVien : ThuNhapBaseObject, IBoPhan
    {
        private BangLuongNhanVien _BangLuongNhanVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private ChotThongTinTinhLuong.ThongTinTinhLuong _ThongTinTinhLuong;
        private decimal _TongTienLuong;
        private decimal _SoNgayNghi;//NghỈ không phép
        private int _SoNgayNghiTS;
        private decimal _ThucLanh;
        private decimal _TongSoTienChiuThue;
        private decimal _TongBaoHiem22PhanTram_LgNN;
        private decimal _TongBaoHiem22PhanTram_PCTNNG;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng lương nhân viên")]
        [Association("BangLuongNhanVien-ListLuongNhanVien")]
        public BangLuongNhanVien BangLuongNhanVien
        {
            get
            {
                return _BangLuongNhanVien;
            }
            set
            {
                SetPropertyValue("BangLuongNhanVien", ref _BangLuongNhanVien, value);
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
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
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
                    ThongTinTinhLuong = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong=? and ThongTinNhanVien=?", BangLuongNhanVien.KyTinhLuong.BangChotThongTinTinhLuong, ThongTinNhanVien));
                }
            }
        }

        [ModelDefault("Caption", "Tổng Tiền Lương")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongTienLuong
        {
            get
            {
                return _TongTienLuong;
            }
            set
            {
                SetPropertyValue("TongTienLuong", ref _TongTienLuong, value);
            }
        }

        [ModelDefault("Caption", "Số ngày nghỉ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayNghi //Nghĩ không phép
        {
            get
            {
                return _SoNgayNghi;
            }
            set
            {
                SetPropertyValue("SoNgayNghi", ref _SoNgayNghi, value);
            }
        }


        [ModelDefault("Caption", "Số ngày nghỉ TS")]
        public int SoNgayNghiTS
        {
            get
            {
                return _SoNgayNghiTS;
            }
            set
            {
                SetPropertyValue("SoNgayNghiTS", ref _SoNgayNghiTS, value);
            }
        }

        [ModelDefault("Caption", "Thực lãnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal ThucLanh
        {
            get
            {
                return _ThucLanh;
            }
            set
            {
                SetPropertyValue("ThuNhap", ref _ThucLanh, value);
            }
        }

        [ModelDefault("Caption", "Tổng số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongSoTienChiuThue
        {
            get
            {
                return _TongSoTienChiuThue;
            }
            set
            {
                SetPropertyValue("TongSoTienChiuThue", ref _TongSoTienChiuThue, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBaoHiem22PhanTram_LgNN
        {
            get
            {
                return _TongBaoHiem22PhanTram_LgNN;
            }
            set
            {
                SetPropertyValue("TongBaoHiem22PhanTram_LgNN", ref _TongBaoHiem22PhanTram_LgNN, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongBaoHiem22PhanTram_PCTNNG
        {
            get
            {
                return _TongBaoHiem22PhanTram_PCTNNG;
            }
            set
            {
                SetPropertyValue("TongBaoHiem22PhanTram_PCTNNG", ref _TongBaoHiem22PhanTram_PCTNNG, value);
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết các khoản lương, phụ cấp")]
        [Association("LuongNhanVien-ListChiTietLuongNhanVien")]
        public XPCollection<ChiTietLuongNhanVien> ListChiTietLuongNhanVien
        {
            get
            {
                return GetCollection<ChiTietLuongNhanVien>("ListChiTietLuongNhanVien");
            }
        }

        public LuongNhanVien(Session session) : base(session) { }

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

        public void XuLy()
        {
            ThucLanh = 0;
            TongSoTienChiuThue = 0;
            //tính tổng số tiền và số tiền chịu thuế
            foreach (ChiTietLuongNhanVien item in ListChiTietLuongNhanVien)
            {
                if (!item.IsDeleted)
                {
                    if (item.CongTru == CongTruEnum.Cong)
                    {
                        ThucLanh += item.SoTien;
                        TongSoTienChiuThue += item.SoTienChiuThue;
                    }
                    else if (item.CongTru == CongTruEnum.Tru)
                    {
                        ThucLanh -= item.SoTien;
                        TongSoTienChiuThue -= item.SoTienChiuThue;
                    }
                }
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //XuLy();
        }
    }
}
