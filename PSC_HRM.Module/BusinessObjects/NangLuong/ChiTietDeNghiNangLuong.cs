using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.NangLuong
{
    [ImageName("BO_DeNghiNangLuong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị nâng lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DeNghiNangLuong;ThongTinNhanVien")]
    
    [Appearance("Hide_NangLuongTruocNghiHuu", TargetItems = "NgayNghiHuu", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai != 2")]
    [Appearance("Hide_NangLuongTruocHan", TargetItems = "LyDo;SoThang;", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoai != 1")]

    [Appearance("Hide_NEU", TargetItems = "ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    //[Appearance("Hide_GTVT", TargetItems = "ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'GTVT'")]
    //[Appearance("Hide_IUH", TargetItems = "LyDo;SoThang;ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    //[Appearance("Hide_UTE", TargetItems = "LyDo;SoThang;ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    //[Appearance("Hide_LUH", TargetItems = "LyDo;SoThang;ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_DLU", TargetItems = "LyDo;SoThang;ThuongHieuQuaTheoThangCu;MucLuongMoi;ThuongHieuQuaTheoThangMoi;MucLuongCu;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    //[Appearance("Hide_HBU", TargetItems = "", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]

    public class ChiTietDeNghiNangLuong : TruongBaseObject
    {
        // Fields...
        private string _SoQuyetDinh;
        private int _VuotKhungMoi;
        private int _VuotKhungCu;
        private NangLuongEnum _PhanLoai;
        private DateTime _MocNangLuongMoi;
        private decimal _HeSoLuongMoi;
        private BacLuong _BacLuongMoi;
        private DateTime _MocNangLuongCu;
        private DateTime _MocNangLuongDieuChinh;
        private DateTime _NgayHuongLuongCu;
        private decimal _HeSoLuongCu;
        private BacLuong _BacLuongCu;
        private NgachLuong _NgachLuong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DeNghiNangLuong _DeNghiNangLuong;
        private DateTime _NgayHuongLuongMoi;
        private DateTime _NgayNghiHuu;
        private string _LyDo;
        private int _SoThang;
        private string _GhiChu;

        //HBU
        private decimal _MucLuongCu;
        private decimal _MucLuongMoi;
        private decimal _ThuongHieuQuaTheoThangCu;
        private decimal _ThuongHieuQuaTheoThangMoi;

        //BUH
        private DateTime _NgayQuyetDinh;
        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đề nghị nâng lương")]
        [Association("DeNghiNangLuong-ListChiTietDeNghiNangLuong")]
        public DeNghiNangLuong DeNghiNangLuong
        {
            get
            {
                return _DeNghiNangLuong;
            }
            set
            {
                SetPropertyValue("DeNghiNangLuong", ref _DeNghiNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuongCu = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuongCu = value.NhanVienThongTinLuong.HeSoLuong;
                    VuotKhungCu = value.NhanVienThongTinLuong.VuotKhung;
                    NgayHuongLuongCu = value.NhanVienThongTinLuong.NgayHuongLuong;

                    MocNangLuongCu = value.NhanVienThongTinLuong.MocNangLuong;
                    MocNangLuongDieuChinh = value.NhanVienThongTinLuong.MocNangLuongDieuChinh;

                    MocNangLuongMoi = value.NhanVienThongTinLuong.MocNangLuongLanSau;
                    NgayHuongLuongMoi = value.NhanVienThongTinLuong.MocNangLuongLanSau;

                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                    BacLuongCu = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
                if (!IsLoading && value != null)
                    HeSoLuongCu = value.HeSoLuong;
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương cũ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Vượt khung cũ")]
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
                if (!IsLoading)
                {
                    if (value > 0)
                    {
                        BacLuongMoi = BacLuongCu;
                        VuotKhungMoi = value + 1;
                    }
                    else if (BacLuongCu != null)
                    {
                        int bac;
                        if (NgachLuong.TotKhung != BacLuongCu && int.TryParse(BacLuongCu.MaQuanLy.Trim(), out bac))
                        {
                            bac++;
                            BacLuongMoi = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy=? and !BacLuongCu", NgachLuong.Oid, bac.ToString()));
                        }
                        else if (NgachLuong.TotKhung == BacLuongCu && int.TryParse(BacLuongCu.MaQuanLy.Trim(), out bac))
                        {
                            BacLuongMoi = BacLuongCu;
                            VuotKhungMoi = 5;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU' and MaTruong != 'UTE'")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh")]
        public DateTime MocNangLuongDieuChinh
        {
            get
            {
                return _MocNangLuongDieuChinh;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinh", ref _MocNangLuongDieuChinh, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Mức lương cũ")]
        public decimal MucLuongCu
        {
            get
            {
                return _MucLuongCu;
            }
            set
            {
                SetPropertyValue("MucLuongCu", ref _MucLuongCu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thưởng hiệu quả theo tháng cũ")]
        public decimal ThuongHieuQuaTheoThangCu
        {
            get
            {
                return _ThuongHieuQuaTheoThangCu;
            }
            set
            {
                SetPropertyValue("ThuongHieuQuaTheoThangCu", ref _ThuongHieuQuaTheoThangCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading && value != null)
                    HeSoLuongMoi = value.HeSoLuong;
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương mới")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
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

        [ModelDefault("Caption", "Vượt khung mới")]
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

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ hưu")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoai = 2")]
        public DateTime NgayNghiHuu
        {
            get
            {
                return _NgayNghiHuu;
            }
            set
            {
                SetPropertyValue("NgayNghiHuu", ref _NgayNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoai = 1")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhanLoai = 1")]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HBU'")]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Mức lương mới")]
        public decimal MucLuongMoi
        {
            get
            {
                return _MucLuongMoi;
            }
            set
            {
                SetPropertyValue("MucLuongMoi", ref _MucLuongMoi, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Thưởng hiệu quả theo tháng mới")]
        public decimal ThuongHieuQuaTheoThangMoi
        {
            get
            {
                return _ThuongHieuQuaTheoThangMoi;
            }
            set
            {
                SetPropertyValue("ThuongHieuQuaTheoThangMoi", ref _ThuongHieuQuaTheoThangMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NangLuongEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
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

        public ChiTietDeNghiNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            PhanLoai = NangLuongEnum.ThuongXuyen;
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
