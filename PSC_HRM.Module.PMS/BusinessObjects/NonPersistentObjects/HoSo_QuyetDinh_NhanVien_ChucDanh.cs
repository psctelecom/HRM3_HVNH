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
    [ModelDefault("Caption", "Quyết định chức danh")]
    public class HoSo_QuyetDinh_NhanVien_ChucDanh : BaseObject
    {
        private NhanVien _NhanVien;
        private ChucDanh _ChucDanh;
        private DateTime _NgayQuyetDinh;
        private string _SoQuyetDinh;

        [ModelDefault("Caption", "Giảng viên")]
        [Browsable(false)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh", ref _ChucDanh, value); }
        }

        [ModelDefault("Caption", "Ngày chuyển chức danh")]
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

        public HoSo_QuyetDinh_NhanVien_ChucDanh(Session session) : base(session) { }
        
    }
}
