using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    /// <summary>
    /// sử dụng trong quyết định bồi dưỡng
    /// </summary>
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenLoaiHinhBoiDuong")]
    [ModelDefault("Caption", "Loại hình bồi dưỡng")]
    public class LoaiHinhBoiDuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHinhBoiDuong;

        public LoaiHinhBoiDuong(Session session) : base(session) { }

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

        [ModelDefault("Caption", "Loại hình bồi dưỡng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHinhBoiDuong
        {
            get
            {
                return _TenLoaiHinhBoiDuong;
            }
            set
            {
                SetPropertyValue("TenLoaiHinhBoiDuong", ref _TenLoaiHinhBoiDuong, value);
            }
        }

    }

}
