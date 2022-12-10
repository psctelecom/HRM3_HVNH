using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_ChiTietQuyetDinhThanhLapCanBoTrucLeDetail : Non_MergeItem
    {
        [System.ComponentModel.DisplayName("Số thứ tự")]
        public string STT { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [System.ComponentModel.DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Chức danh cán bộ trực lễ")]
        public string ChucDanhCanBoTrucLe { get; set; }
        [System.ComponentModel.DisplayName("Vai trò cán bộ trực lễ")]
        public string VaiTroCanBoTrucLe { get; set; }
    }
}
