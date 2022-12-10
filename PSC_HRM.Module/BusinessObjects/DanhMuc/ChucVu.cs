using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("TenChucVu")]
    [ModelDefault("Caption", "Chức vụ")]
    [ImageName("BO_Position")]
    public class ChucVu : TruongBaseObject
    {
        private bool _ChucVuDoan;
        private bool _ChucVuDang;
        private bool _ChucVuDoanThe;

        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public bool ChucVuDoan
        {
            get { return _ChucVuDoan; }
            set { SetPropertyValue("ChucVuDoan", ref _ChucVuDoan, value); }
        }
        [ModelDefault("Caption", "Chức vụ Đảng")]
        public bool ChucVuDang
        {
            get { return _ChucVuDang; }
            set { SetPropertyValue("ChucVuDang", ref _ChucVuDang, value); }
        }
        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        public bool ChucVuDoanThe
        {
            get { return _ChucVuDoanThe; }
            set { SetPropertyValue("ChucVuDoanThe", ref _ChucVuDoanThe, value); }
        }
        #region Khai báo

        private bool _LaQuanLy;
        private string _TenChucVu;
        private string _MaQuanLy;
        private decimal _PhuCapDienThoai;

        #region ChucVu
        private decimal _SoLitXang;
        private decimal _HSPCQuanLy;
        private ChucVuEnum _PhanLoai = ChucVuEnum.TrongTruong;
        private int _ThuTu;
        private decimal _HSPCChucVu;
        private decimal _HSPCTrachNhiem_QNU;//QNU
        private string _GhiChu;
        #endregion
        #endregion

        #region ChiTiet ChucVu
        [ModelDefault("Caption", "Thứ tự")]
        public int ThuTu
        {
            get
            {
                return _ThuTu;
            }
            set
            {
                SetPropertyValue("ThuTu", ref _ThuTu, value);
            }
        }

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

        [ModelDefault("Caption", "Tên chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChucVu
        {
            get
            {
                return _TenChucVu;
            }
            set
            {
                SetPropertyValue("TenChucVu", ref _TenChucVu, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public ChucVuEnum PhanLoai
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

        [ModelDefault("Caption", "HSPC Chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return _HSPCChucVu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu", ref _HSPCChucVu, value);
            }
        }
        // QNU
        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiem_QNU
        {
            get
            {
                return _HSPCTrachNhiem_QNU;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem_QNU", ref _HSPCTrachNhiem_QNU, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "Phụ cấp quản lý")]
        public decimal HSPCQuanLy
        {
            get
            {
                return _HSPCQuanLy;
            }
            set
            {
                SetPropertyValue("HSPCQuanLy", ref _HSPCQuanLy, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return _PhuCapDienThoai;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoai", ref _PhuCapDienThoai, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số lít xăng")]
        public decimal SoLitXang
        {
            get
            {
                return _SoLitXang;
            }
            set
            {
                SetPropertyValue("SoLitXang", ref _SoLitXang, value);
            }
        }

        [ModelDefault("Caption", "Là quản lý")]
        public bool LaQuanLy
        {
            get
            {
                return _LaQuanLy;
            }
            set
            {
                SetPropertyValue("LaQuanLy", ref _LaQuanLy, value);
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
        #endregion



        private decimal _PhuCapChucVu;//Đoàn thể
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        public decimal PhuCapChucVu
        {
            get
            {
                return _PhuCapChucVu;
            }
            set
            {
                SetPropertyValue("PhuCapChucVu", ref _PhuCapChucVu, value);
            }
        }
        public ChucVu(Session session) : base(session) { }
    }

}
