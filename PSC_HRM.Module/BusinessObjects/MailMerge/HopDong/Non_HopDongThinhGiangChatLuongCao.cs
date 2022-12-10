using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongThinhGiangChatLuongCao : Non_HopDong
    {
        [System.ComponentModel.DisplayName("Môn dạy")]
        public string MonDay { get; set; }
        [System.ComponentModel.DisplayName("Học kỳ")]
        public string HocKy { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản người lao động")]
        public string SoTaiKhoanNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng người lao động")]
        public string NganHangNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ ngân hàng người lao động")]
        public string DiaChiNganHangNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng NLĐ viết hoa")]
        public string DanhXungNguoiLaoDongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Học vị")]
        public string HocVi { get; set; }
        [System.ComponentModel.DisplayName("Số tiết lý thuyết")]
        public string SoTietLyThuyet { get; set; }
        [System.ComponentModel.DisplayName("Số tiết thực hành")]
        public string SoTietThucHanh { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Số tiền 1 tiết")]
        public string SoTien1Tiet { get; set; }
        public Non_HopDongThinhGiangChatLuongCao()
        {
            Master = new List<Non_HopDongThinhGiangMaster>();
            Detail = new List<Non_HopDongThinhGiangDetail>();
        }
    }
}
