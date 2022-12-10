using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhanNghiThaiSan : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }

        public Non_QuyetDinhTiepNhanNghiThaiSan()
        {
            Master = new List<Non_ChiTietQuyetDinhTiepNhanNghiThaiSanMaster>();
            Detail = new List<Non_ChiTietQuyetDinhTiepNhanNghiThaiSanDetail>();
        }
    }
}
