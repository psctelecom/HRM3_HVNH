using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhGia;
using System.ComponentModel;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("list1")]
    [ModelDefault("Caption", "Đánh giá cán bộ")]
    public class DanhGiaCanBo : BaseObject
    {
        private QuanLyDanhGiaCanBo _QuanLyDanhGiaCanBo;
        private bool _KhoaSo;
        private DateTime _ThangNam;

        public DanhGiaCanBo(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đánh giá cán bộ")]
        [Association("QuanLyDanhGiaCanBo-ListDanhGiaCanBo")]
        public QuanLyDanhGiaCanBo QuanLyDanhGiaCanBo
        {
            get
            {
                return _QuanLyDanhGiaCanBo;
            }
            set
            {
                SetPropertyValue("QuanLyDanhGiaCanBo", ref _QuanLyDanhGiaCanBo, value);
            }
        }

        [ModelDefault("Caption", "Tháng năm"),
        Custom("DisplayFormat", "MM/yyyy"),
        Custom("EditMask", "MM/yyyy")]
        public DateTime ThangNam
        {
            get
            {
                return _ThangNam;
            }
            set
            {
                SetPropertyValue("ThangNam", ref _ThangNam, value);
            }
        }

        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đánh giá lần 1")]
        [Association("DanhGiaCanBo-DanhSachDanhGiaLan1")]
        public XPCollection<DanhGiaLan1> DanhSachDanhGiaLan1
        {
            get
            {
                return GetCollection<DanhGiaLan1>("DanhSachDanhGiaLan1");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đánh giá lần 2")]
        [Association("DanhGiaCanBo-DanhSachDanhGiaLan2")]
        public XPCollection<DanhGiaLan2> DanhSachDanhGiaLan2
        {
            get
            {
                return GetCollection<DanhGiaLan2>("DanhSachDanhGiaLan2");
            }
        }

        public bool IsExists(ThongTinNhanVien nhanVien)
        {
            foreach (DanhGiaLan1 item in DanhSachDanhGiaLan1)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }
    }

}
