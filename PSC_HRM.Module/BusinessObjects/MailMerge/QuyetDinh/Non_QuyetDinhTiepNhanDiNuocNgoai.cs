using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTiepNhanDiNuocNgoai : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }

        public Non_QuyetDinhTiepNhanDiNuocNgoai()
        {
            Master = new List<Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiMaster>();
            Detail = new List<Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiDetail>();
        }
    }
}
