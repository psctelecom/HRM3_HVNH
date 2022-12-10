using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDongThinhGiangDetail : Non_MergeItem
    {

        [System.ComponentModel.DisplayName("Số tiết")]
        public string SoTiet { get; set; }
        [System.ComponentModel.DisplayName("Số tiền một tiết")]
        public string SoTien1Tiet { get; set; }
        [System.ComponentModel.DisplayName("Bộ môn")]
        public string BoMon { get; set; }
        [System.ComponentModel.DisplayName("Tại khoa")]
        public string TaiKhoa { get; set; }
        [System.ComponentModel.DisplayName("Môn học")]
        public string MonHoc { get; set; }
        [System.ComponentModel.DisplayName("Đơn Vị Tính")]
        public string DonViTinh { get; set; }
        [System.ComponentModel.DisplayName("Số Lượng")]
        public string SoLuong { get; set; }
        [System.ComponentModel.DisplayName("Nội Dung")]
        public string NoiDung { get; set; }
        [System.ComponentModel.DisplayName("Thành Tiền")]
        public string ThanhTien { get; set; }
        [System.ComponentModel.DisplayName("Đơn Giá")]
        public string DonGia { get; set; }
        [System.ComponentModel.DisplayName("Tổng Giá Trị Hợp Đồng")]
        public string TongGTHD { get; set; }
    }
}
