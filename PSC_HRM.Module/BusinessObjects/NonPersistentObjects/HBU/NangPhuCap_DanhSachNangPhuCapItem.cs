using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết danh sách nâng phụ cấp")]
    public class NangPhuCap_DanhSachNangPhuCapItem : BaseObject, ISupportController
    {

        private string _SoQuyetDinh;
		private DateTime _NgayQuyetDinh;
        private DateTime _NgayHieuLuc;
		private string _NguoiKy1;
		private string _NhanVienText;
		private string _BoPhanText;
        private DateTime _NgayHuongPhuCapCu;
        private Decimal _PhuCapTienXangCu;
        private Decimal _PhuCapDienThoaiCu;
        private Decimal _PhuCapTrachNhiemCongViecCu;
        private DateTime _NgayHuongPhuCapMoi;
        private Decimal _PhuCapTienXangMoi;
        private Decimal _PhuCapDienThoaiMoi;
        private Decimal _PhuCapTrachNhiemCongViecMoi;

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày hiệu lực")]
        public DateTime NgayHieuLuc
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

        [ModelDefault("Caption", "Người ký")]
        public string NguoiKy1
        {
            get
            {
                return _NguoiKy1;
            }
            set
            {
                SetPropertyValue("NguoiKy1", ref _NguoiKy1, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public string NhanVienText
        {
            get
            {
                return _NhanVienText;
            }
            set
            {
                SetPropertyValue("NhanVienText", ref _NhanVienText, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng phụ cấp cũ")]
        public DateTime NgayHuongPhuCapCu
        {
            get
            {
                return _NgayHuongPhuCapCu;
            }
            set
            {
                SetPropertyValue("NgayHuongPhuCapCu", ref _NgayHuongPhuCapCu, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp tiền xăng cũ")]
        public decimal PhuCapTienXangCu
        {
            get
            {
                return _PhuCapTienXangCu;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangCu", ref _PhuCapTienXangCu, value);
            }
        }


        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp điện thoại cũ")]
        public decimal PhuCapDienThoaiCu
        {
            get
            {
                return _PhuCapDienThoaiCu;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiCu", ref _PhuCapDienThoaiCu, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp trách nhiệm cũ")]
        public decimal PhuCapTrachNhiemCongViecCu
        {
            get
            {
                return _PhuCapTrachNhiemCongViecCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViecCu", ref _PhuCapTrachNhiemCongViecCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng phụ cấp mới")]
        public DateTime NgayHuongPhuCapMoi
        {
            get
            {
                return _NgayHuongPhuCapMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongPhuCapMoi", ref _NgayHuongPhuCapMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp tiền xăng mới")]
        public decimal PhuCapTienXangMoi
        {
            get
            {
                return _PhuCapTienXangMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangMoi", ref _PhuCapTienXangMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp điện thoại mới")]
        public decimal PhuCapDienThoaiMoi
        {
            get
            {
                return _PhuCapDienThoaiMoi;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiMoi", ref _PhuCapDienThoaiMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Phụ cấp trách nhiệm mới")]
        public decimal PhuCapTrachNhiemCongViecMoi
        {
            get
            {
                return _PhuCapTrachNhiemCongViecMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViecMoi", ref _PhuCapTrachNhiemCongViecMoi, value);
            }
        }

        public NangPhuCap_DanhSachNangPhuCapItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
