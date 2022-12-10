using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using PSC_HRM.Module.MailMerge.QuyetDinh;
using PSC_HRM.Module.MailMerge.HopDong;

namespace PSC_HRM.Module.MailMerge.QuyetDinh
{
    public class Non_QuyetDinhKiemGiang : Non_QuyetDinh
    {

        [System.ComponentModel.DisplayName("Môn thỉnh giảng")]
        public string MonThinhGiang { get; set; }
        [System.ComponentModel.DisplayName("Môn kiêm giảng")]
        public string MonKiemGiang { get; set; } 
       [System.ComponentModel.DisplayName("Đơn vị công tác")]
        public string DonViCongTac { get; set; }
        [System.ComponentModel.DisplayName("Công việc hiện nay")]
        public string CongViecHienNay { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng viết hoa")]
        public string DanhXungVietHoa { get; set; }
        [System.ComponentModel.DisplayName("Danh xưng viết thường")]
        public string DanhXungVietThuong { get; set; }
        [System.ComponentModel.DisplayName("Chức danh")]
        public string ChucDanh { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị")]
        public string DonVi { get; set; }
        [System.ComponentModel.DisplayName("Đơn vị kiêm giảng")]
        public string DonViKiemGiang { get; set; }
        [System.ComponentModel.DisplayName("Nhân viên")]
        public string NhanVien { get; set; }
        [System.ComponentModel.DisplayName("Loại nhân viên")]
        public string LoaiNhanVien { get; set; }
        [System.ComponentModel.DisplayName("Ngày sinh")]
        public string NgaySinh { get; set; }
        [System.ComponentModel.DisplayName("CMND")]
        public string CMND { get; set; }
        [System.ComponentModel.DisplayName("Cấp ngày")]
        public string CapNgay { get; set; }
        [System.ComponentModel.DisplayName("Nơi cấp")]
        public string NoiCap { get; set; }
        [System.ComponentModel.DisplayName("Quyền cao nhất đơn vị")]
        public string QuyenCaoDonVi { get; set; }
    }

}
