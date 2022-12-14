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
    [ModelDefault("Caption", "Chi tiết hợp đồng mới")]
    public class ChiTietHopDongMoi : BaseObject, IBoPhan, ISupportController
    {
        private string _LoaiHopDong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private HopDong _HopDongLaoDong;
        private DateTime _NgayKyHopDong;

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

        [ModelDefault("Caption", "Ngày ký hợp đồng")]
        public DateTime NgayKyHopDong
        {
            get
            {
                return _NgayKyHopDong;
            }
            set
            {
                SetPropertyValue("NgayKyHopDong", ref _NgayKyHopDong, value);
            }
        }

        public ChiTietHopDongMoi(Session session) : base(session) { }
    }

}
