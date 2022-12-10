using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace PSC_HRM.Module.MailMerge.HopDong
{
    public class Non_HopDong : IMailMergeBase
    {
        [System.ComponentModel.Browsable(false)]
        public string Oid { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết hoa")]
        public string TenTruongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết thường")]
        public string TenTruongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ trường")]
        public string DiaChi { get; set; }
        [System.ComponentModel.DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }
        [System.ComponentModel.DisplayName("Loại hợp đồng")]
        public string LoaiHopDong { get; set; }
        [System.ComponentModel.DisplayName("Số hợp đồng")]
        public string SoHopDong { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký")]
        public string NgayKy { get; set; }
        [System.ComponentModel.DisplayName("Ngày ký date")]
        public string NgayKyDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký viết thường")]
        public string ChucVuNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký viết hoa")]
        public string ChucVuNguoiKyVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng người ký")]
        public string DanhXungNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Người ký viết hoa")]
        public string NguoiKyVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Người ký viết thường")]
        public string NguoiKyVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người ký")]
        public string ChucDanhNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người lao động")]
        public string ChucDanhNguoiLaoDong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng NLĐ viết hoa")]
        public string DanhXungNLDVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng NLĐ viết thường")]
        public string DanhXungNLDVietThường { get; set; }
        [System.ComponentModel.DisplayName("Người lao động viết hoa")]
        public string NguoiLaoDongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Người lao động viết thường")]
        public string NguoiLaoDongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [System.ComponentModel.DisplayName("Quốc tịch")]
        public string QuocTich { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh date")]
        public string NgaySinhDate { get; set; }
        [System.ComponentModel.DisplayName("Nơi sinh")]
        public string NoiSinh { get; set; }
        [System.ComponentModel.DisplayName("Quê quán")]
        public string QueQuan { get; set; }
        [System.ComponentModel.DisplayName("Trình độ")]
        public string TrinhDo { get; set; }
        [System.ComponentModel.DisplayName("Học hàm")]
        public string HocHam { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị người ký")]
        public string DonViNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chuyên môn")]
        public string ChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Năm tốt nghiệp")]
        public string NamTotNghiep { get; set; }
        [System.ComponentModel.DisplayName("Số sổ bảo hiểm xã hội")]
        public string SoSoBHXH { get; set; }
        [System.ComponentModel.DisplayName("ĐTDĐ người lao động")]
        public string DienThoaiDiDong { get; set; }
        [System.ComponentModel.DisplayName("ĐT nhà riêng người lao động")]
        public string DienThoaiNhaRieng { get; set; }
        [System.ComponentModel.DisplayName("Địa chỉ thường trú")]
        public string DiaChiThuongTru { get; set; }
        [System.ComponentModel.DisplayName("Nơi ở hiện nay")]
        public string NoiOHienNay { get; set; }
        [System.ComponentModel.DisplayName("Số chứng minh nhân dân")]
        public string SoCMND { get; set; }
        [System.ComponentModel.DisplayName("Ngày cấp")]
        public string NgayCap { get; set; }
        [System.ComponentModel.DisplayName("Ngày cấp date")]
        public string NgayCapDate { get; set; }
        [System.ComponentModel.DisplayName("Nơi cấp")]
        public string NoiCap { get; set; }
        [System.ComponentModel.DisplayName("Chức danh chuyên môn")]
        public string ChucDanhChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Địa điểm làm việc")]
        public string DiaDiemLamViec { get; set; }
        [System.ComponentModel.DisplayName("Căn cứ")]
        public string CanCu { get; set; }
        [System.ComponentModel.DisplayName("Email")]
        public string Email { get; set; }
        [System.ComponentModel.DisplayName("Mã số thuế người lao động")]
        public string MaSoThue { get; set; }
        [System.ComponentModel.DisplayName("Số tài khoản")]
        public string SoTaiKhoan { get; set; }
        [System.ComponentModel.DisplayName("Ngân hàng")]
        public string NganHang { get; set; }
        [System.ComponentModel.DisplayName("Mã ngân hàng")]
        public string MaNganHang { get; set; }
        [System.ComponentModel.DisplayName("Công việc tuyển dụng")]
        public string CongViecTuyenDung { get; set; }

        public IList Master { get; set; }
        public IList Master1 { get; set; }
        public IList Detail { get; set; }
        public IList Detail1 { get; set; }
    }
}
