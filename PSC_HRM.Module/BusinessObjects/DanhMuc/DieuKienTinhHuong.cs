using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Điều kiện tính hưởng")]
    [DefaultProperty("TenDieuKienTinhHuong")]
    public class DieuKienTinhHuong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenDieuKienTinhHuong;
        private DieuKienTinhHuongEnum _PhanLoai;


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
        
        [ModelDefault("Caption", "Tên điều kiện tính hưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenDieuKienTinhHuong
        {
            get
            {
                return _TenDieuKienTinhHuong;
            }
            set
            {
                SetPropertyValue("TenDieuKienTinhHuong", ref _TenDieuKienTinhHuong, value);
            }
        }

        [ModelDefault("Caption", "Phân loại"),
        RuleRequiredField("", DefaultContexts.Save)]
        public DieuKienTinhHuongEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        public DieuKienTinhHuong(Session session) : base(session) { }
    }

}
