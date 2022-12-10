using System;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình bổ nhiệm chức vụ")]
    [RuleCombinationOfPropertiesIsUnique("QuaTrinhBoNhiem.Identifier", DefaultContexts.Save, "ThongTinNhanVien;QuyetDinh")]
    public class QuaTrinhBoNhiem : BaseObject
    {
        private int _STT;
        private string _TuNam;
        private string _DenNam;
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private DateTime _NgayHetQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private decimal _HeSoPhuCapChucVu;
        private DateTime _NgayHuongHeSo;
        private decimal _TienTroCapChucVu;
        private DateTime _NgayHuongTienTroCapChucVu;



        [ModelDefault("Caption", "Ngày hưởng tiền trợ cấp chức vụ")]
        public DateTime NgayHuongTienTroCapChucVu
        {
            get
            {
                return _NgayHuongTienTroCapChucVu;
            }
            set
            {
                SetPropertyValue("NgayHuongTienTroCapChucVu", ref _NgayHuongTienTroCapChucVu, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tiền trợ cấp chức vụ")]
        public decimal TienTroCapChucVu
        {
            get
            {
                return _TienTroCapChucVu;
            }
            set
            {
                SetPropertyValue("TienTroCapChucVu", ref _TienTroCapChucVu, value);
            }
        }


        [Browsable(false)]
        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    object obj = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("ThongTinNhanVien=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Từ năm")]
        public string TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Đến năm")]
        public string DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }

        [ModelDefault("Caption", "Quyết Định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
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

        [ModelDefault("Caption", "Ngày hết hạn quyết định")]
        public DateTime NgayHetQuyetDinh
        {
            get
            {
                return _NgayHetQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayHetQuyetDinh", ref _NgayHetQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public DanhMuc.ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "HS chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoPhuCapChucVu
        {
            get
            {
                return _HeSoPhuCapChucVu;
            }
            set
            {
                SetPropertyValue("HeSoPhuCapChucVu", ref _HeSoPhuCapChucVu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HS chức vụ")]
        public DateTime NgayHuongHeSo
        {
            get
            {
                return _NgayHuongHeSo;
            }
            set
            {
                SetPropertyValue("NgayHuongHeSo", ref _NgayHuongHeSo, value);
            }
        }

        public QuaTrinhBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    if (QuyetDinh != null)
        //    {                
        //        SoQuyetDinh = QuyetDinh.SoQuyetDinh;
        //        NgayQuyetDinh = QuyetDinh.NgayQuyetDinh;
        //    }
        //}
    }

}
