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
using PSC_HRM.Module.BusinessObjects.HoSo;


namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số chức danh")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;HocHam;TrinhDoChuyenMon;ChucDanh", "Hệ số đã tồn tại.")]

    [Appearance("Hide_HeSo_VHU", TargetItems = "LoaiGiangVien"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_HeSo_UEL", TargetItems = "LoaiGiangVien;ThamNien;MacDinh"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'UEL'")]

    [Appearance("Hide_HeSo_HUFLIT", TargetItems = "ThamNien"
                                                , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat != 'HUFLIT'")]
    public class HeSoChucDanh : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoChucDanh")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private LoaiGiangVienEnum? _LoaiGiangVien;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChucDanh _ChucDanh;
        private decimal _HeSo_ChucDanh;
        private bool _MacDinh;
        private ThamNien _ThamNien;


        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
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
        [ModelDefault("Caption", "Thâm niên")]
        public ThamNien ThamNien
        {
            get { return _ThamNien; }
            set { SetPropertyValue("ThamNien", ref _ThamNien, value); }
        }
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoChucDanh", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_ChucDanh
        {
            get { return _HeSo_ChucDanh; }
            set { SetPropertyValue("HeSo_ChucDanh", ref _HeSo_ChucDanh, value); }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }

        [NonPersistent]
        public String Caption
        {
            get
            {
                if (TruongConfig.MaTruong == "HUFLIT")
                {
                    return String.Format("Giảng viên {0} {1} {2} {3} {4}", LoaiGiangVien == 0 ? "Cơ hữu" : "Thỉnh giảng", HocHam != null ? " - Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? "- Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? "- Chức danh: " + ChucDanh.TenChucDanh : "", ThamNien != null ? "Thâm niên " + ThamNien.TenThamNien : "");
                }
                else
                    if (TruongConfig.MaTruong != "VHU")
                        return String.Format("Giảng viên {0} {1} {2} {3} ", LoaiGiangVien == 0 ? "Cơ hữu" : "Thỉnh giảng", HocHam != null ? " - Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? "- Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? "- Chức danh: " + ChucDanh.TenChucDanh : "");
                    else
                        return String.Format("{0} {1} {2}", HocHam != null ? "Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? "- Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? "- Chức danh: " + ChucDanh.TenChucDanh : "");
            }
        }

        public HeSoChucDanh(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (TruongConfig.MaTruong != "VHU")
                LoaiGiangVien = LoaiGiangVienEnum.CoHuu;
            
            HocHam = null;
            TrinhDoChuyenMon = null;
            HeSo_ChucDanh = 1;
        }
    }
}