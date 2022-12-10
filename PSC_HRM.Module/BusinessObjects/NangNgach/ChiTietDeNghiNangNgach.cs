using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NangNgach
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị nâng ngạch")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DeNghiNangNgach;ThongTinNhanVien")]
    public class ChiTietDeNghiNangNgach : BaseObject
    {
        // Fields...
        private DateTime _NgayBoNhiemNgachMoi;
        private DateTime _NgayBoNhiemNgachCu;
        private DateTime _MocNangLuongMoi;
        private DateTime _NgayHuongLuongMoi;
        private decimal _HeSoLuongMoi;
        private BacLuong _BacLuongMoi;
        private NgachLuong _NgachLuongMoi;
        private DateTime _MocNangLuongCu;
        private DateTime _NgayHuongLuongCu;
        private decimal _HeSoLuongCu;
        private BacLuong _BacLuongCu;
        private NgachLuong _NgachLuongCu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DeNghiNangNgach _DeNghiNangNgach;
        private string _SoQuyetDinh;

        [Browsable(false)]
        [ModelDefault("Caption", "Đề nghị nâng ngạch")]
        [Association("DeNghiNangNgach-ListChiTietDeNghiNangNgach")]
        public DeNghiNangNgach DeNghiNangNgach
        {
            get
            {
                return _DeNghiNangNgach;
            }
            set
            {
                SetPropertyValue("DeNghiNangNgach", ref _DeNghiNangNgach, value);
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        [RuleUniqueValue("", DefaultContexts.Save)]
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
                        if (BoPhan == null
                            || value.BoPhan.Oid != BoPhan.Oid)
                            BoPhan = value.BoPhan;
                    NgachLuongCu = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuongCu = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuongCu = value.NhanVienThongTinLuong.HeSoLuong;
                    NgayBoNhiemNgachCu = value.NhanVienThongTinLuong.NgayBoNhiemNgach;
                    NgayHuongLuongCu = value.NhanVienThongTinLuong.NgayHuongLuong;
                    MocNangLuongCu = value.NhanVienThongTinLuong.MocNangLuong;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
                if (!IsLoading)
                    BacLuongCu = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NgachLuongCu.ListBacLuong")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
                if (!IsLoading && value != null)
                {
                    TinhMocNangLuong();
                    HeSoLuongCu = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgachCu
        {
            get
            {
                return _NgayBoNhiemNgachCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachCu", ref _NgayBoNhiemNgachCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongMoi
        {
            get
            {
                return _NgachLuongMoi;
            }
            set
            {
                SetPropertyValue("NgachLuongMoi", ref _NgachLuongMoi, value);
                if (!IsLoading)
                    BacLuongMoi = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NgachLuongMoi.ListBacLuong")]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading && value != null)
                {
                    TinhMocNangLuong();
                    HeSoLuongMoi = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Hệ số lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgachMoi
        {
            get
            {
                return _NgayBoNhiemNgachMoi;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachMoi", ref _NgayBoNhiemNgachMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }

        public ChiTietDeNghiNangNgach(Session session) : base(session) { }

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

        private void TinhMocNangLuong()
        {
            //tính mốc nâng lương ở đây
            //nếu mới 1 - cũ 1 >= cũ 2 - cũ 1
            //  ngày hiệu lực
            //  mốc nâng lương cũ
            if (BacLuongCu != null && BacLuongMoi != null)
            {
                int bac;
                if (int.TryParse(BacLuongCu.MaQuanLy, out bac))
                {
                    bac++;
                    BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy=?",
                        BacLuongCu.NgachLuong.Oid, bac));
                    if (bacLuong != null)
                    {
                        if (BacLuongMoi.HeSoLuong - BacLuongCu.HeSoLuong >= bacLuong.HeSoLuong - BacLuongCu.HeSoLuong)
                            MocNangLuongMoi = HamDungChung.GetServerTime();
                        else
                            MocNangLuongMoi = MocNangLuongCu;
                    }
                }
            }
        }
    }

}
