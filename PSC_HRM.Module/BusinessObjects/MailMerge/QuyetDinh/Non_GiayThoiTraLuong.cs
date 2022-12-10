using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_GiayThoiTraLuong : Non_QuyetDinh
    {
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }
        [DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [DisplayName("HSPC chức vụ")]
        public string HSPCChucVu { get; set; }
        [DisplayName("Phụ cấp vượt khung")]
        public string PhuCapVuotKhung { get; set; }
        [DisplayName("Phụ cấp thâm niên")]
        public string PhuCapThamNien { get; set; }
        [DisplayName("Phụ cấp ưu đãi")]
        public string PhuCapUuDai { get; set; }
        [DisplayName("Số sổ BHXH")]
        public string SoSoBHXH { get; set; }
        [DisplayName("Đóng bảo hiểm đến hết ngày")]
        public string DongBaoHiemDenHetNgay { get; set; }
        [DisplayName("Đề nghị đóng bảo hiểm từ ngày")]
        public string DeNghiDongBaoHiemTuNgay { get; set; }
        [DisplayName("Năm")]
        public string Nam { get; set; }
        [DisplayName("Số ngày nghỉ phép còn lại")]
        public string SoNgayNghiPhepConLai { get; set; }
    }
}
