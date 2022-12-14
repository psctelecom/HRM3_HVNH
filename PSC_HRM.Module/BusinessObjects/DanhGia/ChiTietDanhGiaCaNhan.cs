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
    [ModelDefault("Caption", "Chi tiết đánh giá cá nhân")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietDanhGiaCaNhan.Unique", DefaultContexts.Save, "DanhGiaCaNhan;TieuChuanDanhGiaCaNhan")]
    public class ChiTietDanhGiaCaNhan : BaseObject
    {
        // Fields...
        private int _HieuTruongDanhGia;
        private int _CapTrenDanhGia;
        private int _HoiDongDanhGia;
        private int _CaNhanTuDanhGia;
        private TieuChuanDanhGiaCaNhan _TieuChuanDanhGiaCaNhan;
        private DanhGiaCaNhan _DanhGiaCaNhan;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá cá nhân")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Association("DanhGiaCaNhan-ListChiTietDanhGiaCaNhan")]
        public DanhGiaCaNhan DanhGiaCaNhan
        {
            get
            {
                return _DanhGiaCaNhan;
            }
            set
            {
                SetPropertyValue("DanhGiaCaNhan", ref _DanhGiaCaNhan, value);
            }
        }

        [ModelDefault("Caption", "Tiêu chuẩn đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TieuChuanDanhGiaCaNhan TieuChuanDanhGiaCaNhan
        {
            get
            {
                return _TieuChuanDanhGiaCaNhan;
            }
            set
            {
                SetPropertyValue("TieuChuanDanhGiaCaNhan", ref _TieuChuanDanhGiaCaNhan, value);
            }
        }

        [ModelDefault("Caption", "Điểm tự đánh giá")]
        public int CaNhanTuDanhGia
        {
            get
            {
                return _CaNhanTuDanhGia;
            }
            set
            {
                SetPropertyValue("CaNhanTuDanhGia", ref _CaNhanTuDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Trưởng đơn vị đánh giá")]
        public int CapTrenDanhGia
        {
            get
            {
                return _CapTrenDanhGia;
            }
            set
            {
                SetPropertyValue("CapTrenDanhGia", ref _CapTrenDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Hiệu trưởng đánh giá")]
        public int HieuTruongDanhGia
        {
            get
            {
                return _HieuTruongDanhGia;
            }
            set
            {
                SetPropertyValue("HieuTruongDanhGia", ref _HieuTruongDanhGia, value);
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

        public ChiTietDanhGiaCaNhan(Session session) : base(session) { }
    }

}
