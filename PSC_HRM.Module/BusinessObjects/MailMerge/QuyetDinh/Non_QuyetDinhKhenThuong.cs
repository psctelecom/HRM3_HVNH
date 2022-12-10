using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhKhenThuong : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Danh hiệu")]
        public string DanhHieu { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Số tiền thưởng")]
        public string SoTienThuong { get; set; }
        [System.ComponentModel.DisplayName("Số tiền thưởng bằng chữ")]
        public string SoTienThuongBangChu { get; set; }

        public Non_QuyetDinhKhenThuong()
        {
            Master = new List<Non_ChiTietQuyetDinhKhenThuongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhKhenThuongCaNhanDetail>();
            Master1 = new List<Non_ChiTietQuyetDinhKhenThuongMaster>();
            Detail1 = new List<Non_ChiTietQuyetDinhKhenThuongTapTheDetail>();
        }
    }
}
