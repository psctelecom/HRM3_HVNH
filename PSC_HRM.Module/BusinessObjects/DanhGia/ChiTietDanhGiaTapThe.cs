using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Chi tiết đánh giá tập thể")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDanhGiaTapThe.Unique", DefaultContexts.Save, "DanhGiaTapThe;TieuChuanDanhGiaTapThe")]
    public class ChiTietDanhGiaTapThe : BaseObject
    {
        // Fields...
        private string _MinhChung;
        private int _HoiDongDanhGia;
        private int _DiemTuDanhGia;
        private TieuChuanDanhGiaTapThe _TieuChuanDanhGiaTapThe;
        private DanhGiaTapThe _DanhGiaTapThe;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá tập thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("DanhGiaTapThe-ListChiTietDanhGiaTapThe")]
        public DanhGiaTapThe DanhGiaTapThe
        {
            get
            {
                return _DanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("DanhGiaTapThe", ref _DanhGiaTapThe, value);
            }
        }

        [ModelDefault("Caption", "Tiêu chuẩn đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TieuChuanDanhGiaTapThe TieuChuanDanhGiaTapThe
        {
            get
            {
                return _TieuChuanDanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("TieuChuanDanhGiaTapThe", ref _TieuChuanDanhGiaTapThe, value);
            }
        }

        [ModelDefault("Caption", "Điểm tự đánh giá")]
        public int DiemTuDanhGia
        {
            get
            {
                return _DiemTuDanhGia;
            }
            set
            {
                SetPropertyValue("DiemTuDanhGia", ref _DiemTuDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Hội đồng đánh giá")]
        public int HoiDongDanhGia
        {
            get
            {
                return _HoiDongDanhGia;
            }
            set
            {
                SetPropertyValue("HoiDongDanhGia", ref _HoiDongDanhGia, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Minh chứng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MinhChung
        {
            get
            {
                return _MinhChung;
            }
            set
            {
                SetPropertyValue("MinhChung", ref _MinhChung, value);
            }
        }

        public ChiTietDanhGiaTapThe(Session session) : base(session) { }
    }

}
