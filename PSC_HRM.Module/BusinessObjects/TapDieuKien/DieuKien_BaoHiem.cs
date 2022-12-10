using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảo hiểm")]
    public class DieuKien_BaoHiem : BaseObject
    {
        [ModelDefault("Caption", "Số sổ BHXH")]
        public string SoSoBHXH { get; set; }

        [ModelDefault("Caption", "Ngày tham gia BHXH")]
        public DateTime NgayThamGiaBHXH { get; set; }

        [ModelDefault("Caption", "Số thẻ BHYT")]
        public string SoTheBHYT { get; set; }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay { get; set; }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay { get; set; }

        [ModelDefault("Caption", "Nơi đăng ký khám chữa bệnh")]
        public BenhVien NoiDangKyKhamChuaBenh { get; set; }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT { get; set; }

        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN { get; set; }

        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiThamGiaBaoHiemEnum TrangThai { get; set; }

        public DieuKien_BaoHiem(Session session) : base(session) { }
    }

}
