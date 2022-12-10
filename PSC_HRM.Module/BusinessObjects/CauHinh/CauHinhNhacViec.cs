using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình nhắc việc")]
    public class CauHinhNhacViec : BaseObject
    {
        // Fields...
        private bool _TheoDoiDenHanNangThamNienCongTac;
        private bool _TheoDoiSinhNhat;
        private bool _TheoDoiDenHanNangThamNien;
        private bool _TheoDoiDenHanNangLuong;
        private bool _TheoDoiHetNhiemKyChucVu;
        private bool _TheoDoiHetHanTamHoanTapSu;
        private bool _TheoDoiHetHanThuViec;
        private bool _TheoDoiHetHanTapSu;
        private bool _TheoDoiHetHanNghiKhongHuongLuong;
        private bool _TheoDoiHetHanNghiBHXH;
        private bool _TheoDoiHetHanHopDong;
        private bool _TheoDoiDenHanNopVanBang;
        private bool _TheoDoiDenTuoiNghiHuu;
        private bool _TheoDoiDiHocNuocNgoai;
        private bool _TheoDoiHetHanTamGiuLuong;
        private bool _TheoDoiDenHanNangThamNienHC;
        //
        private int _SoThangTruocKhiHetHanHopDong;
        private int _SoThangTruocKhiNghiHuu;
        private int _SoThangTruocKhiHetNhiemKyChucVu;
        private int _SoThangTruocKhiNangLuong;
        private int _SoThangTruocKhiNangThamNien;
        private int _SoThangTruocKhiHetHanNghiKhongLuong;
        private int _SoThangTruocKhiHetHanNghiThaiSan;
        private int _SoThangTruocKhiDenSinhNhat;
        private int _SoThangTruocKhiHetHanDiHoc;
        private int _SoThangTruocKhiHetHanNangThamNienHC;
        private int _SoThangTruocKhiHetHanTamGiuLuong;

        [ModelDefault("Caption", "Theo dõi đến hạn nộp văn bằng")]
        public bool TheoDoiDenHanNopVanBang
        {
            get
            {
                return _TheoDoiDenHanNopVanBang;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNopVanBang", ref _TheoDoiDenHanNopVanBang, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn hợp đồng")]
        public bool TheoDoiHetHanHopDong
        {
            get
            {
                return _TheoDoiHetHanHopDong;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanHopDong", ref _TheoDoiHetHanHopDong, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn nghỉ hưởng BHXH")]
        public bool TheoDoiHetHanNghiBHXH
        {
            get
            {
                return _TheoDoiHetHanNghiBHXH;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanNghiBHXH", ref _TheoDoiHetHanNghiBHXH, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn nghỉ không hưởng lương")]
        public bool TheoDoiHetHanNghiKhongHuongLuong
        {
            get
            {
                return _TheoDoiHetHanNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanNghiKhongHuongLuong", ref _TheoDoiHetHanNghiKhongHuongLuong, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn tập sự")]
        public bool TheoDoiHetHanTapSu
        {
            get
            {
                return _TheoDoiHetHanTapSu;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanTapSu", ref _TheoDoiHetHanTapSu, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn thử việc")]
        public bool TheoDoiHetHanThuViec
        {
            get
            {
                return _TheoDoiHetHanThuViec;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanThuViec", ref _TheoDoiHetHanThuViec, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi hết hạn tạm hoãn tập sự")]
        public bool TheoDoiHetHanTamHoanTapSu
        {
            get
            {
                return _TheoDoiHetHanTamHoanTapSu;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanTamHoanTapSu", ref _TheoDoiHetHanTamHoanTapSu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi hết nhiệm kỳ chức vụ")]
        public bool TheoDoiHetNhiemKyChucVu
        {
            get
            {
                return _TheoDoiHetNhiemKyChucVu;
            }
            set
            {
                SetPropertyValue("TheoDoiHetNhiemKyChucVu", ref _TheoDoiHetNhiemKyChucVu, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết nhiệm kỳ chức vụ")]
        public int SoThangTruocKhiHetNhiemKyChucVu
        {
            get
            {
                return _SoThangTruocKhiHetNhiemKyChucVu;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetNhiemKyChucVu", ref _SoThangTruocKhiHetNhiemKyChucVu, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi đến hạn nâng lương")]
        public bool TheoDoiDenHanNangLuong
        {
            get
            {
                return _TheoDoiDenHanNangLuong;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangLuong", ref _TheoDoiDenHanNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi đến hạng nâng thâm niên nhà giáo")]
        public bool TheoDoiDenHanNangThamNien
        {
            get
            {
                return _TheoDoiDenHanNangThamNien;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangThamNien", ref _TheoDoiDenHanNangThamNien, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi đến hạng nâng thâm niên công tác")]
        public bool TheoDoiDenHanNangThamNienCongTac
        {
            get
            {
                return _TheoDoiDenHanNangThamNienCongTac;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangThamNienCongTac", ref _TheoDoiDenHanNangThamNienCongTac, value);
            }
        }

        [ModelDefault("Caption", "Theo dõi sinh nhật")]
        public bool TheoDoiSinhNhat
        {
            get
            {
                return _TheoDoiSinhNhat;
            }
            set
            {
                SetPropertyValue("TheoDoiSinhNhat", ref _TheoDoiSinhNhat, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi đến tuổi nghỉ hưu")]
        public bool TheoDoiDenTuoiNghiHuu
        {
            get
            {
                return _TheoDoiDenTuoiNghiHuu;
            }
            set
            {
                SetPropertyValue("TheoDoiDenTuoiNghiHuu", ref _TheoDoiDenTuoiNghiHuu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi đi học nước ngoài")]
        public bool TheoDoiDiHocNuocNgoai
        {
            get
            {
                return _TheoDoiDiHocNuocNgoai;
            }
            set
            {
                SetPropertyValue("TheoDoiDiHocNuocNgoai", ref _TheoDoiDiHocNuocNgoai, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi tạm giữ lương")]
        public bool TheoDoiHetHanTamGiuLuong
        {
            get
            {
                return _TheoDoiHetHanTamGiuLuong;
            }
            set
            {
                SetPropertyValue("TheoDoiHetHanTamGiuLuong", ref _TheoDoiHetHanTamGiuLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Theo dõi nâng thâm niên HC")]
        public bool TheoDoiDenHanNangThamNienHC
        {
            get
            {
                return _TheoDoiDenHanNangThamNienHC;
            }
            set
            {
                SetPropertyValue("TheoDoiDenHanNangThamNienHC", ref _TheoDoiDenHanNangThamNienHC, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi nghỉ hưu")]
        public int SoThangTruocKhiNghiHuu
        {
            get
            {
                return _SoThangTruocKhiNghiHuu;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNghiHuu", ref _SoThangTruocKhiNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn hợp đồng")]
        public int SoThangTruocKhiHetHanHopDong
        {
            get
            {
                return _SoThangTruocKhiHetHanHopDong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanHopDong", ref _SoThangTruocKhiHetHanHopDong, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi nâng lương")]
        public int SoThangTruocKhiNangLuong
        {
            get
            {
                return _SoThangTruocKhiNangLuong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNangLuong", ref _SoThangTruocKhiNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi thâm niên")]
        public int SoThangTruocKhiNangThamNien
        {
            get
            {
                return _SoThangTruocKhiNangThamNien;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiNangThamNien", ref _SoThangTruocKhiNangThamNien, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn nghỉ không lương")]
        public int SoThangTruocKhiHetHanNghiKhongLuong
        {
            get
            {
                return _SoThangTruocKhiHetHanNghiKhongLuong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanNghiKhongLuong", ref _SoThangTruocKhiHetHanNghiKhongLuong, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn nghỉ thai sản")]
        public int SoThangTruocKhiHetHanNghiThaiSan
        {
            get
            {
                return _SoThangTruocKhiHetHanNghiThaiSan;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanNghiThaiSan", ref _SoThangTruocKhiHetHanNghiThaiSan, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi đến sinh nhật")]
        public int SoThangTruocKhiDenSinhNhat
        {
            get
            {
                return _SoThangTruocKhiDenSinhNhat;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiDenSinhNhat", ref _SoThangTruocKhiDenSinhNhat, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn đi học")]
        public int SoThangTruocKhiHetHanDiHoc
        {
            get
            {
                return _SoThangTruocKhiHetHanDiHoc;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanDiHoc", ref _SoThangTruocKhiHetHanDiHoc, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn tạm giữ lương")]
        public int SoThangTruocKhiHetHanTamGiuLuong
        {
            get
            {
                return _SoThangTruocKhiHetHanTamGiuLuong;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanTamGiuLuong", ref _SoThangTruocKhiHetHanTamGiuLuong, value);
            }
        }

        [ModelDefault("Caption", "Số tháng trước khi hết hạn nâng thâm niên HC")]
        public int SoThangTruocKhiHetHanNangThamNienHC
        {
            get
            {
                return _SoThangTruocKhiHetHanNangThamNienHC;
            }
            set
            {
                SetPropertyValue("SoThangTruocKhiHetHanNangThamNienHC", ref _SoThangTruocKhiHetHanNangThamNienHC, value);
            }
        }
        public CauHinhNhacViec(Session session) : base(session) { }

    }

}
