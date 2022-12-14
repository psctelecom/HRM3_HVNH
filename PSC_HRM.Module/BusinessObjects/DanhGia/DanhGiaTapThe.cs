using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Đánh giá tập thể")]
    [RuleCombinationOfPropertiesIsUnique("DanhGiaTapThe.Unique", DefaultContexts.Save, "QuanLyDanhGia;BoPhan")]
    public class DanhGiaTapThe : BaseObject, IBoPhan
    {
        // Fields...
        private NhomTieuChuanDanhGiaTapThe _NhomTieuChuanDanhGiaTapThe;
        private string _GhiChu;
        private BoPhan _BoPhan;
        private QuanLyDanhGia _QuanLyDanhGia;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đánh giá")]
        [Association("QuanLyDanhGia-ListDanhGiaTapThe")]
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

        [ModelDefault("Caption", "Tập thể")]
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
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nhóm tiêu chuẩn đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomTieuChuanDanhGiaTapThe NhomTieuChuanDanhGiaTapThe
        {
            get
            {
                return _NhomTieuChuanDanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("NhomTieuChuanDanhGiaTapThe", ref _NhomTieuChuanDanhGiaTapThe, value);
                if (!IsLoading && value != null)
                {
                    ChiTietDanhGiaTapThe chiTiet;
                    foreach (var item in value.ListTieuChuanDanhGiaTapThe)
                    {
                        chiTiet = Session.FindObject<ChiTietDanhGiaTapThe>(CriteriaOperator.Parse("DanhGiaTapThe=? and TieuChuanDanhGiaTapThe=?", Oid, item.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = new ChiTietDanhGiaTapThe(Session);
                            chiTiet.DanhGiaTapThe = this;
                            chiTiet.TieuChuanDanhGiaTapThe = item;
                        }
                    }
                }
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
        [Association("DanhGiaTapThe-ListChiTietDanhGiaTapThe")]
        public XPCollection<ChiTietDanhGiaTapThe> ListChiTietDanhGiaTapThe
        {
            get
            {
                return GetCollection<ChiTietDanhGiaTapThe>("ListChiTietDanhGiaTapThe");
            }
        }

        public DanhGiaTapThe(Session session) : base(session) { }
    }

}
