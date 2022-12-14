using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BanLamViec;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.HopDong
{
    [NonPersistent]
    [ImageName("BO_HopDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết hết hạn hợp đồng")]
    public class ChiTietHetHanHopDong : BaseObject, IBoPhan, ISupportController
    {
        private string _LoaiHopDong;
        private bool _Chon;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private HopDong _HopDongLaoDong;
        private DateTime _NgayHetHan;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

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
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Số hợp đồng")]
        public HopDong HopDongLaoDong
        {
            get
            {
                return _HopDongLaoDong;
            }
            set
            {
                SetPropertyValue("HopDongLaoDong", ref _HopDongLaoDong, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return _NgayHetHan;
            }
            set
            {
                SetPropertyValue("NgayHetHan", ref _NgayHetHan, value);
            }
        }

        public ChiTietHetHanHopDong(Session session) : base(session) { }
    }

}
