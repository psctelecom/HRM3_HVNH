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


namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{

    [ModelDefault("Caption", "Vai trò NCKH")]
    [DefaultProperty("TenVaiTro")]
    public class VaiTroNCKH : BaseObject
    {
        #region KhaiBao
        private string _MaQuanLy;
        private string _TenVaiTro;
        private decimal _MucHuong;
        private string _GhiChu;
        private bool _Chia;
        #endregion

        [ModelDefault("Caption","Mã vai trò")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên vai trò")]
        [Size(-1)]
        public string TenVaiTro
        {
            get { return _TenVaiTro; }
            set { SetPropertyValue("TenVaiTro", ref _TenVaiTro, value); }
        }

        [ModelDefault("Caption", "Mức hưởng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal MucHuong
        {
            get { return _MucHuong; }
            set { SetPropertyValue("MucHuong", ref _MucHuong, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Chia")]
        public bool Chia
        {
            get { return _Chia; }
            set { SetPropertyValue("Chia", ref _Chia, value); }
        }
        public VaiTroNCKH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
