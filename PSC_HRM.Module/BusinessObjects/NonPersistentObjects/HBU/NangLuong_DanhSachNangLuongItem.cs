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
    [ModelDefault("Caption", "Chi tiết danh sách nâng lương")]
    public class NangLuong_DanhSachNangLuongItem : BaseObject, ISupportController
    {

        private string _SoQuyetDinh;
		private DateTime _NgayQuyetDinh;
        private DateTime _NgayHieuLuc;
		private string _NguoiKy1;
		private string _NhanVienText;
		private string _BoPhanText;
        private DateTime _NgayHuongLuongCu;
		private Decimal _ThuongHieuQuaTheoThangCu;
        private Decimal _MucLuongCu;
        private DateTime _NgayHuongLuongMoi;
        private Decimal _ThuongHieuQuaTheoThangMoi;
        private Decimal _MucLuongMoi;

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

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Thưởng hiệu quả theo tháng cũ")]
        public decimal ThuongHieuQuaTheoThangCu
        {
            get
            {
                return _ThuongHieuQuaTheoThangCu;
            }
            set
            {
                SetPropertyValue("ThuongHieuQuaTheoThangCu", ref _ThuongHieuQuaTheoThangCu, value);
            }
        }
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Mức lương cũ")]
        public decimal MucLuongCu
        {
            get
            {
                return _MucLuongCu;
            }
            set
            {
                SetPropertyValue("MucLuongCu", ref _MucLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Thưởng hiệu quả theo tháng mới")]
        public decimal ThuongHieuQuaTheoThangMoi
        {
            get
            {
                return _ThuongHieuQuaTheoThangMoi;
            }
            set
            {
                SetPropertyValue("ThuongHieuQuaTheoThangMoi", ref _ThuongHieuQuaTheoThangMoi, value);
            }
        }
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("Caption", "Mức lương mới")]
        public decimal MucLuongMoi
        {
            get
            {
                return _MucLuongMoi;
            }
            set
            {
                SetPropertyValue("MucLuongMoi", ref _MucLuongMoi, value);
            }
        }

        public NangLuong_DanhSachNangLuongItem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
