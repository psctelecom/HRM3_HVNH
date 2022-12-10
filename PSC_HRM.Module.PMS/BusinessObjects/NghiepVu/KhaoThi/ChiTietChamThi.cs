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

    [ModelDefault("Caption", "Chi tiết chấm thi")]
    public class ChiTietChamThi : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListChamThi")]
        [ModelDefault("Caption", "Quản lý chấm bài - coi thi")]
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
        private decimal _SoBaiCham;
        private decimal _ThanhTien;
        private decimal _Thue;
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

        [ModelDefault("Caption", "Số bài chấm thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiCham
        {
            get { return _SoBaiCham; }
            set { SetPropertyValue("SoBaiCham", ref _SoBaiCham, value); }
        }
        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }
        [ModelDefault("Caption", "Thuế")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal Thue
        {
            get { return _Thue; }
            set { SetPropertyValue("Thue", ref _Thue, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public ChiTietChamThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
