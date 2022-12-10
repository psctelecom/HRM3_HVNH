using System;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangLuongCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Loại nâng lương")]
        public string LoaiNangLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngày họp hội đồng lương (Date)")]
        public string NgayHopHoiDongLuongDate { get; set; }
        [System.ComponentModel.DisplayName("Nhóm ngạch")]
        public string NhomNgach { get; set; }
        [System.ComponentModel.DisplayName("Tên ngạch lương")]
        public string TenNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch lương")]
        public string MaNgachLuong { get; set; }
        [System.ComponentModel.DisplayName("Ngạch lương")]
        public string NgachLuong { get; set; }

        [System.ComponentModel.DisplayName("Mã bậc lương cũ")]
        public string MaBacLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương cũ")]
        public string BacLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương cũ")]
        public string HeSoLuongCu { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung cũ")]
        public string VuotKhungCu { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương cũ")]
        public string MocNangLuongCu { get; set; }

        [System.ComponentModel.DisplayName("Mã Bậc lương mới")]
        public string MaBacLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Bậc lương mới")]
        public string BacLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Hệ số lương mới")]
        public string HeSoLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Vượt khung mới")]
        public string VuotKhungMoi { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương mới")]
        public string MocNangLuongMoi { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng lương")]
        public string NgayHuongLuong { get; set; }
        //        
        [System.ComponentModel.DisplayName("Ngày hưởng lương (Date)")]
        public string NgayHuongLuongDate { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương cũ (Date)")]
        public string MocNangLuongCuDate { get; set; }
        [System.ComponentModel.DisplayName("Mốc nâng lương mới (Date)")]
        public string MocNangLuongMoiDate { get; set; }
        //
        [System.ComponentModel.DisplayName("Lý do")]
        public string LyDo { get; set; }
        [System.ComponentModel.DisplayName("Số tháng nâng trước hạn")]
        public string SoThang { get; set; }


        ////HBU
        //[System.ComponentModel.DisplayName("Thưởng hiệu quả theo tháng cũ")]
        //public string ThuongHieuQuaTheoThangCu { get; set; }
        //[System.ComponentModel.DisplayName("Thưởng hiệu quả theo tháng mới")]
        //public string ThuongHieuQuaTheoThangMoi { get; set; }
        //[System.ComponentModel.DisplayName("Mức lương cũ")]
        //public string MucLuongCu { get; set; }
        //[System.ComponentModel.DisplayName("Mức lương mới")]
        //public string MucLuongMoi { get; set; }
        //[System.ComponentModel.DisplayName("Thu nhập hiện tại")]
        //public string ThuNhapHienTai { get; set; }
        //[System.ComponentModel.DisplayName("Thu nhập mới")]
        //public string ThuNhapMoi { get; set; }
    }
}
