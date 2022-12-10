using DevExpress.ExpressApp.Model;
using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class PMS_MailMegre_HopDongThinhGiang_ThongTinDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Tên học phần")]
        public string TenHocPhan { get; set; }
        [DisplayName("Số tiết lý thuyết")]
        public string SoTietLT { get; set; }
        [DisplayName("Số tiết thực hành")]
        public string SoTietTH { get; set; }
        [DisplayName("Số tiết khác")]
        public string SoTietKhac { get; set; }
        [DisplayName("Số tiết")]
        public string SiSo { get; set; }
        [DisplayName("Số tín chỉ")]
        public string SoTinChi { get; set; }
        [DisplayName("Tiết quy đổi")]
        public string TietQuyDoi { get; set; }
        [DisplayName("Đơn giá")]
        public string DonGia { get; set; }
        [DisplayName("Địa điểm dạy")]
        public string DiaDiemDay { get; set; }
        [DisplayName("Đơn giá khác")]
        public string DonGiaKhac { get; set; }
    }
}
