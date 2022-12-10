using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapHoiDongKhenThuong : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }

        public Non_QuyetDinhThanhLapHoiDongKhenThuong()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapHoiDongKhenThuongDetail>();
        }
    }
}
