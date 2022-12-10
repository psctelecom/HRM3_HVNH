using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhThanhLapHoiDongMaster : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết hoa")]
        public string TenTruongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết thường")]
        public string TenTruongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký")]
        public string ChucVuNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Người ký")]
        public string NguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Tổng nhân viên")]
        public int TongNhanVien { get; set; }
        [System.ComponentModel.DisplayName("Đợt")]
        public string Dot { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị của tập sự ")]
        public string DonViTapSu { get; set; }
        [System.ComponentModel.DisplayName("Tên tập sự")]
        public string TenTapSu { get; set; }

        [System.ComponentModel.DisplayName("Nội dung trực lễ ")]
        public string NoiDungTrucLe { get; set; }
        
        
        
    }
}
