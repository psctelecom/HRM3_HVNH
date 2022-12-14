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
    [ModelDefault("Caption", "Xếp loại lần 1")]
    [RuleCombinationOfPropertiesIsUnique("XepLoaiLan1.Unique", DefaultContexts.Save, "XepLoaiLaoDong;ThongTinNhanVien")]
    public class XepLoaiLan1 : BaseObject, IBoPhan
    {
        private XepLoaiDanhGiaEnum _PhanMemDanhGia;
        private XepLoaiDanhGiaEnum _TruongDonViDanhGia;
        private XepLoaiLaoDong _XepLoaiLaoDong;
        private XepLoaiDanhGiaEnum _CaNhanTuDanhGia;
        private string _NhanXet;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Xếp loại lao động")]
        [Association("XepLoaiLaoDong-ListXepLoaiLan1")]
        public XepLoaiLaoDong XepLoaiLaoDong
        {
            get
            {
                return _XepLoaiLaoDong;
            }
            set
            {
                SetPropertyValue("XepLoaiLaoDong", ref _XepLoaiLaoDong, value);
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

        [ModelDefault("Caption", "Cá nhân tự đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDanhGiaEnum CaNhanTuDanhGia
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

        [ModelDefault("Caption", "Phần mềm đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDanhGiaEnum PhanMemDanhGia
        {
            get
            {
                return _PhanMemDanhGia;
            }
            set
            {
                SetPropertyValue("PhanMemDanhGia", ref _PhanMemDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Trưởng đơn vị đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiDanhGiaEnum TruongDonViDanhGia
        {
            get
            {
                return _TruongDonViDanhGia;
            }
            set
            {
                SetPropertyValue("TruongDonViDanhGia", ref _TruongDonViDanhGia, value);
            }
        }

        [ModelDefault("Caption", "Nhận xét")]
        public string NhanXet
        {
            get
            {
                return _NhanXet;
            }
            set
            {
                SetPropertyValue("NhanXet", ref _NhanXet, value);
            }
        }

        public XepLoaiLan1(Session session) : base(session) { }

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
