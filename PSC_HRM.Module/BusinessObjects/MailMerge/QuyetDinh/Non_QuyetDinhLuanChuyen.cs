using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhLuanChuyen : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Đơn vị cũ")]
        public string DonViCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký")]
        public string Ngayky { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị Mới")]
        public string DonViMoi { get; set; }
       
    }
}
