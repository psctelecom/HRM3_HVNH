using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ThongBaoNghiPhep : Non_QuyetDinh
    {
        [System.ComponentModel.Browsable(false)]
        public string Oid { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết thường")]
        public string TenTruongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết hoa")]
        public string TenTruongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Ngày thông báo")]
        public string NgayThongBao { get; set; }
        [System.ComponentModel.DisplayName("Ngày thông báo (date)")]
        public string NgayThongBaoDate { get; set; }
        [System.ComponentModel.DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [System.ComponentModel.DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [System.ComponentModel.DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Nơi ở hiện nay")]
        public string NoiOHienNay { get; set; }
        [System.ComponentModel.DisplayName("Ngày nghỉ hưu")]
        public string NgayNghiHuu { get; set; }
        [System.ComponentModel.DisplayName("Hiệu trưởng")]
        public string HieuTruong { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người ký")]
        public string ChucDanhNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Trưởng phòng TCCB")]
        public string TruongPhongTCCB { get; set; }
        [System.ComponentModel.DisplayName("Chức danh trưởng phòng TCCB")]
        public string ChucDanhTPTCCB { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng viết thường")]
        public string DanhXungVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Loại nhân viên")]
        public string LoaiNhanVien { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Thời gian nghỉ phép")]
        public string ThoiGianNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày xin nghỉ phép")]
        public string NgayDuocNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày bắt đầu nghỉ phép")]
        public string NgayBatDauNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Ngày kết thúc nghỉ phép")]
        public string NgayKetThucNghiPhep { get; set; }
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm nghỉ phép")]
        public string DiaDiemNghi { get; set; }

        public Non_ThongBaoNghiPhep()
        {
            Master = new List<Non_ChiTietThongBaoNghiPhepMaster>();
            Detail = new List<Non_ChiTietThongBaoNghiPhepDetail>();
        }
    }
}
