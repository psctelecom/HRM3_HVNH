using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapHoiDongXetDenBuDaoTao : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Đợt")]
        public string Dot { get; set; }
        [System.ComponentModel.DisplayName("Tên đền bù viết thường")]
        public string TenDenBuVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Tên đền bù viết hoa")]
        public string TenDenBuVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị đền bù viết thường")]
        public string DonViDenBuVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị đền bù viết hoa")]
        public string DonViDenBuVietHoa { get; set; }

        public Non_QuyetDinhThanhLapHoiDongXetDenBuDaoTao()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapHoiDongXetDenBuDaoTaoDetail>();
        }
    }
}
