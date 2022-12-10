using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.NEU
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết phân công")]
    public class ChiTietPhanCong : BaseObject
    {
        private NhanVien _NhanVien;
        private string _BoMon;
        private string _TenMonHoc;
        private string _MaMonHoc;
        private string _LopHocPhan;
        private Guid _OidChiTiet;

        [ModelDefault("Caption", "Giảng viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Bộ môn")]
        [ModelDefault("AllowEdit","false")]
        public string BoMon
        {
            get { return _BoMon; }
            set { SetPropertyValue("BoMon", ref _BoMon, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        [ModelDefault("AllowEdit", "false")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Mã môn học")]
        [ModelDefault("AllowEdit", "false")]
        public string MaMonHoc
        {
            get { return _MaMonHoc; }
            set { SetPropertyValue("MaMonHoc", ref _MaMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Oid")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "false")]
        public Guid OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }
        public ChiTietPhanCong(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}