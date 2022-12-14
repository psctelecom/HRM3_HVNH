using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Model;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.BaoMat
{
    [ImageName("BO_Category")]
    [DefaultProperty("TenBoPhan")]
    [ModelDefault("Caption", "Thông tin trường")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy;TenBoPhan")]
    public class ThongTinTruong : BoPhan
    {
        private string _MaKhamChuaBenh;
        private string _MaThamGiaBaoHiem;
        private string _Email;
        private string _Fax;
        private string _WebSite;
        private int _NamThanhLap;
        private string _TenVietTat;
        private string _DonViChuQuan;
        private string _MaSoThue;
        private CoQuanThue _CoQuanThueCapCuc;
        private CoQuanThue _CoQuanThueQuanLy;
        private DiaChi _DiaChi;
        private string _DienThoai;
        private ThongTinChung _ThongTinChung;
        private MocTinhThueTNCN _MocTinhThueTNCN;

        [ModelDefault("Caption", "Mã tham gia bảo hiểm")]
        public string MaThamGiaBaoHiem
        {
            get
            {
                return _MaThamGiaBaoHiem;
            }
            set
            {
                SetPropertyValue("MaThamGiaBaoHiem", ref _MaThamGiaBaoHiem, value);
            }
        }

        [ModelDefault("Caption", "Mã khám chữa bệnh")]
        public string MaKhamChuaBenh
        {
            get
            {
                return _MaKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("MaKhamChuaBenh", ref _MaKhamChuaBenh, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Tên viết hoa")]
        public string TenVietHoa
        {
            get
            {
                if (!String.IsNullOrEmpty(TenBoPhan))
                    return TenBoPhan.ToUpper();
                return "";
            }
        }

        [ModelDefault("Caption", "Tên viết tắt")]
        public string TenVietTat
        {
            get
            {
                return _TenVietTat;
            }
            set
            {
                SetPropertyValue("TenVietTat", ref _TenVietTat, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị chủ quản")]
        public string DonViChuQuan
        {
            get
            {
                return _DonViChuQuan;
            }
            set
            {
                SetPropertyValue("DonViChuQuan", ref _DonViChuQuan, value);
            }
        }

        [ModelDefault("Caption", "Năm thành lập")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamThanhLap
        {
            get
            {
                return _NamThanhLap;
            }
            set
            {
                SetPropertyValue("NamThanhLap", ref _NamThanhLap, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Địa chỉ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại")]
        public string DienThoai
        {
            get
            {
                return _DienThoai;
            }
            set
            {
                SetPropertyValue("DienThoai", ref _DienThoai, value);
            }
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        public string Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                SetPropertyValue("Fax", ref _Fax, value);
            }
        }

        public string WebSite
        {
            get
            {
                return _WebSite;
            }
            set
            {
                SetPropertyValue("WebSite", ref _WebSite, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Thông tin chung")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public ThongTinChung ThongTinChung
        {
            get
            {
                return _ThongTinChung;
            }
            set
            {
                SetPropertyValue("ThongTinChung", ref _ThongTinChung, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Mốc tính thuế TNCN")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocTinhThueTNCN MocTinhThueTNCN
        {
            get
            {
                return _MocTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("MocTinhThueTNCN", ref _MocTinhThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return _MaSoThue;
            }
            set
            {
                SetPropertyValue("MaSoThue", ref _MaSoThue, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cơ quan thuế cấp cục")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CoQuanThue CoQuanThueCapCuc
        {
            get
            {
                return _CoQuanThueCapCuc;
            }
            set
            {
                SetPropertyValue("CoQuanThueCapCuc", ref _CoQuanThueCapCuc, value);
                if (!IsLoading)
                    CoQuanThueQuanLy = null;
            }
        }

        [ModelDefault("Caption", "Cơ quan thuế quản lý")]
        [DataSourceProperty("CoQuanThueCapCuc.DonViTrucThuoc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CoQuanThue CoQuanThueQuanLy
        {
            get
            {
                return _CoQuanThueQuanLy;
            }
            set
            {
                SetPropertyValue("CoQuanThueQuanLy", ref _CoQuanThueQuanLy, value);
            }
        }

        [Delayed]
        [ModelDefault("Caption", "Logo")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        public System.Drawing.Image Logo
        {
            get
            {
                return GetDelayedPropertyValue<System.Drawing.Image>("Logo");
            }
            set
            {
                if (value != null)
                    SetDelayedPropertyValue<System.Drawing.Image>("Logo", new System.Drawing.Bitmap(value));
                else
                    SetDelayedPropertyValue<System.Drawing.Image>("Logo", value);

            }
        }

        [NonPersistent]
        [Browsable(false)]
        public CauHinhChung CauHinhChung
        {
            get
            {
                return Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong=?", Oid));
            }
        }

        [Association("ThongTinTruong-ListNguoiSuDung")]
        [ModelDefault("Caption", "Danh sách người dùng")]
        public XPCollection<NguoiSuDung> ListNguoiSuDung
        {
            get
            {
                return GetCollection<NguoiSuDung>("ListNguoiSuDung");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Tài khoản ngân hàng")]
        [Association("ThongTinTruong-ListTaiKhoanNganHang")]
        public XPCollection<TaiKhoanNganHang> ListTaiKhoanNganHang
        {
            get
            {
                return GetCollection<TaiKhoanNganHang>("ListTaiKhoanNganHang");
            }
        }

        public ThongTinTruong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DiaChi = new DanhMuc.DiaChi(Session);
            ThongTinChung = new ThongTinChung(Session);
            MocTinhThueTNCN = new MocTinhThueTNCN(Session);
            LoaiBoPhan = LoaiBoPhanEnum.Truong;
        }
    }
}
