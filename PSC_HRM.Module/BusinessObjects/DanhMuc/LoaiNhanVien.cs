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
    [DefaultProperty("TenLoaiNhanVien")]
    [ModelDefault("Caption", "Loại hợp đồng")]
    public class LoaiNhanVien : TruongBaseObject
    {
        public LoaiNhanVien(Session session) : base(session) { }

        private string _MaQuanLy;
        private string _TenLoaiNhanVien;
        private bool _TinhThuLao;

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

        [ModelDefault("Caption", "Tên loại hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiNhanVien
        {
            get
            {
                return _TenLoaiNhanVien;
            }
            set
            {
                SetPropertyValue("TenLoaiNhanVien", ref _TenLoaiNhanVien, value);
            }
   
        }
        [ModelDefault("Caption", "Tính thù lao")]
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set { SetPropertyValue("TinhThuLao", ref _TinhThuLao, value); }
        }
        private decimal _CapDo;
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }
    }

}
