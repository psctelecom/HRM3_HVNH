using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Truy thu thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("TruyThuThueTNCN.Unique", DefaultContexts.Save, "BoPhan;ThongTinNhanVien;ChiTietQuanLyTruyThuThueTNCN")]
    public class TruyThuThueTNCN : BaseObject
    {
        // Fields...
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private decimal _TongSoTienTruyThu;
        private decimal _SoTienDaTruyThu;
        private decimal _SoTienTruyThuTrongKy;
        private decimal _SoTienConLai;
        private ChiTietQuanLyTruyThuThueTNCN _ChiTietQuanLyTruyThuThueTNCN;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Tổng số tiền truy thu")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal TongSoTienTruyThu
        {
            get
            {
                return _TongSoTienTruyThu;
            }
            set
            {
                SetPropertyValue("TongSoTienTruyThu", ref _TongSoTienTruyThu, value);
                if (!IsLoading)
                    SoTienConLai = value - SoTienDaTruyThu - SoTienTruyThuTrongKy;
            }
        }

        [ModelDefault("Caption", "Số tiền đã truy thu")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTienDaTruyThu
        {
            get
            {
                return _SoTienDaTruyThu;
            }
            set
            {
                SetPropertyValue("SoTienDaTruyThu", ref _SoTienDaTruyThu, value);
                if (!IsLoading)
                    SoTienConLai = TongSoTienTruyThu - value - SoTienTruyThuTrongKy;
            }
        }

        [ModelDefault("Caption", "Số tiền truy thu trong kỳ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTienTruyThuTrongKy
        {
            get
            {
                return _SoTienTruyThuTrongKy;
            }
            set
            {
                SetPropertyValue("SoTienTruyThuTrongKy", ref _SoTienTruyThuTrongKy, value);
                if (!IsLoading)
                    SoTienConLai = TongSoTienTruyThu - SoTienDaTruyThu - value;
            }
        }

        [ModelDefault("Caption", "Số tiền còn lại")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTienConLai
        {
            get
            {
                return _SoTienConLai;
            }
            set
            {
                SetPropertyValue("SoTienConLai", ref _SoTienConLai, value);
            }
        }

        [Browsable(false)]
        [Association("ChiTietQuanLyTruyThuThueTNCN-ListTruyThuThueTNCN")]
        public ChiTietQuanLyTruyThuThueTNCN ChiTietQuanLyTruyThuThueTNCN
        {
            get
            {
                return _ChiTietQuanLyTruyThuThueTNCN;
            }
            set
            {
                SetPropertyValue("ChiTietQuanLyTruyThuThueTNCN", ref _ChiTietQuanLyTruyThuThueTNCN, value);
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

        public TruyThuThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
