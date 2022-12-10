using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongDanTapSuCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức danh cán bộ hướng dẫn")]
        public string ChucDanhCanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Cán bộ hướng dẫn")]
        public string CanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị cán bộ hướng dẫn")]
        public string DonViCanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Số tháng")]
        public string SoThang { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("HSPC trách nhiệm")]
        public string HSPCTrachNhiem { get; set; }

        [System.ComponentModel.DisplayName("Danh xưng cán bộ hướng dẫn viết hoa")]
        public string DanhXungCanBoHuongDanVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng cán bộ hướng dẫn viết thường")]
        public string DanhXungCanBoHuongDanVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Chức vụcán bộ hướng dẫn")]
        public string ChucVuCanBoHuongDanVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày xác nhận")]
        public string NgayXacNhan { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định tuyển dung")]
        public string SoQDTD { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định tuyển dụng")]
        public string NgayQDTD { get; set; }


    }
}
