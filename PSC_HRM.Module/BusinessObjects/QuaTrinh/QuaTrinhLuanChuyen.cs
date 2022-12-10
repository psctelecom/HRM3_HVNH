using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuaTrinh
{
    [DefaultListViewOptions(true, 0)]
    [ModelDefault("Caption", "Quá Trình Luân Chuyển")]
    [Appearance("QuaTrinhLuanChuyen.TrongTruong", TargetItems = "BoPhan1", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiQuaTrinh=0")]
    [Appearance("QuaTrinhLuanChuyen.NgoaiTruong", TargetItems = "BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "PhanLoaiQuaTrinh=1")]
    public class QuaTrinhLuanChuyen : BaseObject
    {
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private BoPhan _BoPhanCu;
        private QuaTrinhLuanChuyenEnum _PhanLoaiQuaTrinh;
        private string _BoPhan1;
        private ChucVu _ChucVu;
        private decimal _HSPCChucVu;

        public QuaTrinhLuanChuyen(Session session) : base(session) { }
        
        [ModelDefault("Caption", "Số quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    if (string.IsNullOrEmpty(SoQuyetDinh))
                        SoQuyetDinh = value.SoQuyetDinh;
                    if (NgayQuyetDinh != DateTime.MinValue)
                        NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

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

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại đơn vị")]
        public QuaTrinhLuanChuyenEnum PhanLoaiQuaTrinh
        {
            get
            {
                return _PhanLoaiQuaTrinh;
            }
            set
            {
                SetPropertyValue("PhanLoaiQuaTrinh", ref _PhanLoaiQuaTrinh, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);

            }
        }

        [ModelDefault("Caption", "Đơn vị cũ")]
        public BoPhan BoPhanCu
        {
            get
            {
                return _BoPhanCu;
            }
            set
            {
                SetPropertyValue("BoPhanCu", ref _BoPhanCu, value);

            }
        }

        [ModelDefault("Caption", "Đơn vị 1")]
        public string BoPhan1
        {
            get
            {
                return _BoPhan1;
            }
            set
            {
                SetPropertyValue("BoPhan1", ref _BoPhan1, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVuCu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
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

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            PhanLoaiQuaTrinh = QuaTrinhLuanChuyenEnum.ThuyenChuyenNoiBo;
            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }

}
