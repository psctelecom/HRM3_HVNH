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

namespace PSC_HRM.Module.NangThamNien
{
    [ImageName("BO_NangThamNien")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị nâng phụ cấp thâm niên")]
    //[RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DeNghiNangPhuCapThamNien;ThongTinNhanVien")]
    public class ChiTietDeNghiNangPhuCapThamNien : BaseObject
    {
        private string _SoQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DeNghiNangPhuCapThamNien _DeNghiNangPhuCapThamNien;
        private DateTime _NgayHuongThamNienCu;
        private decimal _ThamNienCu;
        private DateTime _NgayHuongThamNienMoi;
        private decimal _ThamNienMoi;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Đề nghị nâng phụ cấp thâm niên")]
        [Association("DeNghiNangPhuCapThamNien-ListChiTietDeNghiNangPhuCapThamNien")]
        public DeNghiNangPhuCapThamNien DeNghiNangPhuCapThamNien
        {
            get
            {
                return _DeNghiNangPhuCapThamNien;
            }
            set
            {
                SetPropertyValue("DeNghiNangPhuCapThamNien", ref _DeNghiNangPhuCapThamNien, value);
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ThamNienCu = value.NhanVienThongTinLuong.ThamNien;
                    NgayHuongThamNienCu = value.NhanVienThongTinLuong.NgayHuongThamNien == DateTime.MinValue ? value.NgayTinhThamNienNhaGiao : value.NhanVienThongTinLuong.NgayHuongThamNien;

                    if (ThongTinNhanVien.ThamGiaGiangDay)
                        ThamNienMoi = (ThamNienCu == 0 && DeNghiNangPhuCapThamNien != null) ? HamDungChung.TinhSoNam(value.NgayTinhThamNienNhaGiao, DeNghiNangPhuCapThamNien.Thang.SetTime(SetTimeEnum.EndMonth)) : ThamNienCu + 1;
                    else
                        if (TruongConfig.MaTruong != "IUH")
                            ThamNienMoi = ThamNienCu == 0 ? 5 : ThamNienCu + 0.5m;
                    NgayHuongThamNienMoi = NgayHuongThamNienCu != DateTime.MinValue ? NgayHuongThamNienCu.AddMonths((int)((ThamNienMoi - ThamNienCu) * 12)) : value.NgayTinhThamNienNhaGiao.AddYears((int)ThamNienMoi);
                }
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên cũ")]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);

            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên cũ")]
        public DateTime NgayHuongThamNienCu
        {
            get
            {
                return _NgayHuongThamNienCu;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienCu", ref _NgayHuongThamNienCu, value);

            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên mới")]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
            }
        }

        [ModelDefault("Caption", "Mốc tính thâm niên mới")]
        public DateTime NgayHuongThamNienMoi
        {
            get
            {
                return _NgayHuongThamNienMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienMoi", ref _NgayHuongThamNienMoi, value);

            }
        }

        public ChiTietDeNghiNangPhuCapThamNien(Session session) : base(session) { }

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
