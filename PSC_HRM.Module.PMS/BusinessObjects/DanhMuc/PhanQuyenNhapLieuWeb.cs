using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.Enum;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Phân quyền nhập liệu Web")]
    [DefaultProperty("Caption")]
    public class PhanQuyenNhapLieuWeb : BaseObject 
    {
        private NhanVien _NhanVien;
        private LoaiKhaoThi? _LoaiKhaoThi;
        private string _GhiChu;
        private bool _NgungSuDung;

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Loại khảo thí")]
        public LoaiKhaoThi? LoaiKhaoThi
        {
            get
            {
                return _LoaiKhaoThi;
            }
            set
            {
                SetPropertyValue("LoaiKhaoThi", ref _LoaiKhaoThi, value);
            }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        [ModelDefault("Caption", "Ngưng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }
        public PhanQuyenNhapLieuWeb(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgungSuDung = false;
        }
    }
}
