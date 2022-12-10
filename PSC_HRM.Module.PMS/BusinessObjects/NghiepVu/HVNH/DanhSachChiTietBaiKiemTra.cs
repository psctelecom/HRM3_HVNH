using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.HVNH
{

    [ModelDefault("Caption", "Chi tiết bài kiểm tra")]
    public class DanhSachChiTietBaiKiemTra : BaseObject
    {
        #region key
        private QuanLyBaiKiemTra _QuanLyBaiKiemTra;
        [Association("QuanLyBaiKiemTra-ListDanhSachBaiKtra")]
        [ModelDefault("Caption", "Quản lý bài kiểm tra")]
        [Browsable(false)]
        public QuanLyBaiKiemTra QuanLyBaiKiemTra
        {
            get
            {
                return _QuanLyBaiKiemTra;
            }
            set
            {
                SetPropertyValue("QuanLyBaiKiemTra", ref _QuanLyBaiKiemTra, value);
            }
        }
        #endregion

        #region KhaiBao
        //private NhanVien _NhanVien;
        //private string _LopHocPhan;
        //private string _LopSinhVien;
        private string _MaHP;
        private string _TenHP;
        private decimal _SoBaiKiemTra;
        private string _GhiChu;

        #endregion

        //[ModelDefault("Caption","Giám khảo")]
        //public NhanVien NhanVien
        //{
        //    get { return _NhanVien; }
        //    set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        //}

        //[ModelDefault("Caption", "Lớp học phần")]
        //public string LopHocPhan
        //{
        //    get { return _LopHocPhan; }
        //    set { SetPropertyValue("_LopHocPhan", ref _LopHocPhan, value); }
        //}

        //[ModelDefault("Caption", "Lớp sinh viên")]
        //public string LopSinhVien
        //{
        //    get { return _LopSinhVien; }  
        //    set { SetPropertyValue("LopSinhVien", ref _LopSinhVien, value); }
        //}

        [ModelDefault("Caption", "Mã học phần")]
        public string MaHP
        {
            get { return _MaHP; }
            set { SetPropertyValue("MaHP", ref _MaHP, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        public string TenHP
        {
            get { return _TenHP; }
            set { SetPropertyValue("TenHP", ref _TenHP, value); }
        }

        [ModelDefault("Caption", "Số bài kiểm tra")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiKiemTra
        {
            get { return _SoBaiKiemTra; }
            set { SetPropertyValue("SoBaiKiemTra", ref _SoBaiKiemTra, value); }
        }
      
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DanhSachChiTietBaiKiemTra(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
