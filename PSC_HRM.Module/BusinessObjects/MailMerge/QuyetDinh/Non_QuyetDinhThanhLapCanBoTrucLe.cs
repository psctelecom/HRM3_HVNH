using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapCanBoTrucLe : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày đề nghị")]
        public string NgayDeNghi { get; set; }
        [System.ComponentModel.DisplayName("Nội dung trực lễ viết thường")]
        public string NoiDungVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Nội dung trực lễ viết hoa")]
        public string NoiDungVietHoa { get; set; }

        public Non_QuyetDinhThanhLapCanBoTrucLe()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapCanBoTrucLeDetail>();
        }
    }
}
