using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinhService;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận và xếp lương")]
    public class QuyetDinhTiepNhanVaXepLuong : QuyetDinhCaNhan
    {
        // Fields...
        private bool _Huong85PhanTramLuong;
        private DateTime _NgayHuongLuong;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private decimal _TienLuong;
        private int _VuotKhung;
        private DateTime _MocNangLuong;
        private DateTime _NgayPhatSinhBienDong;
        private BoPhan _BoPhanMoi;
        private string _DonViCu;
        private bool _QuyetDinhMoi;
        private DateTime _TuNgay;

        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ModelDefault("Caption", "Cơ quan cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string DonViCu
        {
            get
            {
                return _DonViCu;
            }
            set
            {
                SetPropertyValue("DonViCu", ref _DonViCu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                {
                    BacLuong = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương ")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuong = value.HeSoLuong;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung
        {
            get
            {
                return _VuotKhung;
            }
            set
            {
                SetPropertyValue("VuotKhung", ref _VuotKhung, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
                //if (value != DateTime.MinValue)
                //{
                //    MocNangLuong = NgayHuongLuong;
                //}
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }

        [ModelDefault("Caption", "Hưởng 85% lương")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return _Huong85PhanTramLuong;
            }
            set
            {
                SetPropertyValue("Huong85PhanTramLuong", ref _Huong85PhanTramLuong, value);
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        public QuyetDinhTiepNhanVaXepLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhanVaXepLuong;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tiếp nhận và xếp lương"));
        }       

        protected override void OnSaving()
        {
            base.OnSaving();

            SystemContainer.Resolver<IQuyetDinhTiepNhanVaXepLuongService>("QDTiepNhanVaXepLuong" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhTiepNhanVaXepLuongService>("QDTiepNhanVaXepLuong" + TruongConfig.MaTruong).Delete(Session, this);

            base.OnDeleting();
        }

        protected override void AfterNhanVienChanged()
        {
            //thiết lập đơn vị
            BoPhanMoi = ThongTinNhanVien.BoPhan;
            TuNgay = ThongTinNhanVien.NgayVaoCoQuan;

            //cập nhật thông tin vào hồ sơ
            NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuong = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            HeSoLuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            VuotKhung = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            MocNangLuong = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;
            NgayHuongLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            Huong85PhanTramLuong =  ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong;
        }

    }

}
