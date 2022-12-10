using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Đoàn viên")]
    public class DieuKien_DoanVien : BaseObject
    {
        [ModelDefault("Caption", "Số thẻ đoàn")]
        public string SoTheDoan { get; set; }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap { get; set; }

        [ModelDefault("Caption", "Ngày kết nạp")]
        public DateTime NgayKetNap { get; set; }

        [ModelDefault("Caption", "Chức vụ Đoàn")]
        public ChucVuDoan ChucVuDoan { get; set; }

        [ModelDefault("Caption", "Tổ chức Đoàn")]
        public ToChucDoan ToChucDoan { get; set; }

        [ModelDefault("Caption", "Trưởng thành Đoàn")]
        public bool TruongThanhDoan { get; set; }

        public DieuKien_DoanVien(Session session) : base(session) { }
    }

}
