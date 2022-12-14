using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định thành lập khác - Thành viên")]
    public class ChiTietThanhLapKhac_ThanhVien : TruongBaseObject
    {
        private int _SoThuTu;
        private BoPhan _BoPhan; 
        private ThongTinNhanVien _ThongTinNhanVien;
        //private ChucVu _ChucVu;
        private string _ChucVuText;
        private string _HoTenText;
        
        private ChucDanhHoiDong _ChucDanhHoiDong;
        private ChiTietThanhLapKhac_ToChuc _ChiTietThanhLapKhac_ToChuc;
        private GiayToHoSo _GiayToHoSo;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khác")]
        [Association("ChiTietThanhLapKhac_ToChuc-ListChiTietThanhLapKhac_ThanhVien")]
        public ChiTietThanhLapKhac_ToChuc ChiTietThanhLapKhac_ToChuc
        {
            get
            {
                return _ChiTietThanhLapKhac_ToChuc;
            }
            set
            {
                SetPropertyValue("ChiTietThanhLapKhac_ToChuc", ref _ChiTietThanhLapKhac_ToChuc, value);
            }
        }
        
        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                    { BoPhan = value.BoPhan; }
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    GetHoTen(value); 
                    GetChucVu(value);
                }
            }
        }

        //[ModelDefault("Caption", "Chức vụ")]
        //public ChucVu ChucVu
        //{
        //    get
        //    {
        //        return _ChucVu;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ChucVu", ref _ChucVu, value);
        //    }
        //}

        [ModelDefault("Caption", "Chức vụ")]
        public String ChucVuText
        {
            get
            {
                return _ChucVuText;
            }
            set
            {
                SetPropertyValue("ChucVuText", ref _ChucVuText, value);
            }
        }

        [ModelDefault("Caption", "Họ và tên")]
        public String HoTenText
        {
            get
            {
                return _HoTenText;
            }
            set
            {
                SetPropertyValue("HoTenText", ref _HoTenText, value);
            }
        }
        
        [ModelDefault("Caption", "Nhiệm vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChucDanhHoiDong ChucDanhHoiDong
        {
            get
            {
                return _ChucDanhHoiDong;
            }
            set
            {
                SetPropertyValue("ChucDanhHoiDong", ref _ChucDanhHoiDong, value);
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        public ChiTietThanhLapKhac_ThanhVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thành lập khác"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));           
            ChucDanhHoiDong = Session.FindObject<ChucDanhHoiDong>(CriteriaOperator.Parse("TenChucDanhHoiDong = ?","Ủy viên"));
        }
                
        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            //GetHoTen(ThongTinNhanVien);
            //GetChucVu(ThongTinNhanVien);
            //OnSaving();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void GetChucVu(ThongTinNhanVien thongTinNhanVien)
        {
            ChucVuText = thongTinNhanVien.ChucVu != null ? thongTinNhanVien.ChucVu.TenChucVu : "";
            if (TruongConfig.MaTruong == "BUH")
            {
                if (thongTinNhanVien.ChucVu != null && thongTinNhanVien.ChucVu.HSPCChucVu > 0)
                {
                    if (thongTinNhanVien.ChucVu.MaQuanLy.Equals("HT") || thongTinNhanVien.ChucVu.MaQuanLy.Equals("PHT"))
                    { ChucVuText = thongTinNhanVien.ChucVu.TenChucVu; }
                    else
                    { ChucVuText = thongTinNhanVien.ChucVu.MaQuanLy + " - " + thongTinNhanVien.BoPhan.TenBoPhanVietTat; }
                }
                else
                { ChucVuText = thongTinNhanVien.ChucDanh.MaQuanLy + " - " + thongTinNhanVien.BoPhan.TenBoPhanVietTat; }
            }
        }

        private void GetHoTen(ThongTinNhanVien thongTinNhanVien)
        {
            HoTenText = thongTinNhanVien.GioiTinh == GioiTinhEnum.Nam ? "Ông " : "Bà ";
            HoTenText += thongTinNhanVien.HoTen;                        
        }
    }
}
