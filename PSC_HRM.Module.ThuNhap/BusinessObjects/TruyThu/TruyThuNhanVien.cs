using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.TruyThu
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Truy thu")]
    [RuleCombinationOfPropertiesIsUnique("TruyThuNhanVien.Unique", DefaultContexts.Save, "BangTruyThu;ThongTinNhanVien")]
    public class TruyThuNhanVien : ThuNhapBaseObject, IBoPhan
    {
        private BangTruyThu _BangTruyThu;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng truy thu")]
        [Association("BangTruyThu-ListTruyThuNhanVien")]
        public BangTruyThu BangTruyThu
        {
            get
            {
                return _BangTruyThu;
            }
            set
            {
                SetPropertyValue("BangTruyThu", ref _BangTruyThu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết truy thu")]
        [Association("TruyThuNhanVien-ListChiTietTruyThu")]
        public XPCollection<ChiTietTruyThu> ListChiTietTruyThu
        {
            get
            {
                return GetCollection<ChiTietTruyThu>("ListChiTietTruyThu");
            }
        }

        public TruyThuNhanVien(Session session) : base(session) { }

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
