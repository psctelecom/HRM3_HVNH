using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChiTienThuongTienSiCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Học vị")]
        public string HocVi { get; set; }
    }
}
