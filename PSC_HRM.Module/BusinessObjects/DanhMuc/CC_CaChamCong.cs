using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_BenhVien")]
    [ModelDefault("Caption", "Ca chấm công")]
    //[DefaultProperty("LoaiCa")]
    [DefaultProperty("TenCa")] //Lấy tên ca
    public class CC_CaChamCong : BaseObject
    {
        private string _TenCa; //thêm vào cột đã có trong db 23072020
        private string _ThoiGianVaoSang;
        private string _ThoiGianRaSang;
        private string _ThoiGianVaoChieu;
        private string _ThoiGianRaChieu;
        private string _ThoiGianBatDauNghiGiuaCa;
        private string _ThoiGianKetThucNghiGiuaCa;
        private int _SoPhutCong;
        private int _SoPhutTru;
        private int _TongSoGioLamViec;
        private bool _Active;
        private LoaiCaChamCongEnum _LoaiCa;

        [ModelDefault("Caption", "Tên ca")]
        public string TenCa
        {
            get
            {
                return _TenCa;
            }
            set
            {
                SetPropertyValue("TenCa", ref _TenCa, value);
            }
        }


        [ModelDefault("Caption", "Thời gian vào sáng")]
        public string ThoiGianVaoSang
        {
            get
            {
                return _ThoiGianVaoSang;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoSang", ref _ThoiGianVaoSang, value);
            }
        }

        [ModelDefault("Caption", "Thời gian ra sáng")]
        public string ThoiGianRaSang
        {
            get
            {
                return _ThoiGianRaSang;
            }
            set
            {
                SetPropertyValue("ThoiGianRaSang", ref _ThoiGianRaSang, value);
            }
        }

        [ModelDefault("Caption", "Thời gian BD nghỉ giữa ca")]
        public string ThoiGianBatDauNghiGiuaCa
        {
            get
            {
                return _ThoiGianBatDauNghiGiuaCa;
            }
            set
            {
                SetPropertyValue("ThoiGianBatDauNghiGiuaCa", ref _ThoiGianBatDauNghiGiuaCa, value);
            }
        }

        [ModelDefault("Caption", "Thời gian KT nghỉ giữa ca")]
        public string ThoiGianKetThucNghiGiuaCa
        {
            get
            {
                return _ThoiGianKetThucNghiGiuaCa;
            }
            set
            {
                SetPropertyValue("ThoiGianKetThucNghiGiuaCa", ref _ThoiGianKetThucNghiGiuaCa, value);
            }
        }

        [ModelDefault("Caption", "Thời gian vào chiều")]
        public string ThoiGianVaoChieu
        {
            get
            {
                return _ThoiGianVaoChieu;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoChieu", ref _ThoiGianVaoChieu, value);
            }
        }

        [ModelDefault("Caption", "Thời gian ra chiều")]
        public string ThoiGianRaChieu
        {
            get
            {
                return _ThoiGianRaChieu;
            }
            set
            {
                SetPropertyValue("ThoiGianRaChieu", ref _ThoiGianRaChieu, value);
            }
        }

        [ModelDefault("Caption", "Số phút cộng")]
        public int SoPhutCong
        {
            get
            {
                return _SoPhutCong;
            }
            set
            {
                SetPropertyValue("SoPhutCong", ref _SoPhutCong, value);
            }
        }

        [ModelDefault("Caption", "Số phút trừ")]
        public int SoPhutTru
        {
            get
            {
                return _SoPhutTru;
            }
            set
            {
                SetPropertyValue("SoPhutTru", ref _SoPhutTru, value);
            }
        }

        [ModelDefault("Caption", "Tổng số giờ làm việc")]
        public int TongSoGioLamViec
        {
            get
            {
                return _TongSoGioLamViec;
            }
            set
            {
                SetPropertyValue("TongSoGioLamViec", ref _TongSoGioLamViec, value);
            }
        }

        [ModelDefault("Caption", "Kích hoạt")]
        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                SetPropertyValue("Active", ref _Active, value);
            }
        }

        [ModelDefault("Caption", "Kích hoạt")]
        public LoaiCaChamCongEnum LoaiCa
        {
            get
            {
                return _LoaiCa;
            }
            set
            {
                SetPropertyValue("LoaiCa", ref _LoaiCa, value);
            }
        }

        public CC_CaChamCong(Session session) : base(session) { }
    }

}
