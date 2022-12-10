using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenHoatDong")]
    [ModelDefault("Caption", "Hoạt động quản lý")]
    public class HoatDongQuanLy : BaseObject
    {
        private string _MaQuanLy;
        private string _TenHoatDong;
        private DonViTinh _DonViTinh;
        private decimal _SoLuong;
        private decimal _GioQuyDoi;
        private string _GhiChu;
        private bool _HienWeb;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên hoạt động")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenHoatDong
        {
            get { return _TenHoatDong; }
            set { SetPropertyValue("TenHoatDong", ref _TenHoatDong, value); }
        }

        [ModelDefault("Caption", "Đơn vị tính")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DonViTinh DonViTinh
        {
            get { return _DonViTinh; }
            set { SetPropertyValue("DonViTinh", ref _DonViTinh, value); }
        }

        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]       
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }

        [ModelDefault("Caption", "Giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Hiện web")]
        public bool HienWeb
        {
            get { return _HienWeb; }
            set { SetPropertyValue("HienWeb", ref _HienWeb, value); }
        }
        public HoatDongQuanLy(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            SoLuong = 1;
            GioQuyDoi = 1;
        }
    }
}