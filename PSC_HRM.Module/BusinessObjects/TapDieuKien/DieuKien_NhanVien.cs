using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Sơ yếu lý lịch 2")]
    public class DieuKien_NhanVien : BaseObject
    {        
        [ModelDefault("Caption", "Thành phần gia đình")]
        public ThanhPhanXuatThan ThanhPhanXuatThan { get; set; }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh { get; set; }

        [ModelDefault("Caption", "Ưu tiên của bản thân")]
        public UuTienBanThan UuTienBanThan { get; set; }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan { get; set; }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh { get; set; }

        [ModelDefault("Caption", "Công việc được giao")]
        public CongViec CongViecDuocGiao { get; set; }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay { get; set; }

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public DateTime NgayVaoCoQuan { get; set; }

        [ModelDefault("Caption", "Ngày vào ngành")]
        public DateTime NgayVaoNganhGiaoDuc { get; set; }

        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung { get; set; }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang { get; set; }

        [ModelDefault("Caption", "Thời gian công tác (tháng)")]
        public int ThoiGianCongTac { get; set; }

        public DieuKien_NhanVien(Session session) : base(session) { }
    }

}
