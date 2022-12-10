using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhMienNhiemKiemNhiem : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ kiêm nhiệm")]
        public string ChucVuKiemNhiem { get; set; }
        [System.ComponentModel.DisplayName("HSPC kiêm nhiệm")]
        public string HSPCKiemNhiem { get; set; }
        [System.ComponentModel.DisplayName("Tại đơn vị")]
        public string TaiDonVi { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ kiêm nhiệm mới")]
        public string ChucVuKiemNhiemMoi { get; set; }
        [System.ComponentModel.DisplayName("HSPC kiêm nhiệm mới")]
        public string HSPCKiemNhiemMoi { get; set; }
    }
}
