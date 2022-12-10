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


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đơn giá khảo thí")]
    [DefaultProperty("Caption")]
    public class DonGiaKhaoThi : BaseObject
    {
        private LoaiKhaoThi? _LoaiKhaoThi;
        private HinhThucThi _HinhThucThi;
        private decimal _SoLuong;
        private decimal _DonGia;
        private int _UuTien;

        [ModelDefault("Caption", "Loại khảo thí")]
        [VisibleInListView(false)]
        public LoaiKhaoThi? LoaiKhaoThi
        {
            get { return _LoaiKhaoThi; }
            set
            {
                SetPropertyValue("LoaiKhaoThi ", ref _LoaiKhaoThi, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Hình thức thi")]//Học vị
        [VisibleInListView(false)]
        public HinhThucThi HinhThucThi
        {
            get { return _HinhThucThi; }
            set
            {
                SetPropertyValue("HinhThucThi ", ref _HinhThucThi, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("DonGiaKhaoThi_SL", DefaultContexts.Save, 0.00, 1000000000, "Số lượng > 0")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set
            {
                SetPropertyValue("SoLuong ", ref _SoLuong, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DonGiaKhaoThi_DG", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set
            {
                SetPropertyValue("DonGia ", ref _DonGia, value);
            }
        }

        [ModelDefault("Caption", "Ưu tiên")]
        public int UuTien
        {
            get { return _UuTien; }
            set
            {
                SetPropertyValue("UuTien", ref _UuTien, value);
            }
        }

        [NonPersistent]
        public String Caption
        {
            get
            {
                return String.Format(" {0} {1}", LoaiKhaoThi != null ? LoaiKhaoThi.ToString() : "", HinhThucThi != null ? HinhThucThi.TenHinhThucThi : "");
            }
        }
        public DonGiaKhaoThi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
