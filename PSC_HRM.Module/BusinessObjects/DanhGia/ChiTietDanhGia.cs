using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ModelDefault("Caption", "Đánh giá cán bộ")]
    [DefaultProperty("ThongTinNhanVien")]
    public class ChiTietDanhGia : BaseObject, IBoPhan
    {
        // Fields...
        private DanhGiaTongQuat _DanhGiaTongQuat;
        private BoPhan _BoPhan;
        private string _GhiChu;
        private XepLoaiChung _XepLoaiChung;
        private XepLoaiChuyenMon _XepLoaiChuyenMon;
        private XepLoaiDaoDuc _XepLoaiDaoDuc;
        private XepLoaiSucKhoe _XepLoaiSucKhoe;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [Association("DanhGiaTongQuat-ChiTietDanhGiaList")]
        public DanhGiaTongQuat DanhGiaTongQuat
        {
            get
            {
                return _DanhGiaTongQuat;
            }
            set
            {
                SetPropertyValue("DanhGiaTongQuat", ref _DanhGiaTongQuat, value);
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Xếp loại sức khỏe")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiSucKhoe XepLoaiSucKhoe
        {
            get
            {
                return _XepLoaiSucKhoe;
            }
            set
            {
                SetPropertyValue("XepLoaiSucKhoe", ref _XepLoaiSucKhoe, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại đạo đức")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDaoDuc XepLoaiDaoDuc
        {
            get
            {
                return _XepLoaiDaoDuc;
            }
            set
            {
                SetPropertyValue("XepLoaiDaoDuc", ref _XepLoaiDaoDuc, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại chuyên môn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiChuyenMon XepLoaiChuyenMon
        {
            get
            {
                return _XepLoaiChuyenMon;
            }
            set
            {
                SetPropertyValue("XepLoaiChuyenMon", ref _XepLoaiChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại chung")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiChung XepLoaiChung
        {
            get
            {
                return _XepLoaiChung;
            }
            set
            {
                SetPropertyValue("XepLoaiChung", ref _XepLoaiChung, value);
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

        public ChiTietDanhGia(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNVList();
        }

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
