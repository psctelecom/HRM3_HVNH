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
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Danh mục hoạt động khác")]
    //[Appearance("Hide_VHU", TargetItems = "SoLuongThanhTien;SoTienThanhToan"
    //                                      , Visibility = ViewItemVisibility.Hide, Criteria = "TruongConfig.MaTruong = 'VHU'")]
    [DefaultProperty("TenQuyDoi")]
    public class DanhMucHoatDongKhac : BaseObject
    {
        private NhomHoatDongKhac _NhomHDK;
        private NamHoc _NamHoc;
        private DonViTinh _DonViTinh;
        private int _SoThuTu;
        private string _MaQuyDoi;
        private string _MaQuanLy;
        private string _TenQuyDoi;
        private decimal _SoLuong;
        private decimal _HeSo;
        private decimal _SoLuongThanhTien;
        private decimal _SoTienThanhToan;
        private string _GhiChu;
        private bool _ChonMonHoc;

        [ModelDefault("Caption", "Nhóm hoạt động")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public NhomHoatDongKhac NhomHDK
        {
            get { return _NhomHDK; }
            set { SetPropertyValue("NhomHDK", ref _NhomHDK, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        //[VisibleInListView(false)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Đơn vị tính")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public DonViTinh DonViTinh
        {
            get { return _DonViTinh; }
            set { SetPropertyValue("DonViTinh", ref _DonViTinh, value); }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        //[VisibleInListView(false)]
        public int SoThuTu
        {
            get { return _SoThuTu; }
            set { SetPropertyValue("SoThuTu", ref _SoThuTu, value); }
        }

        [ModelDefault("Caption", "Mã quy đổi")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public string MaQuyDoi
        {
            get { return _MaQuyDoi; }
            set { SetPropertyValue("MaQuyDoi", ref _MaQuyDoi, value); }
        }

        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên quy đổi")]
        [Size(-1)]
        public string TenQuyDoi
        {
            get { return _TenQuyDoi; }
            set { SetPropertyValue("TenQuyDoi", ref _TenQuyDoi, value); }
        }

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "false")]
        //[VisibleInDetailView(false)]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "false")]
        //[VisibleInDetailView(false)]
        public decimal HeSo
        {
            get { return _HeSo; }
            set { SetPropertyValue("HeSo", ref _HeSo, value); }
        }

        [ModelDefault("Caption", "Số lượng thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[ModelDefault("AllowEdit", "false")]
        //[VisibleInDetailView(false)]
        public decimal SoLuongThanhTien
        {
            get { return _SoLuongThanhTien; }
            set { SetPropertyValue("SoLuongThanhTien", ref _SoLuongThanhTien, value); }
        }

        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "false")]
        //[VisibleInDetailView(false)]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Ghi Chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Chọn môn học")]
        public bool ChonMonHoc
        {
            get { return _ChonMonHoc; }
            set { SetPropertyValue("ChonMonHoc", ref _ChonMonHoc, value); }
        }

        public DanhMucHoatDongKhac(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();           
        }
    }

}
