using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.DieuKien
{
    [NonPersistent]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Điều kiện thù lao giảng dạy")]
    public class DieuKien_ThuLaoGiangDay : BaseObject
    {
        [ModelDefault("Caption", "Chức vụ")]
        [DataSourceCriteria("PhanLoai=0 or PhanLoai=2")]
        public ChucVu ChucVu { get; set; }
        
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem { get; set; }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao { get; set; }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon { get; set; }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam { get; set; }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang { get; set; }

        public DieuKien_ThuLaoGiangDay(Session session) : base(session) { }
    }
}
