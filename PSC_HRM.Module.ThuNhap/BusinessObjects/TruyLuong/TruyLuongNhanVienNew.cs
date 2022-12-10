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
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.ThuNhap.TruyLuong
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Truy lĩnh lương nhân viên mới")]
    //[RuleCombinationOfPropertiesIsUnique("TruyLuongNhanVienNew.Unique", DefaultContexts.Save, "BangTruyLuong;ThongTinNhanVien;KyTinhLuong")]
    [Appearance("Hide_QNU", TargetItems = "PhanTramKhoiHCCu;PhanTramKhoiHCMoi;HSPCKhoiHanhChinhCu;HSPCKhoiHanhChinhMoi;PhanTramThamNienHCCu;PhanTramThamNienHCMoi;HSPCThamNienHCCu;HSPCThamNienHCMoi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_NEU", TargetItems = "SoNgayNghi;ChenhLechLuongCoBanCu;ChenhLechLuongCoBanMoi;HSPCChucVuChenhLenh;HeSoLuongChenhLech", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    
    public class TruyLuongNhanVienNew : TruongBaseObject, IBoPhan
    {
        private BangTruyLuongNew _BangTruyLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private KyTinhLuong _KyTinhLuong;
        private decimal _MucLuongCoSoCu;
        private decimal _MucLuongCoSoMoi;
        private decimal _ChenhLechLuongCoBanCu;
        private decimal _ChenhLechLuongCoBanMoi;
        private decimal _PhanTramHuongLuongCu;
        private decimal _PhanTramHuongLuongMoi; 
        private decimal _HeSoLuongCu;
        private decimal _HeSoLuongMoi;
        private decimal _HeSoLuongChenhLech;
        private int _VuotKhungCu;
        private int _VuotKhungMoi;
        private decimal _HSPCVuotKhungCu;
        private decimal _HSPCVuotKhungMoi;
        private decimal _HSPCChucVuCu;
        private decimal _HSPCChucVuMoi;
        private decimal _HSPCChucVuChenhLenh;
        private decimal _ThamNienCu;
        private decimal _ThamNienMoi;
        private decimal _HSPCThamNienCu;
        private decimal _HSPCThamNienMoi;       
        
        private decimal _SoTien;
        private decimal _SoTienChiuThue;
        private string _GhiChu;

        private decimal _SoNgayNghi;
        private decimal _PhanTramThamNienHCCu;
        private decimal _PhanTramThamNienHCMoi;
        private decimal _HSPCThamNienHCCu;
        private decimal _HSPCThamNienHCMoi;
        private int _PhuCapUuDaiCu;
        private int _PhuCapUuDaiMoi;
        private decimal _HSPCUuDaiCu;
        private decimal _HSPCUuDaiMoi;
        private decimal _PhanTramKhoiHCCu;
        private decimal _PhanTramKhoiHCMoi;
        private decimal _HSPCKhoiHanhChinhCu;
        private decimal _HSPCKhoiHanhChinhMoi;
                
        [Browsable(false)]
        [ModelDefault("Caption", "Bảng truy lĩnh")]
        [Association("BangTruyLuongNew-ListTruyLuongNhanVienNew")]
        public BangTruyLuongNew BangTruyLuong
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
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    LayThongTinLuong(ThongTinNhanVien, KyTinhLuong);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading && value != null)
                {
                    LayThongTinLuong(ThongTinNhanVien, KyTinhLuong);
                }
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
        [ModelDefault("Caption", "Hệ số lương lệch")]
        public decimal HeSoLuongChenhLech
        {
            get
            {
                return _HeSoLuongChenhLech;
            }
            set
            {
                SetPropertyValue("HeSoLuongChenhLech", ref _HeSoLuongChenhLech, value);
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
        [ModelDefault("Caption", "HSPC thâm niên cũ")]
        public decimal HSPCThamNienCu
        {
            get
            {
                return _HSPCThamNienCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienCu", ref _HSPCThamNienCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC thâm niên mới")]
        public decimal HSPCThamNienMoi
        {
            get
            {
                return _HSPCThamNienMoi;
            }
            set
            {
                SetPropertyValue("HSPCThamNienMoi", ref _HSPCThamNienMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC thâm niên chênh lệch")]
        public decimal HSPCThamNienChenhLech
        {
            get
            {
                return _HeSoLuongChenhLech;
            }
            set
            {
                SetPropertyValue("HSPCThamNienChenhLech", ref _HeSoLuongChenhLech, value);
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

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC vượt khung cũ")]
        public decimal HSPCVuotKhungCu
        {
            get
            {
                return _HSPCVuotKhungCu;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhungCu", ref _HSPCVuotKhungCu, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC vượt khung mới")]
        public decimal HSPCVuotKhungMoi
        {
            get
            {
                return _HSPCVuotKhungMoi;
            }
            set
            {
                SetPropertyValue("HSPCVuotKhungMoi", ref _HSPCVuotKhungMoi, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ cũ")]
        public decimal HSPCChucVuCu
        {
            get
            {
                return _HSPCChucVuCu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuCu", ref _HSPCChucVuCu, value);               
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ mới")]
        public decimal HSPCChucVuMoi
        {
            get
            {
                return _HSPCChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSPCChucVuMoi", ref _HSPCChucVuMoi, value);                
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ chênh lệch")]
        public decimal HSPCChucVuChenhLenh
        {
            get
            {
                return _HSPCChucVuChenhLenh;
            }
            set
            {
                SetPropertyValue("HSPCChucVuChenhLenh", ref _HSPCChucVuChenhLenh, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "PC ưu đãi cũ")]
        public int PhuCapUuDaiCu
        {
            get
            {
                return _PhuCapUuDaiCu;
            }
            set
            {
                SetPropertyValue("PhuCapUuDaiCu", ref _PhuCapUuDaiCu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "PC ưu đãi mới")]
        public int PhuCapUuDaiMoi
        {
            get
            {
                return _PhuCapUuDaiMoi;
            }
            set
            {
                SetPropertyValue("PhuCapUuDaiMoi", ref _PhuCapUuDaiMoi, value);
            }
        }       

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC ưu đãi cũ")]
        public decimal HSPCUuDaiCu
        {
            get
            {
                return _HSPCUuDaiCu;
            }
            set
            {
                SetPropertyValue("HSPCUuDaiCu", ref _HSPCUuDaiCu, value);
            }
        }

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC ưu đãi mới")]
        public decimal HSPCUuDaiMoi
        {
            get
            {
                return _HSPCUuDaiMoi;
            }
            set
            {
                SetPropertyValue("HSPCUuDaiMoi", ref _HSPCUuDaiMoi, value);
            }
        }       

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% hưởng lương cũ")]
        public decimal PhanTramHuongLuongCu
        {
            get
            {
                return _PhanTramHuongLuongCu;
            }
            set
            {
                SetPropertyValue("PhanTramHuongLuongCu", ref _PhanTramHuongLuongCu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% hưởng lương mới")]
        public decimal PhanTramHuongLuongMoi
        {
            get
            {
                return _PhanTramHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("PhanTramHuongLuongMoi", ref _PhanTramHuongLuongMoi, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phần trăm thâm niên HC cũ")]
        public decimal PhanTramThamNienHCCu
        {
            get
            {
                return _PhanTramThamNienHCCu;
            }
            set
            {
                SetPropertyValue("PhanTramThamNienHCCu", ref _PhanTramThamNienHCCu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phần trăm thâm niên HC mới")]
        public decimal PhanTramThamNienHCMoi
        {
            get
            {
                return _PhanTramThamNienHCMoi;
            }
            set
            {
                SetPropertyValue("PhanTramThamNienHCMoi", ref _PhanTramThamNienHCMoi, value);
            }
        }

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC thâm niên HC cũ")]
        public decimal HSPCThamNienHCCu
        {
            get
            {
                return _HSPCThamNienHCCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienHCCu", ref _HSPCThamNienHCCu, value);
            }
        }

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC thâm niên HC mới")]
        public decimal HSPCThamNienHCMoi
        {
            get
            {
                return _HSPCThamNienHCMoi;
            }
            set
            {
                SetPropertyValue("HSPCThamNienHCMoi", ref _HSPCThamNienHCMoi, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Mức lương cơ sở cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucLuongCoSoCu
        {
            get
            {
                return _MucLuongCoSoCu;
            }
            set
            {
                SetPropertyValue("MucLuongCoSoCu", ref _MucLuongCoSoCu, value);
            }
        }

        [ModelDefault("Caption", "Mức lương cơ sở mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucLuongCoSoMoi
        {
            get
            {
                return _MucLuongCoSoMoi;
            }
            set
            {
                SetPropertyValue("MucLuongCoSoMoi", ref _MucLuongCoSoMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chênh lệch lương cơ bản cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ChenhLechLuongCoBanCu
        {
            get
            {
                return _ChenhLechLuongCoBanCu;
            }
            set
            {
                SetPropertyValue("ChenhLechLuongCoBanCu", ref _ChenhLechLuongCoBanCu, value);
            }
        }

        [ModelDefault("Caption", "Chênh lệch lương cơ bản mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ChenhLechLuongCoBanMoi
        {
            get
            {
                return _ChenhLechLuongCoBanMoi;
            }
            set
            {
                SetPropertyValue("ChenhLechLuongCoBanMoi", ref _ChenhLechLuongCoBanMoi, value);
            }
        }

        [Size(-1)]
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

        [ModelDefault("Caption", "Phần trăm khối HC cũ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramKhoiHCCu
        {
            get
            {
                return _PhanTramKhoiHCCu;
            }
            set
            {
                SetPropertyValue("PhanTramKhoiHCCu", ref _PhanTramKhoiHCCu, value);
            }
        }

        [ModelDefault("Caption", "Phần trăm khối HC mới")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhanTramKhoiHCMoi
        {
            get
            {
                return _PhanTramKhoiHCMoi;
            }
            set
            {
                SetPropertyValue("PhanTramKhoiHCMoi", ref _PhanTramKhoiHCMoi, value);
            }
        }

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC khối hành chính cũ")]
        public decimal HSPCKhoiHanhChinhCu
        {
            get
            {
                return _HSPCKhoiHanhChinhCu;
            }
            set
            {
                SetPropertyValue("HSPCKhoiHanhChinhCu", ref _HSPCKhoiHanhChinhCu, value);
            }
        }

        [ModelDefault("EditMask", "N4")]
        [ModelDefault("DisplayFormat", "N4")]
        [ModelDefault("Caption", "HSPC khối hành chính mới")]
        public decimal HSPCKhoiHanhChinhMoi
        {
            get
            {
                return _HSPCKhoiHanhChinhMoi;
            }
            set
            {
                SetPropertyValue("HSPCKhoiHanhChinhMoi", ref _HSPCKhoiHanhChinhMoi, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết truy lương")]
        [Association("TruyLuongNhanVienNew-ListChiTietTruyLuongNew")]
        public XPCollection<ChiTietTruyLuongNew> ListChiTietTruyLuong
        {
            get
            {
                return GetCollection<ChiTietTruyLuongNew>("ListChiTietTruyLuong");
            }
        }

        public TruyLuongNhanVienNew(Session session) : base(session) { }

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
            //
            if (!IsDeleted)
            {
                XuLy();
            }
        }

        public void XuLy()
        {
            decimal soTien = 0, soTienChiuThue = 0;

            foreach (ChiTietTruyLuongNew item in ListChiTietTruyLuong)
            {
                if (!item.IsDeleted)
                {
                    if (item.CongTru == CongTruEnum.Cong)
                        soTien += item.SoTien;
                    else
                        soTien -= item.SoTien;

                    //if (item.CongTru == CongTruEnum.Tru &&
                    //    item.MaChiTiet.ToLower().Contains("bh") &&
                    //    item.SoTienChiuThue == 0)
                    //    soTienChiuThue -= item.SoTien;
                    //else
                    //    soTienChiuThue += item.SoTienChiuThue;
                }
            }

            SoTien = soTien;
            SoTienChiuThue = soTienChiuThue;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //XuLy();
        }

        public void LayThongTinLuong(ThongTinNhanVien thongTinNhanVien, KyTinhLuong kyTinhLuong)
        {
            if (thongTinNhanVien != null && kyTinhLuong != null)
            {
                ThongTinTinhLuong thongTinTinhLuongCu = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong.Oid=? and ThongTinNhanVien=?", kyTinhLuong.BangChotThongTinTinhLuong,thongTinNhanVien.Oid));
                ThongTinTinhLuong thongTinTinhLuongMoi = Session.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("BangChotThongTinTinhLuong.Oid=? and ThongTinNhanVien=?", BangTruyLuong.KyTinhLuong.BangChotThongTinTinhLuong, thongTinNhanVien.Oid));

                MucLuongCoSoCu = kyTinhLuong.ThongTinChung.LuongCoBan;
                MucLuongCoSoMoi = kyTinhLuong.ThongTinChung.LuongCoBan;
                //MucLuongCoSoMoi = BangTruyLuong.KyTinhLuong.ThongTinChung.LuongCoBan;
                ChenhLechLuongCoBanCu = kyTinhLuong.ThongTinChung.ChenhLechLuongCoBan;
                ChenhLechLuongCoBanMoi = kyTinhLuong.ThongTinChung.ChenhLechLuongCoBan; ;
                //ChenhLechLuongCoBanMoi = BangTruyLuong.KyTinhLuong.ThongTinChung.ChenhLechLuongCoBan;
                
                if (thongTinTinhLuongCu != null && thongTinTinhLuongMoi != null)
                {
                    PhanTramHuongLuongCu = thongTinTinhLuongCu.TinhTrang.PhanTramHuongLuong;
                    PhanTramHuongLuongMoi = thongTinTinhLuongMoi.TinhTrang.PhanTramHuongLuong;
                    HeSoLuongCu = thongTinTinhLuongCu.HeSoLuong;
                    HeSoLuongMoi = thongTinTinhLuongMoi.HeSoLuong;
                    VuotKhungCu = thongTinTinhLuongCu.VuotKhung;
                    VuotKhungMoi = thongTinTinhLuongMoi.VuotKhung;
                    HSPCVuotKhungCu = thongTinTinhLuongCu.HSPCVuotKhung;
                    HSPCVuotKhungMoi = thongTinTinhLuongMoi.HSPCVuotKhung;
                    HSPCChucVuCu = thongTinTinhLuongCu.HSPCChucVu;
                    HSPCChucVuMoi = thongTinTinhLuongMoi.HSPCChucVu;
                    ThamNienCu = thongTinTinhLuongCu.ThamNien;
                    ThamNienMoi = thongTinTinhLuongMoi.ThamNien;
                    HSPCThamNienCu = thongTinTinhLuongCu.HSPCThamNien;
                    HSPCThamNienMoi = thongTinTinhLuongMoi.HSPCThamNienHC;
                    PhuCapUuDaiCu = thongTinTinhLuongCu.PhuCapUuDai;
                    PhuCapUuDaiMoi = thongTinTinhLuongMoi.PhuCapUuDai;
                    HSPCUuDaiCu = thongTinTinhLuongCu.HSPCUuDai;
                    HSPCUuDaiMoi = thongTinTinhLuongMoi.HSPCUuDai;
                    PhanTramThamNienHCCu = thongTinTinhLuongCu.PhanTramThamNienHC;
                    PhanTramThamNienHCMoi = thongTinTinhLuongMoi.PhanTramThamNienHC;
                    HSPCThamNienHCCu = thongTinTinhLuongCu.HSPCThamNienHC;
                    HSPCThamNienHCMoi = thongTinTinhLuongMoi.HSPCThamNienHC;
                    PhanTramKhoiHCCu = thongTinTinhLuongCu.PhanTramKhoiHC;
                    PhanTramKhoiHCMoi = thongTinTinhLuongMoi.PhanTramKhoiHC;
                    HSPCKhoiHanhChinhCu = thongTinTinhLuongCu.HSPCKhoiHanhChinh;
                    HSPCKhoiHanhChinhMoi = thongTinTinhLuongMoi.HSPCKhoiHanhChinh;
                }
                else if (thongTinTinhLuongMoi != null)
                {
                    PhanTramHuongLuongCu = thongTinTinhLuongMoi.TinhTrang.PhanTramHuongLuong;
                    PhanTramHuongLuongMoi = thongTinTinhLuongMoi.TinhTrang.PhanTramHuongLuong;
                    HeSoLuongCu = thongTinTinhLuongMoi.HeSoLuong;
                    HeSoLuongMoi = thongTinTinhLuongMoi.HeSoLuong;
                    VuotKhungCu = thongTinTinhLuongMoi.VuotKhung;
                    VuotKhungMoi = thongTinTinhLuongMoi.VuotKhung;
                    HSPCChucVuCu = thongTinTinhLuongMoi.HSPCChucVu;
                    HSPCChucVuMoi = thongTinTinhLuongMoi.HSPCChucVu;
                    ThamNienCu = thongTinTinhLuongMoi.ThamNien;
                    ThamNienMoi = thongTinTinhLuongMoi.ThamNien;
                    HSPCThamNienCu = thongTinTinhLuongMoi.HSPCThamNien;
                    HSPCThamNienMoi = thongTinTinhLuongMoi.HSPCThamNienHC;
                    PhuCapUuDaiCu = thongTinTinhLuongMoi.PhuCapUuDai;
                    PhuCapUuDaiMoi = thongTinTinhLuongMoi.PhuCapUuDai;
                    HSPCUuDaiCu = thongTinTinhLuongMoi.HSPCUuDai;
                    HSPCUuDaiMoi = thongTinTinhLuongMoi.HSPCUuDai;
                    PhanTramThamNienHCCu = thongTinTinhLuongMoi.PhanTramThamNienHC;
                    PhanTramThamNienHCMoi = thongTinTinhLuongMoi.PhanTramThamNienHC;
                    HSPCThamNienHCCu = thongTinTinhLuongMoi.HSPCThamNienHC;
                    HSPCThamNienHCMoi = thongTinTinhLuongMoi.HSPCThamNienHC;
                    PhanTramKhoiHCCu = thongTinTinhLuongMoi.PhanTramKhoiHC;
                    PhanTramKhoiHCMoi = thongTinTinhLuongMoi.PhanTramKhoiHC;
                    HSPCKhoiHanhChinhCu = thongTinTinhLuongMoi.HSPCKhoiHanhChinh;
                    HSPCKhoiHanhChinhMoi = thongTinTinhLuongMoi.HSPCKhoiHanhChinh;
                }
            }
        }
    }

}
