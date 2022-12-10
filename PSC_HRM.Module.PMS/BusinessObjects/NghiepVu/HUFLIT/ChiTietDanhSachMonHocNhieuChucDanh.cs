using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NghiepVu.HUFLIT
{

    [ModelDefault("Caption", "Chi tiết môn học nhiều chức danh")]
    [NonPersistent]
    [DefaultProperty("Caption")]
    public class ChiTietDanhSachMonHocNhieuChucDanh : BaseObject
    {
        private bool _Chon;
        private Guid _NhanVien;
        private string _TenNhanVien;
        private Guid _LoaiMonHoc;
        private string _TenLoaiMonHoc;
        private Guid _BacDaoTao;
        private string _TenBacDaoTao;
        private Guid _HeDaoTao;
        private string _TenHeDaoTao;
        private string _MaMonhoc;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private string _LopSinhVien;
        private decimal _HeSo;

        [ModelDefault("Caption", "Chọn")]
        [ImmediatePostData]
        public bool Chon
        {
            get { return _Chon; }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        [Browsable(false)]
        public Guid NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string TenNhanVien
        {
            get { return _TenNhanVien; }
            set
            {
                SetPropertyValue("TenNhanVien", ref _TenNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Loại môn học")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        [Browsable(false)]
        public Guid LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set
            {
                SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value);
            }
        }

        [ModelDefault("Caption", "Loại môn học")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string TenLoaiMonHoc
        {
            get { return _TenLoaiMonHoc; }
            set
            {
                SetPropertyValue("TenLoaiMonHoc", ref _TenLoaiMonHoc, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "false")]
        public Guid BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string TenBacDaoTao
        {
            get { return _TenBacDaoTao; }
            set
            {
                SetPropertyValue("TenBacDaoTao", ref _TenBacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public Guid HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string TenHeDaoTao
        {
            get { return _TenHeDaoTao; }
            set
            {
                SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Mã môn học")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string MaMonhoc
        {
            get { return _MaMonhoc; }
            set
            {
                SetPropertyValue("MaMonhoc", ref _MaMonhoc, value);
            }
        }

        [ModelDefault("Caption", "Tên môn học")]
        [Size(-1)]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set
            {
                SetPropertyValue("TenMonHoc", ref _TenMonHoc, value);
            }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set
            {
                SetPropertyValue("LopHocPhan", ref _LopHocPhan, value);
            }
        }

        [ModelDefault("Caption", "Lớp sinh viên")]
        [Size(-1)]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set
            {
                SetPropertyValue("LopSinhVien", ref _LopSinhVien, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        public ChiTietDanhSachMonHocNhieuChucDanh(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}