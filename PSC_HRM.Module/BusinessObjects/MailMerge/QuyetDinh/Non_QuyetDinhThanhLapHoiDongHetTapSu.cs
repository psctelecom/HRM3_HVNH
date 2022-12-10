using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapHoiDongHetTapSu : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Đợt")]
        public string Dot { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị của tập sự viết thường")]
        public string DonViTapSuVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị của tập sự viết hoa")]
        public string DonViTapSuVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Tên tập sự")]
        public string TenTapSu { get; set; }

        public Non_QuyetDinhThanhLapHoiDongHetTapSu()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapHoiDongHetTapSuDetail>();
        }
    }
}
