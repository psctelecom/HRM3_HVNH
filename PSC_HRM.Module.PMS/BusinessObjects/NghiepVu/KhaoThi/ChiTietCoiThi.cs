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

    [ModelDefault("Caption", "Chi tiết coi thi")]
    public class ChiTietCoiThi : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListCoiThi")]
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
        private decimal _SoCaCoiThi;
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

        [ModelDefault("Caption", "Tổ/Bộ môn")]
        public BoPhan BoPhan_To
        {
            get { return _BoPhan_To; }  
            set { SetPropertyValue("BoPhan_To", ref _BoPhan_To, value); }
        }

        [ModelDefault("Caption", "Số ca coi thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoCaCoiThi
        {
            get { return _SoCaCoiThi; }
            set { SetPropertyValue("SoCaCoiThi", ref _SoCaCoiThi, value); }
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
        public ChiTietCoiThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
