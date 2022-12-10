using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhHuongCheDoDiHoc : Non_QuyetDinh
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo")]
        public string HinhThucDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia")]
        public string QuocGia { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo")]
        public string TruongDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Ngành đào tạo")]
        public string NganhDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo")]
        public string TrinhDoChuyenMon { get; set; }
        [System.ComponentModel.DisplayName("Tên khóa đào tạo")]
        public string TenKhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí")]
        public string NguonKinhPhi { get; set; }
        [System.ComponentModel.DisplayName("Trường hỗ trợ")]
        public string TruongHoTro { get; set; }
        [System.ComponentModel.DisplayName("Thời gian")]
        public string KhoaDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Thời gian đào tạo")]
        public string ThoiGianDaoTao { get; set; }
        [System.ComponentModel.DisplayName("Nước đào tạo")]
        public string NuocDaoTao { get; set; }

        public Non_QuyetDinhHuongCheDoDiHoc()
        {
            Master = new List<Non_ChiTietQuyetDinhHuongCheDoDiHocMaster>();
            Detail = new List<Non_ChiTietQuyetDinhHuongCheDoDiHocDetail>();
        }
    }
}
