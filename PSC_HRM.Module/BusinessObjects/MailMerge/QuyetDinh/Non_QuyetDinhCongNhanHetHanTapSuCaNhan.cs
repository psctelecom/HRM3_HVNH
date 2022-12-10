using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhCongNhanHetHanTapSuCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ tháng")]
        public string TuThang { get; set; }

        [System.ComponentModel.DisplayName("Ngày hết hạn tập sự (date)")]
        public string NgayHetHanTapSuDate { get; set; }
        [System.ComponentModel.DisplayName("Giảng dạy tại")]
        public string TaiBoMon { get; set; }
        [System.ComponentModel.DisplayName("% Ưu đãi")]
        public string PhuCapUuDai { get; set; }
    }
}
