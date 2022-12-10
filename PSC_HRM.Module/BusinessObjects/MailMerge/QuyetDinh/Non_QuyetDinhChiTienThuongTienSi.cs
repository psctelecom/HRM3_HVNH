using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChiTienThuongTienSi : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Học vị")]
        public string HocVi { get; set; }

        public Non_QuyetDinhChiTienThuongTienSi()
        {
            Master = new List<Non_ChiTietQuyetDinhChiTienThuongTienSiMaster>();
            Detail = new List<Non_ChiTietQuyetDinhChiTienThuongTienSiDetail>();
        }
    }
}
