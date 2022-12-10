using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhBoiDuongCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức")]
        public string DonViToChuc { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị tổ chức theo chương trình")]
        public string DonViToChucTheoChuongTrinh { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm")]
        public string DiaDiem { get; set; }
        [System.ComponentModel.DisplayName(" Mã ngành đào tạo")]
        public string MaNganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm theo chương trình")]
        public string DiaDiemTheoChuongTrinh { get; set; }
        [System.ComponentModel.DisplayName("Nội dung bồi dưỡng")]
        public string NoiDungBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Chứng chỉ")]
        public string ChungChi { get; set; }
        [System.ComponentModel.DisplayName("Số công văn")]
        public string SoCongVan { get; set; }
        [System.ComponentModel.DisplayName("Chương trình bồi dưỡng")]
        public string ChuongTrinhBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Nơi bồi dưỡng")]
        public string NoiBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Loại hình bồi dưỡng")]
        public string LoaiHinhBoiDuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian")]
        public string ThoiGian { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ nhân viên được thay thế")]
        public string ChucVuNhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Chức danh nhân viên được thay thế")]
        public string ChucDanhNhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Mã đơn vị nhân viên được thay thế")]
        public string MaDonViNhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Tên đơn vị nhân viên được thay thế")]
        public string TenDonViNhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Tên viết tắt đơn vị nhân viên được thay thế")]
        public string TenVietTatDonViNhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Nhân viên được thay thế")]
        public string NhanVienThayThe { get; set; }
        [System.ComponentModel.DisplayName("Số tiền")]
        public string SoTien { get; set; }
        [System.ComponentModel.DisplayName("Số tiền bằng chữ")]
        public string SoTienBangChu { get; set; }
        [System.ComponentModel.DisplayName("Ngày khai giảng")]
        public string NgayKhaiGiang { get; set; }
    }
}
