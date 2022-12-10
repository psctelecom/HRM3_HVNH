using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhBoiDuongMaster : Non_MergeItem
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
        [System.ComponentModel.DisplayName("Ngày quyết định (Date)")]
        public string NgayQuyetDinhDate { get; set; }
        [System.ComponentModel.DisplayName("Tổng nhân viên")]
        public int TongNhanVien { get; set; }

        [System.ComponentModel.DisplayName("Nội dung bồi dưỡng")]
        public string NoiDungBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Loại hình bồi dưỡng")]
        public string LoaiHinhBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Cử đoàn")]
        public string CuDoan { get; set; }
        [System.ComponentModel.DisplayName("Về việc viết in hoa")]
        public string NoiDungVietHoa { get; set; }
    }
}
