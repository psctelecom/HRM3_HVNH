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
    public class ThongTinChamBai : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListThongTinChamBai")]
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
        private decimal _SoGioQuyDoi;
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

        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioQuyDoi
        {
            get { return _SoGioQuyDoi; }
            set { SetPropertyValue("SoGioQuyDoi", ref _SoGioQuyDoi, value); }
        }
     
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [Aggregated]
        [Association("ThongTinChamBai-ListChiTietChamBaiCoiThi")]
        [ModelDefault("Caption", "Chi tiết Coi thi/Chấm thi")]
        public XPCollection<ChiTietChamBaiCoiThi_HVNH> ListChiTietChiTietChamBaiCoiThi_HVNH
        {
            get
            {
                return GetCollection<ChiTietChamBaiCoiThi_HVNH>("ListChiTietChiTietChamBaiCoiThi_HVNH");
            }
        }
        public ThongTinChamBai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
