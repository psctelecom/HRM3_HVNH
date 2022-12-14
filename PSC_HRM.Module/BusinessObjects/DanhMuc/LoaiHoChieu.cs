using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLoaiHoChieu")]
    [ModelDefault("Caption", "Loại hộ chiếu")]
    [RuleCombinationOfPropertiesIsUnique("LoaiHoChieu.Identifier", DefaultContexts.Save, "MaQuanLy;TenLoaiHoChieu")]
    public class LoaiHoChieu : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHoChieu;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên loại hộ chiếu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHoChieu
        {
            get
            {
                return _TenLoaiHoChieu;
            }
            set
            {
                SetPropertyValue("TenLoaiHoChieu", ref _TenLoaiHoChieu, value);
            }
        }

        public LoaiHoChieu(Session session) : base(session) { }
    }

}
