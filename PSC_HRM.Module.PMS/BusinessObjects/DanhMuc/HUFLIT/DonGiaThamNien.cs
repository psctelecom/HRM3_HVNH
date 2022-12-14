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
using PSC_HRM.Module.BusinessObjects.HoSo;

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Đơn giá vượt giờ thâm niên")]
    [DefaultProperty("Caption")]
    public class DonGiaThamNien : BaseObject
    {
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChucDanh _ChucDanh;
        private ThamNien _ThamNien;
        private decimal _DonGia;

        [ModelDefault("Caption", "Học hàm")]
        [VisibleInListView(false)]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set
            {
                SetPropertyValue("HocHam ", ref _HocHam, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]//Học vị
        [VisibleInListView(false)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon ", ref _TrinhDoChuyenMon, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Chức danh")]//Học vị
        [VisibleInListView(false)]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set
            {
                SetPropertyValue("ChucDanh ", ref _ChucDanh, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Thâm niên")]//Học vị
        public ThamNien ThamNien
        {
            get { return _ThamNien; }
            set
            {
                SetPropertyValue("ThamNien ", ref _ThamNien, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DonGiaThamNien", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set
            {
                SetPropertyValue("DonGia ", ref _DonGia, value);
            }
        }

        [NonPersistent]
        public String Caption
        {
            get
            {
                return String.Format(" {0} {1} {2} ",  HocHam != null ? " Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? "- Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? "- Chức danh: " + ChucDanh.TenChucDanh : "");
            }
        }
        public DonGiaThamNien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
