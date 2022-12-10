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
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Danh sách đơn giá thanh toán vượt giờ")]
    [DefaultProperty("Caption")]
    public class DanhSachDonGiaThanhToan : BaseObject
    {
        private DonGiaThanhToanVuotGio _DonGiaThanhToanVuotGio;

        [Association("DonGiaThanhToanVuotGio-ListDanhSachDonGiaThanhToan")]
        [Browsable(false)]
        public DonGiaThanhToanVuotGio DonGiaThanhToanVuotGio
        {
            get { return _DonGiaThanhToanVuotGio; }
            set { SetPropertyValue("DonGiaThanhToanVuotGio", ref _DonGiaThanhToanVuotGio, value); }
        }

        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private decimal _DonGiaThanhToan;
        private bool _MacDinh;

        [ModelDefault("Caption","Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }


        [ModelDefault("Caption", "Đơn giá thanh toán")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public decimal DonGiaThanhToan
        {
            get { return _DonGiaThanhToan; }
            set { SetPropertyValue("DonGiaThanhToan", ref _DonGiaThanhToan, value); }
        }
        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }
        public DanhSachDonGiaThanhToan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
       
    }
}
