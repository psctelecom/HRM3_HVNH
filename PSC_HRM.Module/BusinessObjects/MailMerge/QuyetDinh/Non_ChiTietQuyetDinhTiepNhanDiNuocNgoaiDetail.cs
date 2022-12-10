using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhTiepNhanDiNuocNgoaiDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Mốc nâng lương lần sau")]
        public string MocNangLuongLanSau { get; set; }
        [DisplayName("Mốc nâng lương")]
        public string MocNangLuong { get; set; }
        [DisplayName("Mốc nâng lương điều chỉnh")]
        public string MocNangLuongDieuChinh { get; set; }
    }
}
