using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongDanTapSu : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Số quyết định tuyển dung")]
        public string SoQDTD { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định tuyển dụng")]
        public string NgayQDTD { get; set; }
        [System.ComponentModel.DisplayName("Năm tuyển dụng")]
        public string NamTD { get; set; }    
        public Non_QuyetDinhHuongDanTapSu()
        {
            Master = new List<Non_ChiTietQuyetDinhHuongDanTapSuMaster>();
            Detail = new List<Non_ChiTietQuyetDinhHuongDanTapSuDetail>();
        }
    }
}
