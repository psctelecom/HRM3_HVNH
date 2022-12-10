 using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangThamNien;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiế nâng phụ cấp thâm niên hành chính")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangPhuCapThamNienHanhChinh;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangPhuCapThamNienHanhChinh : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangPhuCapThamNienHanhChinh _QuyetDinhNangPhuCapThamNienHanhChinh;
        private DateTime _NgayHuongThamNienHCCu;
        private DateTime _NgayHuongThamNienHCMoi;
        private decimal _PhanTramThamNienHCMoi;
        private decimal _PhanTramThamNienHCCu;
        private decimal _HSPCThamNienHCCu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng phụ cấp thâm niên hành chính")]
        [Association("QuyetDinhNangPhuCapThamNienHanhChinh-ListChiTietQuyetDinhNangPhuCapThamNienHanhChinh")]
        public QuyetDinhNangPhuCapThamNienHanhChinh QuyetDinhNangPhuCapThamNienHanhChinh
        {
            get
            {
                return _QuyetDinhNangPhuCapThamNienHanhChinh;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangPhuCapThamNienHanhChinh", ref _QuyetDinhNangPhuCapThamNienHanhChinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                    BoPhanText = value.TenBoPhan;
                }
            }
        }
        [VisibleInDetailView(false)]
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

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "% Thâm niên HC cũ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal PhanTramThamNienHCCu
        {
            get
            {
                return _PhanTramThamNienHCCu;
            }
            set
            {
                SetPropertyValue("PhanTramThamNienHCCu", ref _PhanTramThamNienHCCu, value);
            }
        }
        [ModelDefault("Caption", "HSPC Thâm niên HC cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNienHCCu
        {
            get
            {
                return _HSPCThamNienHCCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienHCCu", ref _HSPCThamNienHCCu, value);
            }
        }
        [ModelDefault("Caption", "Ngày hưởng thâm niên cũ")]
        public DateTime NgayHuongThamNienHCCu
        {
            get
            {
                return _NgayHuongThamNienHCCu;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienHCCu", ref _NgayHuongThamNienHCCu, value);
            }
        }

        [ModelDefault("Caption", "% Thâm niên HC mới")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal PhanTramThamNienHCMoi
        {
            get
            {
                return _PhanTramThamNienHCMoi;
            }
            set
            {
                SetPropertyValue("PhanTramThamNienHCMoi", ref _PhanTramThamNienHCMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên HC mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongThamNienHCMoi
        {
            get
            {
                return _NgayHuongThamNienHCMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienHCMoi", ref _NgayHuongThamNienHCMoi, value);
            }
        }

        public ChiTietQuyetDinhNangPhuCapThamNienHanhChinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (this.QuyetDinhNangPhuCapThamNienHanhChinh.QuyetDinhMoi && NgayHuongThamNienHCMoi <= HamDungChung.GetServerTime())
                {
                    //cập nhật thâm niên
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanTramThamNienHC = PhanTramThamNienHCMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNienHC = NgayHuongThamNienHCMoi;
                }
            }
        }

        protected override void OnDeleting()
        {
            //lấy lại dữ liệu cũ
            if (this.QuyetDinhNangPhuCapThamNienHanhChinh.QuyetDinhMoi)
            {
                ThongTinNhanVien.NhanVienThongTinLuong.PhanTramThamNienHC = PhanTramThamNienHCCu;
                ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNienHC = NgayHuongThamNienHCCu;
                ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienHC = HSPCThamNienHCCu;
            }

            base.OnDeleting();
        }
    }

}
