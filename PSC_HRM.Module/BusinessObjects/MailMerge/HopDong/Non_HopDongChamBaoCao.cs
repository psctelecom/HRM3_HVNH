using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongChamBaoCao : Non_HopDong
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Mã số thuế trường")]
        public string MaSoThueTruong { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản trường")]
        public string SoTaiKhoanTruong { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng trường")]
        public string NganHangTruong { get; set; }
        [System.ComponentModel.DisplayName("Mã số thuế người lao động")]
        public string MaSoThueNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản người lao động")]
        public string SoTaiKhoanNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng người lao động")]
        public string NganHangNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Môn dạy")]
        public string MonDay { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền")]
        public string TongTien { get; set; }
        [System.ComponentModel.DisplayName("Tổng tiền bằng chữ")]
        public string TongTienBangChu { get; set; }

        public Non_HopDongChamBaoCao()
        {
            Master = new List<Non_ChiTietHopDongChamBaoCao>();
        }
    }
}
