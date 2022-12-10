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


namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{

    [ModelDefault("Caption", "Chi tiết ra đề")]
    public class ChiTietRaDe : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListRaDe")]
        [ModelDefault("Caption", "Quản lý ra đề")]
        [Browsable(false)]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get
            {
                return _QuanLyKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }
        #endregion


        #region KhaiBao
        private NhanVien _NhanVien;
        private BoPhan _BoPhanNhanVien;
        private BoPhan _BoPhan_To;
        private string _TenMonHoc;
        private decimal _SoBoDe;
        private decimal _TienRaDe;
        private decimal _TienDuyetDe;
        private bool _NguoiRaDe;
        private bool _NguoiKiemDuyet;

        
        private string _GhiChu;

        #endregion


        [ModelDefault("Caption","Giám khảo")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhanNhanVien
        {
            get { return _BoPhanNhanVien; }
            set { SetPropertyValue("BoPhanNhanVien", ref _BoPhanNhanVien, value); }
        }

        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Tổ/Bộ môn")]
        public BoPhan BoPhan_To
        {
            get { return _BoPhan_To; }  
            set { SetPropertyValue("BoPhan_To", ref _BoPhan_To, value); }
        }

        [ModelDefault("Caption", "Số bộ đề thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBoDe
        {
            get { return _SoBoDe; }
            set { SetPropertyValue("SoBoDe", ref _SoBoDe, value); }
        }
        [ModelDefault("Caption", "Thành tiền (Ra đề)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienRaDe
        {
            get { return _TienRaDe; }
            set { SetPropertyValue("TienRaDe", ref _TienRaDe, value); }
        }
        [ModelDefault("Caption", "Thành tiền (Duyệt đề)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienDuyetDe
        {
            get { return _TienDuyetDe; }
            set { SetPropertyValue("TienDuyetDe", ref _TienDuyetDe, value); }
        }
        [ModelDefault("Caption", "Người ra đề")]
        public bool NguoiRaDe
        {
            get { return _NguoiRaDe; }
            set { _NguoiRaDe = value; }
        }
        [ModelDefault("Caption", "Người kiểm duyệt")]
        public bool NguoiKiemDuyet
        {
            get { return _NguoiKiemDuyet; }
            set { _NguoiKiemDuyet = value; }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public ChiTietRaDe(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
