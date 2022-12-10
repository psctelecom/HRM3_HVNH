using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhTuyenDung : Non_QuyetDinh
    {
        public Non_QuyetDinhTuyenDung()
        {
            Master = new List<Non_ChiTietQuyetDinhTuyenDungMaster>();
            Detail = new List<Non_ChiTietQuyetDinhTuyenDungDetail>();
        }

        [System.ComponentModel.DisplayName("Đợt tuyển dụng")]
        public int DotTuyenDung { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTuyenDung { get; set; }
    }
}
