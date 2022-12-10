using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Quyết định học hàm")]
    public class HoSo_QuyetDinh_NhanVien_HocHam : BaseObject
    {
        private NhanVien _NhanVien;
        private HocHam _HocHam;
        private DateTime _NgayCongNhan;

        [ModelDefault("Caption", "Giảng viên")]
        [Browsable(false)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Ngày công nhận học hàm")]
        public DateTime NgayCongNhan
        {
            get { return _NgayCongNhan; }
            set { SetPropertyValue("NgayCongNhan", ref _NgayCongNhan, value); }
        }

        public HoSo_QuyetDinh_NhanVien_HocHam(Session session) : base(session) { }
    }
}
