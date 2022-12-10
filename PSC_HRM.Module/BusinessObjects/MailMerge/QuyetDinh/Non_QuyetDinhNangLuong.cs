using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangLuong : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Ngày họp hội đồng lương (Date)")]
        public string NgayHopHoiDongLuongDate { get; set; }

        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương thường xuyên")]
        public string SoLuongNangThuongXuyen { get; set; }

        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương trước nghỉ hưu")]
        public string SoLuongNangTruocNghiHuu { get; set; }

        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng lương trước hạn")]
        public string SoLuongNangTruocHan { get; set; }

        [System.ComponentModel.DisplayName("Số lượng cán bộ nâng vượt khung")]
        public string SoLuongNangVuotKhung { get; set; }

        public Non_QuyetDinhNangLuong()
        {
            Master = new List<Non_ChiTietQuyetDinhNangLuongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhNangLuongDetail>();
        }
    }
}
