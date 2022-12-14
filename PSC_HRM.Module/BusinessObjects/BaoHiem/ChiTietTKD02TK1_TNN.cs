using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết TKD02TK1_TNN")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "TKD02TK1_TNN")]
    public class ChiTietTKD02TK1_TNN : BaoMatBaseObject
    {
        private TKD02TK1_TNN _TKD02TK1_TNN;
        private string _IDLaoDong;
        private string _HoTen;
        private string _MaSoBHXH;
        private string _Loai;
        private string _PA;
        private string _CMND;
        private string _DinhDangNgaySinh;
        private string _NgaySinh;
        private string _GioiTinh;
        private string _CoGiamChet;
        private string _NgayChet;
        private string _ChucVu;
        private string _TienLuong;
        private string _PhuCapCV;
        private string _PhuCapTNVK;
        private string _PhuCapTNNghe;
        private string _PhuCapLuong;
        private string _PhuCapBoSung;
        private string _TuThang;
        private string _DenThang;
        private string _GhiChu;
        private string _TyLeDong;
        private string _TinhLai;
        private string _DaCoSo;
        private string _MucHuongBHYT;
        private string _MaPhongBan;
        private string _NoiLamViec;
        private string _MaVungSinhSong;
        private string _MaVungLuongToiThieu;
        private string _VTVL_NQL_TuNgay;
        private string _VTVL_NQL_DenNgay;
        private string _VTVL_CMKTBC_TuNgay;
        private string _VTVL_CMKTBC_DenNgay;
        private string _VTVL_CMKTBT_TuNgay;
        private string _VTVL_CMKTBT_DenNgay;
        private string _VTVL_Khac_TuNgay;
        private string _VTVL_Khac_DenNgay;
        private string _NNDH_TuNgay;
        private string _NNDH_DenNgay;
        private string _HDLD_TuNgay;
        private string _HDLD_XDTH_TuNgay;
        private string _HDLD_XDTH_DenNgay;
        private string _HDLD_Khac_TuNgay;
        private string _HDLD_Khac_DenNgay;
        private string _ADD_TK1_TS;


        [ImmediatePostData]
        [ModelDefault("Caption", "TKD02TK1_TNN")]
        [Association("TKD02TK1_TNN-ListChiTietTKD02TK1_TNN")]
        public TKD02TK1_TNN TKD02TK1_TNN
        {
            get
            {
                return _TKD02TK1_TNN;
            }
            set
            {
                SetPropertyValue("TKD02TK1_TNN", ref _TKD02TK1_TNN, value);
            }
        }


        [ModelDefault("Caption", "ID lao động")]
        public string IDLaoDong
        {
            get
            {
                return _IDLaoDong;
            }
            set
            {
                SetPropertyValue("IDLaoDong", ref _IDLaoDong, value);
            }
        }


        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Mã số BHXH")]
        public string MaSoBHXH
        {
            get
            {
                return _MaSoBHXH;
            }
            set
            {
                SetPropertyValue("MaSoBHXH", ref _MaSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Loại khai báo")]
        public string Loai
        {
            get
            {
                return _Loai;
            }
            set
            {
                SetPropertyValue("Loai", ref _Loai, value);
            }
        }

        [ModelDefault("Caption", "Phương án")]
        public string PA
        {
            get
            {
                return _PA;
            }
            set
            {
                SetPropertyValue("PA", ref _PA, value);
            }
        }

        [ModelDefault("Caption", "Chứng minh nhân dân")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Định dạng ngày sinh")]
        public string DinhDangNgaySinh
        {
            get
            {
                return _DinhDangNgaySinh;
            }
            set
            {
                SetPropertyValue("DinhDangNgaySinh", ref _DinhDangNgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public string NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public string GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Có giảm do chết")]
        public string CoGiamChet
        {
            get
            {
                return _CoGiamChet;
            }
            set
            {
                SetPropertyValue("CoGiamChet", ref _CoGiamChet, value);
            }
        }

        [ModelDefault("Caption", "Ngày chết")]
        public string NgayChet
        {
            get
            {
                return _NgayChet;
            }
            set
            {
                SetPropertyValue("NgayChet", ref _NgayChet, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public string ChucVu
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

        [ModelDefault("Caption", "Tiền lương")]
        public string TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp CV")]
        public string PhuCapCV
        {
            get
            {
                return _PhuCapCV;
            }
            set
            {
                SetPropertyValue("PhuCapCV", ref _PhuCapCV, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp TNVK")]
        public string PhuCapTNVK
        {
            get
            {
                return _PhuCapTNVK;
            }
            set
            {
                SetPropertyValue("PhuCapTNVK", ref _PhuCapTNVK, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp TN nghề")]
        public string PhuCapTNNghe
        {
            get
            {
                return _PhuCapTNNghe;
            }
            set
            {
                SetPropertyValue("PhuCapTNNghe", ref _PhuCapTNNghe, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp lương")]
        public string PhuCapLuong
        {
            get
            {
                return _PhuCapLuong;
            }
            set
            {
                SetPropertyValue("PhuCapLuong", ref _PhuCapLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp bổ sung")]
        public string PhuCapBoSung
        {
            get
            {
                return _PhuCapBoSung;
            }
            set
            {
                SetPropertyValue("PhuCapBoSung", ref _PhuCapBoSung, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng")]
        public string TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng")]
        public string DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
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

        [ModelDefault("Caption", "Tỷ lệ đóng")]
        public string TyLeDong
        {
            get
            {
                return _TyLeDong;
            }
            set
            {
                SetPropertyValue("TyLeDong", ref _TyLeDong, value);
            }
        }

        [ModelDefault("Caption", "Tính lãi")]
        public string TinhLai
        {
            get
            {
                return _TinhLai;
            }
            set
            {
                SetPropertyValue("TinhLai", ref _TinhLai, value);
            }
        }

        [ModelDefault("Caption", "Đã có sổ")]
        public string DaCoSo
        {
            get
            {
                return _DaCoSo;
            }
            set
            {
                SetPropertyValue("DaCoSo", ref _DaCoSo, value);
            }
        }

        [ModelDefault("Caption", "Mức hưởng BHYT")]
        public string MucHuongBHYT
        {
            get
            {
                return _MucHuongBHYT;
            }
            set
            {
                SetPropertyValue("MucHuongBHYT", ref _MucHuongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Phòng ban")]
        public string MaPhongBan
        {
            get
            {
                return _MaPhongBan;
            }
            set
            {
                SetPropertyValue("MaPhongBan", ref _MaPhongBan, value);
            }
        }

        [ModelDefault("Caption", "Nơi làm việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        [ModelDefault("Caption", "Mã vùng sinh sống")]
        public string MaVungSinhSong
        {
            get
            {
                return _MaVungSinhSong;
            }
            set
            {
                SetPropertyValue("MaVungSinhSong", ref _MaVungSinhSong, value);
            }
        }

        [ModelDefault("Caption", "Mã vùng lương tối thiểu")]
        public string MaVungLuongToiThieu
        {
            get
            {
                return _MaVungLuongToiThieu;
            }
            set
            {
                SetPropertyValue("MaVungLuongToiThieu", ref _MaVungLuongToiThieu, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu vị trí quản lý")]
        public string VTVL_NQL_TuNgay
        {
            get
            {
                return _VTVL_NQL_TuNgay;
            }
            set
            {
                SetPropertyValue("VTVL_NQL_TuNgay", ref _VTVL_NQL_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc vị trí quản lý")]
        public string VTVL_NQL_DenNgay
        {
            get
            {
                return _VTVL_NQL_DenNgay;
            }
            set
            {
                SetPropertyValue("VTVL_NQL_DenNgay", ref _VTVL_NQL_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc cao")]
        public string VTVL_CMKTBC_TuNgay
        {
            get
            {
                return _VTVL_CMKTBC_TuNgay;
            }
            set
            {
                SetPropertyValue("VTVL_CMKTBC_TuNgay", ref _VTVL_CMKTBC_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc vị trí chuyên môn kĩ thuật bậc cao")]
        public string VTVL_CMKTBC_DenNgay
        {
            get
            {
                return _VTVL_CMKTBC_DenNgay;
            }
            set
            {
                SetPropertyValue("VTVL_CMKTBC_DenNgay", ref _VTVL_CMKTBC_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc trung")]
        public string VTVL_CMKTBT_TuNgay
        {
            get
            {
                return _VTVL_CMKTBT_TuNgay;
            }
            set
            {
                SetPropertyValue("VTVL_CMKTBT_TuNgay", ref _VTVL_CMKTBT_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc vị trí chuyên môn kĩ thuật bậc trung")]
        public string VTVL_CMKTBT_DenNgay
        {
            get
            {
                return _VTVL_CMKTBT_DenNgay;
            }
            set
            {
                SetPropertyValue("VTVL_CMKTBT_DenNgay", ref _VTVL_CMKTBT_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu vị trí khác")]
        public string VTVL_Khac_TuNgay
        {
            get
            {
                return _VTVL_Khac_TuNgay;
            }
            set
            {
                SetPropertyValue("VTVL_Khac_TuNgay", ref _VTVL_Khac_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc vị trí khác")]
        public string VTVL_Khac_DenNgay
        {
            get
            {
                return _VTVL_Khac_DenNgay;
            }
            set
            {
                SetPropertyValue("VTVL_Khac_DenNgay", ref _VTVL_Khac_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu ngành nặng nhọc độc hại")]
        public string NNDH_TuNgay
        {
            get
            {
                return _NNDH_TuNgay;
            }
            set
            {
                SetPropertyValue("NNDH_TuNgay", ref _NNDH_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc ngành nặng nhọc độc hại")]
        public string NNDH_DenNgay
        {
            get
            {
                return _NNDH_DenNgay;
            }
            set
            {
                SetPropertyValue("NNDH_DenNgay", ref _NNDH_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu HĐLĐ không xác định thời hạn")]
        public string HDLD_TuNgay
        {
            get
            {
                return _HDLD_TuNgay;
            }
            set
            {
                SetPropertyValue("HDLD_TuNgay", ref _HDLD_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu HĐLĐ xác định thời hạn")]
        public string HDLD_XDTH_TuNgay
        {
            get
            {
                return _HDLD_XDTH_TuNgay;
            }
            set
            {
                SetPropertyValue("HDLD_XDTH_TuNgay", ref _HDLD_XDTH_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc HĐLĐ xác định thời hạn")]
        public string HDLD_XDTH_DenNgay
        {
            get
            {
                return _HDLD_XDTH_DenNgay;
            }
            set
            {
                SetPropertyValue("HDLD_XDTH_DenNgay", ref _HDLD_XDTH_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu HĐLĐ khác")]
        public string HDLD_Khac_TuNgay
        {
            get
            {
                return _HDLD_Khac_TuNgay;
            }
            set
            {
                SetPropertyValue("HDLD_Khac_TuNgay", ref _HDLD_Khac_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc HĐLĐ")]
        public string HDLD_Khac_DenNgay
        {
            get
            {
                return _HDLD_Khac_DenNgay;
            }
            set
            {
                SetPropertyValue("HDLD_Khac_DenNgay", ref _HDLD_Khac_DenNgay, value);
            }
        }


        [ModelDefault("Caption", "Bổ sung TK1")]
        public string ADD_TK1_TS
        {
            get
            {
                return _ADD_TK1_TS;
            }
            set
            {
                SetPropertyValue("ADD_TK1_TS", ref _ADD_TK1_TS, value);
            }
        }

        public ChiTietTKD02TK1_TNN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

    }

}
