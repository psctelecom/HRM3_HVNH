using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [ImageName("BO_KhauTru")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết khấu trừ tạm ứng")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietKhauTruTamUng", DefaultContexts.Save, "BangKhauTruTamUng;ThongTinNhanVien")]
    [Appearance("ChiTietKhauTruTamUng.Khoa", TargetItems = "*", Enabled = false,
        Criteria = "BangKhauTruTamUng is not null and ((BangKhauTruTamUng.KyTinhLuong is not null and BangKhauTruTamUng.KyTinhLuong.KhoaSo) or BangKhauTruTamUng.ChungTu is not null)")]
    public class ChiTietKhauTruTamUng : ThuNhapBaseObject, IBoPhan
    {
        // Fields...
        private TamUng _TamUng;
        private string _GhiChu;
        private decimal _SoTien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private BangKhauTruTamUng _BangKhauTruTamUng;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng khấu trừ tạm ứng")]
        [Association("BangKhauTruTamUng-ListChiTietKhauTruTamUng")]
        public BangKhauTruTamUng BangKhauTruTamUng
        {
            get
            {
                return _BangKhauTruTamUng;
            }
            set
            {
                SetPropertyValue("BangKhauTruTamUng", ref _BangKhauTruTamUng, value);
            }
        }

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

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        //chỉ dùng để lưu vết
        [Browsable(false)]
        public TamUng TamUng
        {
            get
            {
                return _TamUng;
            }
            set
            {
                SetPropertyValue("TamUng", ref _TamUng, value);
            }
        }

        public ChiTietKhauTruTamUng(Session session) : base(session) { }

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
