using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa
{
    [ModelDefault("Caption", "Nhập hướng dẫn từ xa NonPersistent")]
    [NonPersistent]
    public class NhapHuongDan_TuXaNon : BaseObject
    {

        #region Khai báo Master
        private NhanVien _NhanVien;
        private string _TenMonHoc;
        private string _LopMonHoc;
        private BoPhan _BoMonQuanLy;
        #endregion

        #region ChiTiet
        private LoaiHoatDong _LoaiHuongDan;
        private int _SoLuongHuongDan;
        #endregion
        #region Master
        
        [ModelDefault("Caption", "Giảng viên")]
        //[ModelDefault("AllowEdit", "False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string LopMonHoc
        {
            get { return _LopMonHoc; }
            set { SetPropertyValue("LopMonHoc", ref _LopMonHoc, value); }
        }
        [ModelDefault("Caption", "Bộ môn quản lý")]
        //[ModelDefault("AllowEdit", "False")]
        public BoPhan BoMonQuanLy
        {
            get { return _BoMonQuanLy; }
            set { SetPropertyValue("BoMonQuanLy", ref _BoMonQuanLy, value); }
        }
        
        #endregion

        #region chitiet
        
        [ModelDefault("Caption", "Loại hướng dẫn")]
        public LoaiHoatDong LoaiHuongDan
        {
            get { return _LoaiHuongDan; }
            set { SetPropertyValue("LoaiHuongDan", ref _LoaiHuongDan, value); }
        }
        [ModelDefault("Caption", "Số lượng hướng dẫn")]
        public int SoLuongHuongDan
        {
            get { return _SoLuongHuongDan; }
            set { SetPropertyValue("SoLuongHuongDan", ref _SoLuongHuongDan, value); }
        }

        #endregion
        public NhapHuongDan_TuXaNon(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}