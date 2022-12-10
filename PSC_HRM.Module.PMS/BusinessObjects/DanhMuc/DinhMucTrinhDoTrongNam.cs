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
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Định mức trình độ trong năm")]
    [DefaultProperty("Caption")]
    [Appearance("HUFLIT_Hide", TargetItems = "HeSo_GioTroi;ChucVu;DinhMucNCKH_UngDung;ChucDanh",
                                    Visibility = ViewItemVisibility.Hide,
                                    Criteria = "MaTruong = 'HUFLIT'")]
    public class DinhMucTrinhDoTrongNam : BaseObject
    {
        private string _MaTruong;
        private ChucVu _ChucVu;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChucDanh _ChucDanh;
        private decimal _DinhMucGD;
        private decimal _DinhMucNCKH_UngDung;
        private decimal _DinhMucNCKH;
        private decimal _DinhMucKhac;
        private decimal _HeSo_GioTroi;
        private bool _MacDinh;

        [ModelDefault("Caption", "Chức vụ")]
        [VisibleInListView(false)]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set
            {
                SetPropertyValue("ChucVu ", ref _ChucVu, value);
            }
        }

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

        [ModelDefault("Caption", "ĐM Giảng dạy")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DinhMucGD", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DinhMucGD
        {
            get { return _DinhMucGD; }
            set
            {
                SetPropertyValue("DinhMucGD ", ref _DinhMucGD, value);
            }
        }

        [ModelDefault("Caption", "ĐM NCKH(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DinhMucNCKH_UngDung", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DinhMucNCKH_UngDung
        {
            get { return _DinhMucNCKH_UngDung; }
            set
            {
                SetPropertyValue("DinhMucNCKH_UngDung ", ref _DinhMucNCKH_UngDung, value);
            }
        }

        [ModelDefault("Caption", "ĐM NCKH")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DinhMucNCKH", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DinhMucNCKH
        {
            get { return _DinhMucNCKH; }
            set
            {
                SetPropertyValue("DinhMucNCKH ", ref _DinhMucNCKH, value);
            }
        }

        [ModelDefault("Caption", "ĐM giờ quản lý")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DinhMucKhac", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DinhMucKhac
        {
            get { return _DinhMucKhac; }
            set
            {
                SetPropertyValue("DinhMucKhac ", ref _DinhMucKhac, value);
            }
        }

        [ModelDefault("Caption", "Hệ số giờ trời")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_GioTroi
        {
            get { return _HeSo_GioTroi; }
            set
            {
                SetPropertyValue("HeSo_GioTroi ", ref _HeSo_GioTroi, value);
            }
        }
        [ModelDefault("Caption", "Mã trường")]
        [Browsable(false)]
        public string MaTruong
        {
            get { return _MaTruong; }
            set
            {
                SetPropertyValue("MaTruong ", ref _MaTruong, value);
            }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set
            {
                SetPropertyValue("MacDinh ", ref _MacDinh, value);
            }
        }

        [NonPersistent]
        public String Caption
        {
            get
            {
                return String.Format(" {0}{1}{2} ", HocHam != null ? "Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? " Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? " Chức danh: " + ChucDanh.TenChucDanh : "");
            }
        }
        public DinhMucTrinhDoTrongNam(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;

        }
    }

}
