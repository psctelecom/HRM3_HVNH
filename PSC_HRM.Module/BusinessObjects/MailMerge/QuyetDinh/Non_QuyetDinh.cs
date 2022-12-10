using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinh : IMailMergeBase
    {
        [System.ComponentModel.Browsable(false)]
        public string Oid { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị chủ quản")]
        public string DonViChuQuan { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết hoa")]
        public string TenTruongVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết thường")]
        public string TenTruongVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Tên trường viết tắt")]
        public string TenTruongVietTat { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định")]
        public string SoQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Số phiếu trình")]
        public string SoPhieuTrinh { get; set; }

        [System.ComponentModel.DisplayName("Ngày phiếu trình")]
        public string NgayPhieuTrinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định")]
        public string NgayQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Năm quyết định 1")]
        public string NamQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Quý quyết định")]
        public string QuyQuyetDinh { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực")]
        public string NgayHieuLuc { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định (Date)")]
        public string NgayQuyetDinhDate { get; set; }
        [System.ComponentModel.DisplayName("Ngày hiệu lực (Date)")]
        public string NgayHieuLucDate { get; set; }
        [System.ComponentModel.DisplayName("Căn cứ")]
        public string CanCu { get; set; }
        [System.ComponentModel.DisplayName("Về việc")]
        public string NoiDung { get; set; }
        [System.ComponentModel.DisplayName("Chức danh người ký")]
        public string ChucDanhNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký")]
        public string ChucVuNguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ người ký viết thường")]
        public string ChucVuNguoiKyVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Người ký")]
        public string NguoiKy { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng người ký viết thường")]
        public string DanhXungNguoiKyVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng người ký viết hoa")]
        public string DanhXungNguoiKyVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Số lượng cán bộ")]
        public string SoLuongCanBo { get; set; }
        [System.ComponentModel.DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
        [System.ComponentModel.DisplayName("Cử đoàn")]
        public string CuDoan { get; set; }       

        public IList Master { get; set; }
        public IList Detail { get; set; }

        public IList Master1 { get; set; }
        public IList Detail1 { get; set; }
    }

}
