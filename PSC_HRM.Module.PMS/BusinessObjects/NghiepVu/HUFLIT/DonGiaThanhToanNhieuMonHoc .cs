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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.CauHinh.HeSo;

namespace PSC_HRM.Module.PMS.NghiepVu.HUFLIT
{

    [ModelDefault("Caption", "Đơn giá thanh toán từng môn học")]
    public class DonGiaThanhToanNhieuMonHoc : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private NhanVien _NhanVien;
        private LoaiMonHoc _LoaiMonHoc;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private string _MaMonhoc;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private string _LopSinhVien;
        private decimal _DonGia;

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Loại môn học")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public LoaiMonHoc LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set
            {
                SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value);
            }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "false")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
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

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set
            {
                SetPropertyValue("DonGia", ref _DonGia, value);
            }
        }
        public DonGiaThanhToanNhieuMonHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
