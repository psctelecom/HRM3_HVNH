using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [DefaultProperty("LoaiHinhThucHanh")]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số khối lượng thỉnh giảng(TH)")]
    public class HeSoKhoiLuongThinhGiang : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private string _LoaiHinhThucHanh;
        private decimal _HeSo;
        private decimal _SoBuoi;
        private decimal _SoNhom;
        private DonViTinh _DonViTinh;
        private string _GhiChu;


        //[ModelDefault("Caption", "Quản lý hệ số")]
        //[Browsable(false)]
        //[RuleRequiredField("", DefaultContexts.Save)]
        //[Association("QuanLyHeSo-ListHeSoKhoiLuongThinhGiang")]
        //public QuanLyHeSo QuanLyHeSo
        //{
        //    get
        //    {
        //        return _QuanLyHeSo;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
        //    }
        //}

        [ModelDefault("Caption", "Loại hình thực hành")]
        [Size(-1)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string LoaiHinhThucHanh
        {
            get
            {
                return _LoaiHinhThucHanh;
            }
            set
            {
                SetPropertyValue("LoaiHinhThucHanh", ref _LoaiHinhThucHanh, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal HeSo
        {
            get
            {
                return _HeSo;
            }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [ModelDefault("Caption", "Số buổi")]
        public decimal SoBuoi
        {
            get
            {
                return _SoBuoi;
            }
            set
            {
                SetPropertyValue("SoBuoi", ref _SoBuoi, value);
            }
        }

        [ModelDefault("Caption", "Số nhóm")]
        public decimal SoNhom
        {
            get
            {
                return _SoNhom;
            }
            set
            {
                SetPropertyValue("SoNhom", ref _SoNhom, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị tính")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
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

        public HeSoKhoiLuongThinhGiang(Session session) : base(session) { }
    }

}
