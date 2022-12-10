using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThanhLapKhac : Non_QuyetDinh
    {
        //[System.ComponentModel.DisplayName("Năm học")]
        //public string NamHoc { get; set; }
        //[System.ComponentModel.DisplayName("Đợt")]
        //public string Dot { get; set; }

        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }

        public Non_QuyetDinhThanhLapKhac()
        {
            Master = new List<Non_ChiTietQuyetDinhThanhLapKhacMaster>();
            Detail = new List<Non_ChiTietQuyetDinhThanhLapKhacDetail>();
            Master1 = new List<Non_ChiTietQuyetDinhThanhLapKhacMaster>();
            Detail1 = new List<Non_ChiTietQuyetDinhThanhLapKhacBoPhanDetail>();
        }
    }
}
