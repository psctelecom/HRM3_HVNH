using System;
using System.Collections.Generic;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhChoPhepNghiHocTamThoiCaNhan : Non_QuyetDinhNhanVien
    {
        [System.ComponentModel.DisplayName("Hình thức đào tạo QDDT")]
        public string HinhThucDaoTaoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Quốc gia QDDT")]
        public string QuocGiaQDDT { get; set; }
        [System.ComponentModel.DisplayName("Trường đào tạo QDDT")]
        public string TruongDaoTaoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Ngành đào tạo QDDT")]
        public string NganhDaoTaoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Chuyên ngành đào tạo QDDT")]
        public string ChuyenNganhDaoTaoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Trình độ đào tạo QDDT")]
        public string TrinhDoChuyenMonQDDT { get; set; }
        [System.ComponentModel.DisplayName("Nguồn kinh phí QDDT")]
        public string NguonKinhPhiQDDT { get; set; }
        [System.ComponentModel.DisplayName("Trường hổ trợ QDDT")]
        public string TruongHoTroQDDT { get; set; }
        [System.ComponentModel.DisplayName("Khóa đào tạo QDDT")]
        public string KhoaDaoTaoQDDT { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày QDDT (Date)")]
        public string TuNgayQDDTDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày QDDT (Date)")]
        public string DenNgayQDDTDate { get; set; }
        [System.ComponentModel.DisplayName("Số quyết định QDDT")]
        public string SoQuyetDinhQDDT { get; set; }
        [System.ComponentModel.DisplayName("Ngày quyết định QDDT (Date)")]
        public string NgayQuyetDinhQDDTDate { get; set; }
        [System.ComponentModel.DisplayName("Cơ quan quyết định QDDT")]
        public string CoQuanQuyetDinhQDDT { get; set; }
        [System.ComponentModel.DisplayName("Loại QDDT")]
        public string LoaiQDDT { get; set; }

        [System.ComponentModel.DisplayName("Từ ngày")]
        public string TuNgay { get; set; }
        [System.ComponentModel.DisplayName("Từ ngày (date)")]
        public string TuNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày")]
        public string DenNgay { get; set; }
        [System.ComponentModel.DisplayName("Đến ngày (date)")]
        public string DenNgayDate { get; set; }
        [System.ComponentModel.DisplayName("Lý do nghỉ học")]
        public string LyDoNghiHoc { get; set; }
                
    }
}
