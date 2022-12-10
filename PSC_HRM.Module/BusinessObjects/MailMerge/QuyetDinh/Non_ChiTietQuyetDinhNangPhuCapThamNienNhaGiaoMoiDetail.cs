using System;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiDetail : Non_MergeItem
    {
        [DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }
        [DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Ngày tính thâm niên")]
        public string NgayTinhThamNien { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên mới")]
        public string ThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên mới")]
        public string NgayHuongThamNienMoi { get; set; }
        [DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }
    }
}
