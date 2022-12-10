using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapHoiDongKyLuat : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }

        public Non_QuyetDinhThanhLapHoiDongKyLuat()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapHoiDongKyLuatDetail>();
        }
    }
}
