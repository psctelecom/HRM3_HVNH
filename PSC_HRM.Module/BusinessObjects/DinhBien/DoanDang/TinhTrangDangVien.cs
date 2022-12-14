using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Metadata;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTinhTrang")]
    [ModelDefault("Caption", "Tình trạng Đảng viên")]
    public class TinhTrangDangVien : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTinhTrang;
        private bool _KhongThuocToChucDang;
        
        public TinhTrangDangVien(Session session) : base(session) { }

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhTrang
        {
            get
            {
                return _TenTinhTrang;
            }
            set
            {
                SetPropertyValue("TenTinhTrang", ref _TenTinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Không còn thuộc Tổ chức Đảng")]
        public bool KhongThuocToChucDang
        {
            get
            {
                return _KhongThuocToChucDang;
            }
            set
            {
                SetPropertyValue("KhongThuocToChucDang", ref _KhongThuocToChucDang, value);
            }
        }
    }

}
