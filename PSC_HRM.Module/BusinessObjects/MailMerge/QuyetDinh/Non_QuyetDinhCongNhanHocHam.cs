using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhCongNhanHocHam : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Học hàm mới")]
        public string HocHamMoi { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (Date)")]
        public string TuNgayDate { get; set; }
    }
}
