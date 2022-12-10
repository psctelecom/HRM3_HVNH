using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhThoiChucKiemNhiem : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Tại đơn vị")]
        public string TaiDonVi { get; set; }
    }
}
