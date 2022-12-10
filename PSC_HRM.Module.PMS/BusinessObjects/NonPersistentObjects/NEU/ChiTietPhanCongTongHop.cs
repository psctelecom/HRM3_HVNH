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
    public class ChiTietPhanCongTongHop : BaseObject
    {
        private Guid _Oid;
        [Browsable(false)]
        [ModelDefault("Caption", "Oid")]
        public Guid Oid
        {
            get { return _Oid; }
            set { _Oid = value; }
        }
        private string _MaMonHoc;
        [ModelDefault("Caption", "Mã môn học")]
        public string MaMonHoc
        {
            get { return _MaMonHoc; }
            set { _MaMonHoc = value; }
        }
        private string _TenMonHoc;
        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { _TenMonHoc = value; }
        }
        private string _MaLopHocPhan;
        [ModelDefault("Caption", "Mã lớp học phần")]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { _MaLopHocPhan = value; }
        }
        private string _LopHocPhan;
        [ModelDefault("Caption", "Tên lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { _LopHocPhan = value; }
        }
        private string _MaLopSV;
        [ModelDefault("Caption", "Mã lớp sinh viên")]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { _MaLopSV = value; }
        }
        private string _TenLopSV;
        [ModelDefault("Caption", "Tên lớp sinh viên")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { _TenLopSV = value; }
        }
        private string _SiSo;
        [ModelDefault("Caption", "Sỉ số")]
        public string SiSo
        {
            get { return _SiSo; }
            set { _SiSo = value; }
        }
        private string _HeDaoTao;
        [ModelDefault("Caption", "Hệ đào tạo")]
        public string HeDaoTao
        {
            get { return _HeDaoTao; }
            set { _HeDaoTao = value; }
        }
        private string _BacDaoTao;
        [ModelDefault("Caption", "Bậc đào tạo")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { _BacDaoTao = value; }
        }
        private string _MaBoPhan;
        [ModelDefault("Caption", "Mã bộ phận")]
        public string MaBoPhan
        {
            get { return _MaBoPhan; }
            set { _MaBoPhan = value; }
        }
        private string _MaGV;
        [ModelDefault("Caption", "Mã GV")]
        public string MaGV
        {
            get { return _MaGV; }
            set { _MaGV = value; }
        }
        private string _TenGV;
        [ModelDefault("Caption", "Tên GV")]
        public string TenGV
        {
            get { return _TenGV; }
            set { _TenGV = value; }
        }
        private string _SoTietHeThong;
        [ModelDefault("Caption", "Số tiết hệ thống")]
        public string SoTietHeThong
        {
            get { return _SoTietHeThong; }
            set { _SoTietHeThong = value; }
        }
        private string _SoTietDungLop;
        [ModelDefault("Caption", "Số tiết đứng lớp")]
        public string SoTietDungLop
        {
            get { return _SoTietDungLop; }
            set { _SoTietDungLop = value; }
        }
        private string _SoTinChi;
        [ModelDefault("Caption", "Số tín chỉ")]
        public string SoTinChi
        {
            get { return _SoTinChi; }
            set { _SoTinChi = value; }
        }
        private string _TrangThai;
        [ModelDefault("Caption", "Trạng thái")]
        public string TrangThai
        {
            get { return _TrangThai; }
            set { _TrangThai = value; }
        }

        private string _KhoaHoc;
        [ModelDefault("Caption", "Khóa học")]
        public string KhoaHoc
        {
            get { return _KhoaHoc; }
            set { _KhoaHoc = value; }
        }

        public ChiTietPhanCongTongHop(Session session)
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