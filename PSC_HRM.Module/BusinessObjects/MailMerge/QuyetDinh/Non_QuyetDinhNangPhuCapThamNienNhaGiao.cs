using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhNangPhuCapThamNienNhaGiao : Non_QuyetDinh
    {
        [DisplayName("Số giảng viên được công nhận")]
        public string SoGiangVienDuocCongNhan { get; set; }
        [DisplayName("Số giảng viên được nâng thâm niên")]
        public string SoGiangVienDuocNangThamNien { get; set; }
        [DisplayName("Quyết định nâng thâm niên năm trước")]
        public string QuyetDinhNangThamNienNamTruoc { get; set; }
        [System.ComponentModel.DisplayName("Từ tháng")]
        public string TuThang { get; set; }
        [System.ComponentModel.DisplayName("Đến tháng")]
        public string DenThang { get; set; }

        //
        [DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [DisplayName("Danh xưng viết thường")]
        public string DanhXungVietThuong { get; set; }
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }
        [DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên mới")]
        public string NgayHuongThamNienMoi { get; set; }
        [System.ComponentModel.DisplayName("Tên ngạch")]
        public string TenNgach { get; set; }
        [System.ComponentModel.DisplayName("Mã ngạch")]
        public string MaNgach { get; set; }
        [System.ComponentModel.DisplayName("Thâm niên cũ")]
        public string ThamNienCu { get; set; }
        [System.ComponentModel.DisplayName("Ngày hưởng thâm niên cũ")]
        public string NgayHuongThamNienCu { get; set; }
           [System.ComponentModel.DisplayName("Ngày Ký")]
        public string NgayKy { get; set; }
        public Non_QuyetDinhNangPhuCapThamNienNhaGiao()
        {
            Master = new List<Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMaster>();
            Detail = new List<Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoDetail>();
            Master1 = new List<Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiMaster>();
            Detail1 = new List<Non_ChiTietQuyetDinhNangPhuCapThamNienNhaGiaoMoiDetail>();
        }
    }
}
