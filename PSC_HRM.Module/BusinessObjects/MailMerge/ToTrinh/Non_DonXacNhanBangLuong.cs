using System;
using System.Collections;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.ToTrinh
{
    public class Non_DonXacNhanBangLuong : IMailMergeBase
    {
        [DisplayName("Oid")]
        public string Oid { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [DisplayName("Lý do")]
        public string LyDo { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Danh xưng nhân viên viết thường")]
        public string DanhXungVietThuong { get; set; }
        [DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [DisplayName("Bậc lương")]
        public string BacLuong { get; set; }
        [DisplayName("Hệ số lương")]
        public string HeSoLuong { get; set; }        
        [DisplayName("HSPC chức vụ")]
        public string HSPCChucVu { get; set; }
        [DisplayName("HSPC vượt khung")]
        public string HSPCVuotKhung { get; set; }
        [DisplayName("HSPC thâm niên")]
        public string HSPCThamNien { get; set; }
        [DisplayName("HSPC giảng dạy")]
        public string HSPCUuDai { get; set; }
        [DisplayName("Bảo hiểm")]
        public string BaoHiem { get; set; }
        [DisplayName("Thu nhập tăng thêm")]
        public string ThuNhapTangThem { get; set; }
        [DisplayName("PC ăn trưa")]
        public string PhuCapAnTrua { get; set; }
        [DisplayName("PC quản lý")]
        public string PhuCapQuanLy { get; set; }
        [DisplayName("Thực lĩnh")]
        public string TongThucLinh { get; set; }
        [DisplayName("Tiền bằng chữ")]
        public string TienBangChu { get; set; }
        [DisplayName("Từ tháng")]
        public string TuThang { get; set; }
        [DisplayName("Đến tháng")]
        public string DenThang { get; set; }
        [DisplayName("Người ký")]
        public string NguoiKy { get; set; }
        [DisplayName("Chức danh người ký")]
        public string ChucDanhNguoiKy { get; set; }
        [DisplayName("Chức vụ người ký")]
        public string ChucVuNguoiKy { get; set; }
        [DisplayName("Ngày lập đơn")]
        public string NgayLapDon { get; set; }

        public IList Master { get; set; }
        public IList Detail { get; set; }

        public IList Master1 { get; set; }
        public IList Detail1 { get; set; }
    }
}
