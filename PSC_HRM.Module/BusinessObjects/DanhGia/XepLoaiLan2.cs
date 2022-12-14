using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Xếp loại lần 2")]
    [RuleCombinationOfPropertiesIsUnique("XepLoaiLan2.Unique", DefaultContexts.Save, "XepLoaiLaoDong;ThongTinNhanVien")]
    public class XepLoaiLan2 : BaseObject, IBoPhan
    {
        private XepLoaiLaoDong _XepLoaiLaoDong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private XepLoaiDanhGiaEnum _XepLoai;
        private string _NhanXet;

        [Browsable(false)]
        [ModelDefault("Caption", "Xếp loại lao động")]
        [Association("XepLoaiLaoDong-ListXepLoaiLan2")]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiDanhGiaEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
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

        public XepLoaiLan2(Session session) : base(session) { }

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
