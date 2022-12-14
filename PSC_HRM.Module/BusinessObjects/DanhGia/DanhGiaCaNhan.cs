using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Đánh giá cá nhân")]
    [RuleCombinationOfPropertiesIsUnique("DanhGiaCaNhan.Unique", DefaultContexts.Save, "QuanLyDanhGia;ThongTinNhanVien")]
    public class DanhGiaCaNhan : BaseObject, IBoPhan
    {
        // Fields...
        private string _GhiChu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyDanhGia _QuanLyDanhGia;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đánh giá")]
        [Association("QuanLyDanhGia-ListDanhGiaCaNhan")]
        public QuanLyDanhGia QuanLyDanhGia
        {
            get
            {
                return _QuanLyDanhGia;
            }
            set
            {
                SetPropertyValue("QuanLyDanhGia", ref _QuanLyDanhGia, value);
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
                    UpdateNVList();
                }
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

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

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đánh giá")]
        [Association("DanhGiaCaNhan-ListChiTietDanhGiaCaNhan")]
        public XPCollection<ChiTietDanhGiaCaNhan> ListChiTietDanhGiaCaNhan
        {
            get
            {
                return GetCollection<ChiTietDanhGiaCaNhan>("ListChiTietDanhGiaCaNhan");
            }
        }

        public DanhGiaCaNhan(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
