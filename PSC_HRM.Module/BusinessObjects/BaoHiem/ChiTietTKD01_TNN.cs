using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Chi tiết TKD01_TNN")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "TKD01_TNN")]
    public class ChiTietTKD01_TNN : BaoMatBaseObject
    {
        private TKD01_TNN _TKD01_TNN;
        private string _IDLaoDong;
        private string _HoTen;
        private string _MaSoBHXH;
        private string _TenLoaiVanBan;
        private string _SoVanBan;
        private string _NgayBanHanh;
        private string _NgayHieuLuc;
        private string _CoQuanBanHanh;
        private string _TrichYeu;
        private string _NoiDungThamDinh;


        [ImmediatePostData]
        [ModelDefault("Caption", "TKD01_TNN")]
        [Association("TKD01_TNN-ListChiTietTKD01_TNN")]
        public TKD01_TNN TKD01_TNN
        {
            get
            {
                return _TKD01_TNN;
            }
            set
            {
                SetPropertyValue("TKD01_TNN", ref _TKD01_TNN, value);
            }
        }


        [ModelDefault("Caption", "ID lao động")]
        public string IDLaoDong
        {
            get
            {
                return _IDLaoDong;
            }
            set
            {
                SetPropertyValue("IDLaoDong", ref _IDLaoDong, value);
            }
        }

        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Mã số BHXH")]
        public string MaSoBHXH
        {
            get
            {
                return _MaSoBHXH;
            }
            set
            {
                SetPropertyValue("MaSoBHXH", ref _MaSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Tên văn bản")]
        public string TenLoaiVanBan
        {
            get
            {
                return _TenLoaiVanBan;
            }
            set
            {
                SetPropertyValue("TenLoaiVanBan", ref _TenLoaiVanBan, value);
            }
        }

        [ModelDefault("Caption", "Số văn bản")]
        public string SoVanBan
        {
            get
            {
                return _SoVanBan;
            }
            set
            {
                SetPropertyValue("SoVanBan", ref _SoVanBan, value);
            }
        }

        [ModelDefault("Caption", "Ngày ban hành")]
        public string NgayBanHanh
        {
            get
            {
                return _NgayBanHanh;
            }
            set
            {
                SetPropertyValue("NgayBanHanh", ref _NgayBanHanh, value);
            }
        }

        [ModelDefault("Caption", "Ngày hiệu lực")]
        public string NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }

        [ModelDefault("Caption", "Cơ quan ban hành")]
        public string CoQuanBanHanh
        {
            get
            {
                return _CoQuanBanHanh;
            }
            set
            {
                SetPropertyValue("CoQuanBanHanh", ref _CoQuanBanHanh, value);
            }
        }

        [ModelDefault("Caption", "Trích yếu văn bản")]
        public string TrichYeu
        {
            get
            {
                return _TrichYeu;
            }
            set
            {
                SetPropertyValue("TrichYeu", ref _TrichYeu, value);
            }
        }

        [ModelDefault("Caption", "Trích lược nội dung thẩm định")]
        public string NoiDungThamDinh
        {
            get
            {
                return _NoiDungThamDinh;
            }
            set
            {
                SetPropertyValue("NoiDungThamDinh", ref _NoiDungThamDinh, value);
            }
        }


        public ChiTietTKD01_TNN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

    }

}
