﻿using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNghiThaiSan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin nghỉ")]
        public string NgayXinNghi { get; set; }
        [System.ComponentModel.DisplayName("Số sổ BHXH")]
        public string SoSoBHXH { get; set; }
    }
}
