using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Đảng viên")]
    public class DieuKien_DangVien : BaseObject
    {
        [ModelDefault("Caption", "Số lý lịch")]
        public static string SoLyLich { get; set; }

        [ModelDefault("Caption", "Ngày vào Đảng")]
        public DateTime NgayDuBi { get; set; }

        [ModelDefault("Caption", "Ngày vào Đảng chính thức")]
        public DateTime NgayVaoDangChinhThuc { get; set; }

        [ModelDefault("Caption", "Số thẻ Đảng")]
        public string SoTheDang { get; set; }

        [ModelDefault("Caption", "Ngày cấp thẻ")]
        public DateTime NgayCap { get; set; }

        [ModelDefault("Caption", "Nơi cấp thẻ")]
        public string NoiCapThe { get; set; }

        [ModelDefault("Caption", "Tổ chức Đảng")]
        public ToChucDang ToChucDang { get; set; }

        [ModelDefault("Caption", "Chức vụ Đảng")]
        public ChucVuDang ChucVuDang { get; set; }

        [ModelDefault("Caption", "Tuổi Đảng")]
        public int TuoiDang { get; set; }

        public DieuKien_DangVien(Session session) : base(session) { }
    }

}
