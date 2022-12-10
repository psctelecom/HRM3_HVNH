using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhHuongDanTapSuDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Chức danh cán bộ hướng dẫn")]
        public string ChucDanhCanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Cán bộ hướng dẫn")]
        public string CanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị cán bộ hướng dẫn")]
        public string DonViCanBoHuongDan { get; set; }
        [System.ComponentModel.DisplayName("Số tháng")]
        public string SoThang { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("HSPC trách nhiệm")]
        public string HSPCTrachNhiem { get; set; }
    }
}
