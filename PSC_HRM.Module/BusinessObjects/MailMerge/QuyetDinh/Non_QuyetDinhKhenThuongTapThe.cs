using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhKhenThuongTapThe : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Danh hiệu")]
        public string DanhHieu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Số tiển thưởng")]
        public string SoTienThuong { get; set; }
        [System.ComponentModel.DisplayName("Số tiển thưởng bằng chữ")]
        public string SoTienThuongBangChu { get; set; }

        public Non_QuyetDinhKhenThuongTapThe()
        {
            Master1 = new List<Non_ChiTietQuyetDinhKhenThuongTapTheMaster>();
            Detail1 = new List<Non_ChiTietQuyetDinhKhenThuongTapTheDetail>();
        }
    }
}
