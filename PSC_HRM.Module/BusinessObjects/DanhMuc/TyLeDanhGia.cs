using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTyLeDanhGia")]
    [ModelDefault("Caption", "Tỷ lệ đánh giá")]
    public class TyLeDanhGia : BaseObject
    {
        private string _TenTyLeDanhGia;
        private int _CapDoCaoNhat;
        private DonViTinh _DonViTinh;

        [ModelDefault("Caption", "Tên tỷ lệ đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTyLeDanhGia
        {
            get
            {
                return _TenTyLeDanhGia;
            }
            set
            {
                SetPropertyValue("TenTyLeDanhGia", ref _TenTyLeDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Cấp độ cao nhất")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int CapDoCaoNhat
        {
            get
            {
                return _CapDoCaoNhat;
            }
            set
            {
                SetPropertyValue("CapDoCaoNhat", ref _CapDoCaoNhat, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tính")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DonViTinh DonViTinh
        {
            get
            {
                return _DonViTinh;
            }
            set
            {
                SetPropertyValue("DonViTinh", ref _DonViTinh, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết tỷ lệ đánh giá")]
        [Association("TyLeDanhGia-ListChiTietTyLeDanhGia")]
        public XPCollection<ChiTietTyLeDanhGia> ListChiTietTyLeDanhGia
        {
            get
            {
                return GetCollection<ChiTietTyLeDanhGia>("ListChiTietTyLeDanhGia");
            }
        }

        public TyLeDanhGia(Session session) : base(session) { }
    }

}
