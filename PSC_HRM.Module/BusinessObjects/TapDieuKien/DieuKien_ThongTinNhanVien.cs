using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Thông tin nhân viên")]
    public class DieuKien_ThongTinNhanVien : BaseObject
    {
        [ModelDefault("Caption", "Số hiệu công chức")]
        public string SoHieuCongChuc { get; set; }

        [ModelDefault("Caption", "Số hồ sơ")]
        public string SoHoSo { get; set; }

        [ModelDefault("Caption", "Biên chế")]
        public bool BienChe { get; set; }

        [ModelDefault("Caption", "Tham gia giảng dạy")]
        public bool ThamGiaGiangDay { get; set; }

        [ModelDefault("Caption", "Chức vụ")]
        [DataSourceCriteria("PhanLoai=0 or PhanLoai=2")]
        public ChucVu ChucVu { get; set; }

        [ModelDefault("Caption", "Lần bổ nhiệm chức vụ")]
        public int LanBoNhiemChucVu { get; set; }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem { get; set; }

        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem { get; set; }

        [ModelDefault("Caption", "Điện thoại cơ quan")]
        public string DienThoaiCoQuan { get; set; }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public LoaiNhanVien LoaiNhanVien { get; set; }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu { get; set; }

        public DieuKien_ThongTinNhanVien(Session session) : base(session) { }
    }

}
