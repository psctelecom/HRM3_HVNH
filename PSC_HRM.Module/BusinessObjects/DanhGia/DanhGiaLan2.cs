using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhGia
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Đánh giá lần 2")]
    public class DanhGiaLan2 : BaseObject, IBoPhan
    {
        private DanhGiaCanBo _DanhGiaCanBo;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private XepLoaiCanBo _XepLoaiLaoDong;
        private string _NhanXet;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá cán bộ")]
        [Association("DanhGiaCanBo-DanhSachDanhGiaLan2")]
        public DanhGiaCanBo DanhGiaCanBo
        {
            get
            {
                return _DanhGiaCanBo;
            }
            set
            {
                SetPropertyValue("DanhGiaCanBo", ref _DanhGiaCanBo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
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
                    ThongTinNhanVien = null;
                    UpdateNVList();
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiCanBo XepLoaiLaoDong
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

        public DanhGiaLan2(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
