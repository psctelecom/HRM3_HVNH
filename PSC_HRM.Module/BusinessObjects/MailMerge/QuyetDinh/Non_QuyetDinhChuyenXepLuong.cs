using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChuyenXepLuong : Non_QuyetDinhNhanVien
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

        public Non_QuyetDinhChuyenXepLuong()
        {
            Master = new List<Non_ChiTietQuyetDinhChuyenXepLuongMaster>();
            Detail = new List<Non_ChiTietQuyetDinhChuyenXepLuongDetail>();
        }
    }
}
