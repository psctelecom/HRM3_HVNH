using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_NgoaiNguKhac")]
    [ModelDefault("Caption", "Kiêm nhiệm")]
    public class ChucVuKiemNhiem : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private BoPhan _BoPhan;
        private BoPhan _BoMon;
        private decimal _TienTroCapChucVuKiemNhiem;
        private DateTime _NgayBoNhiem;
        private bool _DaMienNhiem;
        private QuyetDinhBoNhiemKiemNhiem _QuyetDinhBoNhiemKiemNhiem;       
       
        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListChucVuKiemNhiem")]
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

        [ModelDefault("Caption", "Chức vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVu
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

        [ModelDefault("Caption", "Chức danh")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Bộ môn")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("Caption", "Ngày kiêm nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                return _NgayBoNhiem;
            }
            set
            {
                SetPropertyValue("NgayBoNhiem", ref _NgayBoNhiem, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Tiền trợ cấp chức vụ kiêm nhiệm")]
        public decimal TienTroCapChucVuKiemNhiem
        {
            get
            {
                return _TienTroCapChucVuKiemNhiem;
            }
            set
            {
                SetPropertyValue("TienTroCapChucVuKiemNhiem", ref _TienTroCapChucVuKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Đã miễn nhiệm")]
        public bool DaMienNhiem
        {
            get
            {
                return _DaMienNhiem;
            }
            set
            {
                SetPropertyValue("DaMienNhiem", ref _DaMienNhiem, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định bổ nhiệm kiêm nhiệm")]
        public QuyetDinhBoNhiemKiemNhiem QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Phân loại")]
        public string PhanLoai
        {
            get
            {
                return ChucDanh != null ? ChucDanh.TenChucDanh : "Kiêm nhiệm chức vụ";
            }          
        }
        public ChucVuKiemNhiem(Session session) : base(session) { }

        
    }

}

