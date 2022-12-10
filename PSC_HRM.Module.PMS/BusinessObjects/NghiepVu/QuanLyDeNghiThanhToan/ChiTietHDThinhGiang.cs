using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;



namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Chi tiết thanh lý - hợp đồng(thỉnh giảng)")]
    [DefaultProperty("TenMonHoc")]
    public class ChiTietHDThinhGiang : BaseObject
    {
        private HopDong_ThanhLy_ThinhGiang _HopDong_ThanhLy_ThinhGiang;

        [ModelDefault("Caption", "Quản lý")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("HopDong_ThanhLy_ThinhGiang-ListChiTietHDThinhGiang")]
        public HopDong_ThanhLy_ThinhGiang HopDong_ThanhLy_ThinhGiang
        {
            get
            {
                return _HopDong_ThanhLy_ThinhGiang;
            }
            set
            {
                SetPropertyValue("HopDong_ThanhLy_ThinhGiang", ref _HopDong_ThanhLy_ThinhGiang, value);
            }
        }

        private string _LopHocPhan;
        private string _TenMonHoc;
        private string _LopSinhVien;
        private string _TenLopSinhVien;
        private decimal _SoTietLyThuyet;
        private decimal _GioQuyDoiLyThuyet;
        private decimal _SoLuongSV;


        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp sinh viên")]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set { SetPropertyValue("LopSinhVien", ref _LopSinhVien, value); }
        }
        [ModelDefault("Caption", "Tên lớp sinh viên")]
        [Size(-1)]
        public string TenLopSinhVien
        {
            get { return _TenLopSinhVien; }
            set { SetPropertyValue("TenLopSinhVien", ref _TenLopSinhVien, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Số tiết lý thuyết")]
        public decimal SoTietLyThuyet
        {
            get { return _SoTietLyThuyet; }
            set { SetPropertyValue("SoTietLyThuyet", ref _SoTietLyThuyet, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Giời quy đổi lý thuyết")]
        public decimal GioQuyDoiLyThuyet
        {
            get { return _GioQuyDoiLyThuyet; }
            set { SetPropertyValue("GioQuyDoiLyThuyet", ref _GioQuyDoiLyThuyet, value); }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Sỉ số")]
        public decimal SoLuongSV
        {
            get { return _SoLuongSV; }
            set { SetPropertyValue("SoLuongSV", ref _SoLuongSV, value); }
        }
        
        public ChiTietHDThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }       
    }
}
