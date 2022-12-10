using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhTuyenDungMaster : Non_MergeItem
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
        [System.ComponentModel.DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
    }
}
