using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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
    [ModelDefault("Caption", "Quyết định công tác")]
    public class HoSo_QuyetDinh_NhanVien_CongTac : BaseObject
    {
        private NhanVien _NhanVien;
        private LoaiNhanVien _LoaiNhanVien;
        private DateTime _NgayQuyetDinh;
        private string _SoQuyetDinh;

        [ModelDefault("Caption", "Giảng viên")]
        [Browsable(false)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Loại nhân viên")]
        public LoaiNhanVien LoaiNhanVien
        {
            get { return _LoaiNhanVien; }
            set { SetPropertyValue("LoaiNhanVien", ref _LoaiNhanVien, value); }
        }

        [ModelDefault("Caption", "Ngày chuyển công tác")]
        public DateTime NgayQuyetDinh
        {
            get { return _NgayQuyetDinh; }
            set { SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value); }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get { return _SoQuyetDinh; }
            set { SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value); }
        }

        public HoSo_QuyetDinh_NhanVien_CongTac(Session session) : base(session) { }
        
    }
}
