using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.NangThamNienTangThem
{
    [ImageName("BO_NangThamNien")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị nâng thâm niên tăng thêm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DeNghiNangThamNienTangThem;ThongTinNhanVien")]
    public class ChiTietDeNghiNangThamNienTangThem : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DeNghiNangThamNienTangThem _DeNghiNangThamNienTangThem;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienCu;
        private HSLTangThemTheoThamNien _HSLTangThemTheoThamNienMoi;
        private DateTime _MocHuongThamNienTangThemCu;
        private DateTime _MocHuongThamNienTangThemMoi;
        private string _SoQuyetDinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Đề nghị nâng thâm niên tăng thêm")]
        [Association("DeNghiNangThamNienTangThem-ListChiTietDeNghiNangThamNienTangThem")]
        public DeNghiNangThamNienTangThem DeNghiNangThamNienTangThem
        {
            get
            {
                return _DeNghiNangThamNienTangThem;
            }
            set
            {
                SetPropertyValue("DeNghiNangThamNienTangThem", ref _DeNghiNangThamNienTangThem, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
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
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    MocHuongThamNienTangThemCu = value.NhanVienThongTinLuong.NgayHuongThamNien == DateTime.MinValue ? value.NgayTinhThamNienNhaGiao : value.NhanVienThongTinLuong.NgayHuongThamNien;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số TNTT cũ")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienCu
        {
            get
            {
                return _HSLTangThemTheoThamNienCu;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienCu", ref _HSLTangThemTheoThamNienCu, value);

            }
        }

        [ModelDefault("Caption", "Hệ số TNTT mới")]
        public HSLTangThemTheoThamNien HSLTangThemTheoThamNienMoi
        {
            get
            {
                return _HSLTangThemTheoThamNienMoi;
            }
            set
            {
                SetPropertyValue("HSLTangThemTheoThamNienMoi", ref _HSLTangThemTheoThamNienMoi, value);
            }
        }

        [ModelDefault("Caption", "Mốc hưởng TNTT cũ")]
        public DateTime MocHuongThamNienTangThemCu
        {
            get
            {
                return _MocHuongThamNienTangThemCu;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemCu", ref _MocHuongThamNienTangThemCu, value);

            }
        }
               
        [ModelDefault("Caption", "Mốc tính TNTT mới")]
        public DateTime MocHuongThamNienTangThemMoi
        {
            get
            {
                return _MocHuongThamNienTangThemMoi;
            }
            set
            {
                SetPropertyValue("MocHuongThamNienTangThemMoi", ref _MocHuongThamNienTangThemMoi, value);

            }
        }

        public ChiTietDeNghiNangThamNienTangThem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
