using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Hợp đồng lao động")]
    public class DieuKien_HopDong : BaseObject
    {
        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDong { get; set; }

        [ModelDefault("Caption", "Ngày ký")]
        public DateTime NgayKy { get; set; }

        [ModelDefault("Caption", "Hợp đồng đã hết hạn")]
        public bool HopDongCu { get; set; }

        [ModelDefault("Caption", "Chức danh chuyên môn")]
        public string ChucDanhChuyenMon { get; set; }

        [ModelDefault("Caption", "Hình thức hợp đồng")]
        public HinhThucHopDong HinhThucHopDong { get; set; }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay { get; set; }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay { get; set; }
        
        public DieuKien_HopDong(Session session) : base(session) { }
    }

}
