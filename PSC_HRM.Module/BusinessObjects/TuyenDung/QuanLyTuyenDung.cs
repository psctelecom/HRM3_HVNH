using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.TuyenDung
{
    [DefaultClassOptions]
    [ImageName("group2_32x32")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc;DotTuyenDung")]
    //[Appearance("QuanLyTuyenDung", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyTuyenDung : BaoMatBaseObject
    {
        private TrangThaiTuyenDungEnum _TrangThai;
        private DateTime _NopHoSoDenNgay;
        private DateTime _NopHoSoTuNgay;
        private DateTime _ThucHienDenNgay;
        private DateTime _ThucHienTuNgay;
        private DateTime _DuKienDenNgay;
        private DateTime _DuKienTuNgay;
        private NamHoc _NamHoc;
        private int _DotTuyenDung;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Đợt tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        public int DotTuyenDung
        {
            get
            {
                return _DotTuyenDung;
            }
            set
            {
                SetPropertyValue("DotTuyenDung", ref _DotTuyenDung, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Caption
        {
            get
            {
                if (NamHoc != null)
                    return string.Format("Năm học {0} đợt {1}", NamHoc.TenNamHoc, DotTuyenDung);
                else
                    return "";
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime DuKienTuNgay
        {
            get
            {
                return _DuKienTuNgay;
            }
            set
            {
                SetPropertyValue("DuKienTuNgay", ref _DuKienTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DuKienDenNgay
        {
            get
            {
                return _DuKienDenNgay;
            }
            set
            {
                SetPropertyValue("DuKienDenNgay", ref _DuKienDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày 1")]
        public DateTime ThucHienTuNgay
        {
            get
            {
                return _ThucHienTuNgay;
            }
            set
            {
                SetPropertyValue("ThucHienTuNgay", ref _ThucHienTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày 1")]
        public DateTime ThucHienDenNgay
        {
            get
            {
                return _ThucHienDenNgay;
            }
            set
            {
                SetPropertyValue("ThucHienDenNgay", ref _ThucHienDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày 2")]
        public DateTime NopHoSoTuNgay
        {
            get
            {
                return _NopHoSoTuNgay;
            }
            set
            {
                SetPropertyValue("NopHoSoTuNgay", ref _NopHoSoTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày 2")]
        public DateTime NopHoSoDenNgay
        {
            get
            {
                return _NopHoSoDenNgay;
            }
            set
            {
                SetPropertyValue("NopHoSoDenNgay", ref _NopHoSoDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiTuyenDungEnum TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [Association("QuanLyTuyenDung-ListViTriTuyenDung")]
        public XPCollection<ViTriTuyenDung> ListViTriTuyenDung
        {
            get
            {
                return GetCollection<ViTriTuyenDung>("ListViTriTuyenDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đăng ký tuyển dụng")]
        [Association("QuanLyTuyenDung-ListDangKyTuyenDung")]
        public XPCollection<DangKyTuyenDung> ListDangKyTuyenDung
        {
            get
            {
                return GetCollection<DangKyTuyenDung>("ListDangKyTuyenDung");
            }
        }

        [ModelDefault("Caption", "Duyệt đăng ký tuyển dụng")]
        [Association("QuanLyTuyenDung-ListNhuCauTuyenDung")]
        public XPCollection<NhuCauTuyenDung> ListNhuCauTuyenDung
        {
            get
            {
                return GetCollection<NhuCauTuyenDung>("ListNhuCauTuyenDung");
            }
        }

        [ModelDefault("Caption", "Hội đồng tuyển dụng")]
        [Association("QuanLyTuyenDung-ListHoiDongTuyenDung")]
        public XPCollection<HoiDongTuyenDung> ListHoiDongTuyenDung
        {
            get
            {
                return GetCollection<HoiDongTuyenDung>("ListHoiDongTuyenDung");
            }
        }

        [ModelDefault("Caption", "Danh sách ứng viên")]
        [Association("QuanLyTuyenDung-ListUngVien")]
        public XPCollection<UngVien> ListUngVien
        {
            get
            {
                return GetCollection<UngVien>("ListUngVien");
            }
        }

        [ModelDefault("Caption", "Chi tiết tuyển dụng")]
        [Association("QuanLyTuyenDung-ListChiTietTuyenDung")]
        public XPCollection<ChiTietTuyenDung> ListChiTietTuyenDung
        {
            get
            {
                return GetCollection<ChiTietTuyenDung>("ListChiTietTuyenDung");
            }
        }

        [ModelDefault("Caption", "Danh sách trúng tuyển")]
        [Association("QuanLyTuyenDung-ListTrungTuyen")]
        public XPCollection<TrungTuyen> ListTrungTuyen
        {
            get
            {
                return GetCollection<TrungTuyen>("ListTrungTuyen");
            }
        }

        public QuanLyTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DotTuyenDung = 1;

            //khoi tao du lieu vi tri tuyen dung
            //TuyenDungHelper.CreateViTriTuyenDung(Session, this, "01", "Giảng viên", LoaiNhanVienEnum.CoHuu);
            //TuyenDungHelper.CreateViTriTuyenDung(Session, this, "02", "Giảng viên", LoaiNhanVienEnum.ThinhGiang);
            //TuyenDungHelper.CreateViTriTuyenDung(Session, this, "03", "Nhân viên", LoaiNhanVienEnum.BienChe);
        }

        protected override void OnDeleting()
        {
            Session.Delete(ListViTriTuyenDung);
            Session.Save(ListViTriTuyenDung);
            Session.Delete(ListDangKyTuyenDung);
            Session.Save(ListDangKyTuyenDung);
            Session.Delete(ListNhuCauTuyenDung);
            Session.Save(ListNhuCauTuyenDung);
            Session.Delete(ListHoiDongTuyenDung);
            Session.Save(ListHoiDongTuyenDung);
            Session.Delete(ListUngVien);
            Session.Save(ListUngVien);
            Session.Delete(ListChiTietTuyenDung);
            Session.Save(ListChiTietTuyenDung);
            Session.Delete(ListTrungTuyen);
            Session.Save(ListTrungTuyen);

            base.OnDeleting();
        }
    }
}
