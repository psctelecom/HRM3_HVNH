using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Truy lĩnh lương nhân viên")]
    [RuleCombinationOfPropertiesIsUnique("TruyLuongNhanVien.Unique", DefaultContexts.Save, "BangTruyLuong;ThongTinNhanVien")]
    public class TruyLuongNhanVien : BaseObject, IBoPhan
    {
        private BangTruyLuong _BangTruyLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private decimal _HeSoLuongCu;
        private decimal _HeSoLuongMoi;
        private decimal _HSChucVuCu;
        private decimal _HSChucVuMoi;
        private decimal _ChenhLech;
        private decimal _ChenhLechHSCV;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private decimal _SoNgayNghi;
        private decimal _ThamNienCu;
        private decimal _ThamNienMoi;
        private decimal _ChenhLechThamNien;
        private int _VuotKhungCu;
        private int _VuotKhungMoi;
        private int _ChenhLechVuotKhung;
        private decimal _HSPCChuyenMonCu;
        private decimal _HSPCChuyenMonMoi;
        private decimal _ChenhLechHSPCChuyenMon;
        
        
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng truy lĩnh")]
        [Association("BangTruyLuong-ListTruyLuongNhanVien")]
        public BangTruyLuong BangTruyLuong
        {
            get
            {
                return _BangTruyLuong;
            }
            set
            {
                SetPropertyValue("BangTruyLuong", ref _BangTruyLuong, value);
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
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương cũ")]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương mới")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Chênh lệch HSL")]
        public decimal ChenhLech
        {
            get
            {
                return _ChenhLech;
            }
            set
            {
                SetPropertyValue("ChenhLech", ref _ChenhLech, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số CV cũ")]
        public decimal HSChucVuCu
        {
            get
            {
                return _HSChucVuCu;
            }
            set
            {
                SetPropertyValue("HSChucVuCu", ref _HSChucVuCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số CV mới")]
        public decimal HSChucVuMoi
        {
            get
            {
                return _HSChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSChucVuMoi", ref _HSChucVuMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Chênh lệch HSCV")]
        public decimal ChenhLechHSCV
        {
            get
            {
                return _ChenhLechHSCV;
            }
            set
            {
                SetPropertyValue("ChenhLechHSCV", ref _ChenhLechHSCV, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "% thâm niên cũ")]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "% thâm niên mới")]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Chênh lệch % thâm niên")]
        public decimal ChenhLechThamNien
        {
            get
            {
                return _ChenhLechThamNien;
            }
            set
            {
                SetPropertyValue("ChenhLechThamNien", ref _ChenhLechThamNien, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% vượt khung cũ")]
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% vượt khung mới")]
        public int VuotKhungMoi
        {
            get
            {
                return _VuotKhungMoi;
            }
            set
            {
                SetPropertyValue("VuotKhungMoi", ref _VuotKhungMoi, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Chênh lệch % vượt khung")]
        public int ChenhLechVuotKhung
        {
            get
            {
                return _ChenhLechVuotKhung;
            }
            set
            {
                SetPropertyValue("ChenhLechVuotKhung", ref _ChenhLechVuotKhung, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chuyên môn cũ")]
        public decimal HSPCChuyenMonCu
        {
            get
            {
                return _HSPCChuyenMonCu;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonCu", ref _HSPCChuyenMonCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chuyên môn mới")]
        public decimal HSPCChuyenMonMoi
        {
            get
            {
                return _HSPCChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonMoi", ref _HSPCChuyenMonMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Chênh lệch HSPC chuyên môn")]
        public decimal ChenhLechHSPCChuyenMon
        {
            get
            {
                return _ChenhLechHSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("ChenhLechHSPCChuyenMon", ref _ChenhLechHSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
        public decimal SoNgayNghi
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết truy lương")]
        [Association("TruyLuongNhanVien-ListChiTietTruyLuong")]
        public XPCollection<ChiTietTruyLuong> ListChiTietTruyLuong
        {
            get
            {
                return GetCollection<ChiTietTruyLuong>("ListChiTietTruyLuong");
            }
        }

        public TruyLuongNhanVien(Session session) : base(session) { }

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

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                XuLy();
            }
        }

        public void XuLy()
        {
            decimal soTien = 0, soTienChiuThue = 0;

            foreach (ChiTietTruyLuong item in ListChiTietTruyLuong)
            {
                if (!item.IsDeleted)
                {
                    if (item.CongTru == CongTruEnum.Cong)
                        soTien += item.SoTien;
                    else
                        soTien -= item.SoTien;
                    
                    if (item.CongTru == CongTruEnum.Tru &&
                        item.MaChiTiet.ToLower().Contains("bh") &&
                        item.SoTienChiuThue == 0)
                        soTienChiuThue -= item.SoTien;
                    else
                        soTienChiuThue += item.SoTienChiuThue;
                }
            }

            SoTien = soTien;
            SoTienChiuThue = soTienChiuThue;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            XuLy();
        }
    }

}
