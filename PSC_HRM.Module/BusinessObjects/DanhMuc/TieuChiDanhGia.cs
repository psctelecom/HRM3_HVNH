using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTieuChiDanhGia")]
    [ModelDefault("Caption", "Tiêu chí đánh giá")]
    public class TieuChiDanhGia : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTieuChiDanhGia;
        private TyLeDanhGia _TyLeDanhGia;
        
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên tiêu chí đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTieuChiDanhGia
        {
            get
            {
                return _TenTieuChiDanhGia;
            }
            set
            {
                SetPropertyValue("TenTieuChiDanhGia", ref _TenTieuChiDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Tỷ lệ đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TyLeDanhGia TyLeDanhGia
        {
            get
            {
                return _TyLeDanhGia;
            }
            set
            {
                SetPropertyValue("TyLeDanhGia", ref _TyLeDanhGia, value);
            }
        }

        public TieuChiDanhGia(Session session) : base(session) { }
    }

}
