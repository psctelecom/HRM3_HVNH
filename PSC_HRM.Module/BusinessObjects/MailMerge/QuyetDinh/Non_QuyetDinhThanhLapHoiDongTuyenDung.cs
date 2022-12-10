using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapHoiDongTuyenDung : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Năm học")]
        public string NamHoc { get; set; }
        [System.ComponentModel.DisplayName("Đợt")]
        public string Dot { get; set; }
        [System.ComponentModel.DisplayName("Cán bộ")]
        public string CanBo{ get; set; }
        [System.ComponentModel.DisplayName("Bộ phận")]
        public string BoPhan { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        
        public Non_QuyetDinhThanhLapHoiDongTuyenDung()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapHoiDongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapHoiDongTuyenDungDetail>();
        }
    }
}
