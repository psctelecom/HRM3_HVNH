using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng nhân viên")]
    public class HopDong_NhanVien : HopDong
    {
        // Fields...
        private ChucVu _ChucVu;
        private HinhThucHopDong _HinhThucHopDong;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private QuanLyHopDong _QuanLyHopDong;
        private DateTime _TapSuTuNgay;
        private DateTime _TapSuDenNgay;
        private string _NgheNghiepTruocKhiDuocTuyen;
        private string _CongViecTuyenDung;

        private DateTime _TuNgayCu;


        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý hợp đồng")]
        [Association("QuanLyHopDong-ListHopDong")]
        public QuanLyHopDong QuanLyHopDong
        {
            get
            {
                return _QuanLyHopDong;
            }
            set
            {
                SetPropertyValue("QuanLyHopDong", ref _QuanLyHopDong, value);
                if (!IsLoading && value != null)
                {
                    TaoSoHopDong();
                }
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }



        [ImmediatePostData]
        [ModelDefault("Caption", "Hình thức hợp đồng")]
        public HinhThucHopDong HinhThucHopDong
        {
            get
            {
                return _HinhThucHopDong;
            }
            set
            {
                SetPropertyValue("HinhThucHopDong", ref _HinhThucHopDong, value);
                if (!IsLoading && value != null &&
                    TuNgay != DateTime.MinValue)
                    DenNgay = TuNgay.AddMonths(value.SoThang).AddDays(value.SoNgay).AddDays(-1);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && HinhThucHopDong != null)
                {
                    DenNgay = TuNgay.AddMonths(HinhThucHopDong.SoThang).AddDays(HinhThucHopDong.SoNgay).AddDays(-1);

                    if (!TruongConfig.MaTruong.Equals("UEL"))
                        TapSuTuNgay = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    if(!TruongConfig.MaTruong.Equals("UEL"))
                        TapSuDenNgay = value;
                    DateTime currentDate = HamDungChung.GetServerTime();
                    if (value < currentDate)
                        HopDongCu = true;
                    else
                        HopDongCu = false;
                }
            }
        }

        [ModelDefault("Caption", "Nghề nghiệp trước khi được tuyển")]
        public string NgheNghiepTruocKhiDuocTuyen
        {
            get
            {
                return _NgheNghiepTruocKhiDuocTuyen;
            }
            set
            {
                SetPropertyValue("NgheNghiepTruocKhiDuocTuyen", ref _NgheNghiepTruocKhiDuocTuyen, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Công việc tuyển dụng")]
        public string CongViecTuyenDung
        {
            get
            {
                return _CongViecTuyenDung;
            }
            set
            {
                SetPropertyValue("CongViecTuyenDung", ref _CongViecTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Tập sự từ ngày")]
        public DateTime TapSuTuNgay
        {
            get
            {
                return _TapSuTuNgay;
            }
            set
            {
                SetPropertyValue("TapSuTuNgay", ref _TapSuTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Tập sự đến ngày")]
        public DateTime TapSuDenNgay
        {
            get
            {
                return _TapSuDenNgay;
            }
            set
            {
                SetPropertyValue("TapSuDenNgay", ref _TapSuDenNgay, value);
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Từ ngày cũ")]
        public DateTime TuNgayCu
        {
            get
            {
                return _TuNgayCu;
            }
            set
            {
                SetPropertyValue("TuNgayCu", ref _TuNgayCu, value);
            }
        }


        public HopDong_NhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();

            //TuNgayCu = NhanVien.TuNgayHD;
           

        }

        protected void UpdateNhanVienList()
        {
            if (NVList == null)
            {
                NVList = new XPCollection<NhanVien>(Session, HamDungChung.GetCriteriaThongTinNhanVien(BoPhan, Session));
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (TuNgay <= HamDungChung.GetServerTime())
            {
                NhanVien.TuNgayHD = TuNgay;

            }
        }

        protected override void OnDeleting()
        {
            base.OnDeleting();
            if(!IsSaving)
            {
                NhanVien.TuNgayHD = TuNgayCu;
            }
        }
    }

}
