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
    [ModelDefault("Caption", "Quyết định chức vụ")]
    public class HoSo_QuyetDinh_NhanVien_ChucVu : BaseObject
    {
        private NhanVien _NhanVien;
        private ChucVu _ChucVu;
        private DateTime _NgayQuyetDinh;
        private DateTime _NgayHetHieuLuc;
        private string _SoQuyetDinh;

        [ModelDefault("Caption", "Giảng viên")]
        [Browsable(false)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }

        [ModelDefault("Caption", "Ngày chuyển chức vụ")]
        public DateTime NgayQuyetDinh
        {
            get { return _NgayQuyetDinh; }
            set { SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value); }
        }

        [ModelDefault("Caption", "Ngày hết hiệu lực")]
        public DateTime NgayHetHieuLuc
        {
            get { return _NgayHetHieuLuc; }
            set { SetPropertyValue("NgayHetHieuLuc", ref _NgayHetHieuLuc, value); }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get { return _SoQuyetDinh; }
            set { SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value); }
        }

        public HoSo_QuyetDinh_NhanVien_ChucVu(Session session) : base(session) { }
        
    }
}
