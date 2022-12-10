using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Đoàn thể")]
    public class DieuKien_DoanThe : BaseObject
    {
        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        public ChucVuDoanThe ChucVuDoanThe { get; set; }

        [ModelDefault("Caption", "Tổ chức Đoàn thể")]
        public ToChucDoanThe ToChucDoanThe { get; set; }

        [ModelDefault("Captin", "Số thẻ")]
        public string SoThe { get; set; }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh { get; set; }

        public DieuKien_DoanThe(Session session) : base(session) { }
    }

}
