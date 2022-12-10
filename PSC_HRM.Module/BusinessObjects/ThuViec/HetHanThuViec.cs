using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HopDong;

namespace PSC_HRM.Module.ThuViec
{
    [NonPersistent]
    [ModelDefault("AllowNew", "False")]
    [ModelDefault("AllowDelete", "False")]
    [ModelDefault("Caption", "Hết hạn thử việc")]
    public class HetHanThuViec : BaseObject, ISupportController
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _NgayHopDong;
        private decimal _MucLuong;
        private decimal _ThuongHieuQuaTheoThang;

        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
                }
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

        [ModelDefault("Caption", "Ngày hợp đồng")]
        public DateTime NgayHopDong
        {
            get
            {
                return _NgayHopDong;
            }
            set
            {
                SetPropertyValue("NgayHopDong", ref _NgayHopDong, value);
            }
        }

        [ModelDefault("Caption", "Mức lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal MucLuong
        {
            get
            {
                if (HamDungChung.IsCreateGranted<NhanVienThongTinLuong>() == true)
                    return _MucLuong;
                else
                    return 0;
            }
            set
            {
                SetPropertyValue("MucLuong", ref _MucLuong, value);
            }
        }

        [ModelDefault("Caption", "Thưởng hiệu quả theo tháng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal ThuongHieuQuaTheoThang
        {
            get
            {
                if (HamDungChung.IsCreateGranted<NhanVienThongTinLuong>() == true)
                    return _ThuongHieuQuaTheoThang;
                else
                    return 0;
            }
            set
            {
                SetPropertyValue("ThuongHieuQuaTheoThang", ref _ThuongHieuQuaTheoThang, value);
            }
        }

        public HetHanThuViec(Session session)
            : base(session)
        { }
    }
}
