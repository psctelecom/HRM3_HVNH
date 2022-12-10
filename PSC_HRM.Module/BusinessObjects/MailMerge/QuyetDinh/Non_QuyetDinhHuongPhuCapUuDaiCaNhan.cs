using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongPhuCapUuDaiCaNhan : Non_QuyetDinhNhanVien
    {
        
        [System.ComponentModel.DisplayName("Năm")]
        public string Nam { get; set; }
        [System.ComponentModel.DisplayName("Phụ cấp ưu đãi")]
        public string PhuCapUuDai { get; set; }
        
    }
}
