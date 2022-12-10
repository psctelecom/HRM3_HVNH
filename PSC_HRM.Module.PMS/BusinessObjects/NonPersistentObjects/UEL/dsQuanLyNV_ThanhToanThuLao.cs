using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL
{
    [DefaultClassOptions]
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách bảng chốt thù lao")]
    public class dsQuanLyNV_ThanhToanThuLao : BaseObject
    {
        private Guid _Oid_ChiTiet;//Oid này chỉ lưu khi tạo mới 1 dòng 
        private bool _Chon;
        private NhanVien _NhanVien;
        //


        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Oid")]
        [Browsable(false)]
        public Guid Oid_ChiTiet
        {
            get { return _Oid_ChiTiet; }
            set { SetPropertyValue("Oid_ChiTiet", ref _Oid_ChiTiet, value); }
        }                                    
        //
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        private BoPhan _BoPhan;
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }


        private BacDaoTao _BacDaoTao;
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }


        private string _KhoanChi;
        [ModelDefault("Caption", "Khoản chi")]

        public string KhoanChi
        {
            get { return _KhoanChi; }
            set { SetPropertyValue("KhoanChi", ref _KhoanChi, value); }
        }



        private string _TenMon;
        [ModelDefault("Caption", "Tên môn")]
        public string TenMon
        {
            get { return _TenMon; }
            set { SetPropertyValue("TenMon", ref _TenMon, value); }
        }

        private string _LopHocPhan;
        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }


        private string _MaLopSinhVien;
        [ModelDefault("Caption", "Lớp sinh viên")]
        public string MaLopSinhVien
        {
            get { return _MaLopSinhVien; }
            set { SetPropertyValue("MaLopSinhVien", ref _MaLopSinhVien, value); }
        }


        private bool _CNTN_CLC;
        [ModelDefault("Caption", "CNTN _CLC")]
        public bool CNTN_CLC
        {
            get { return _CNTN_CLC; }
            set { SetPropertyValue("CNTN_CLC", ref _CNTN_CLC, value); }
        }


        private int _SiSo;
        [ModelDefault("Caption", "Sỉ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }



        private decimal _TongSoTietThucDay;

        [ModelDefault("Caption", "Tổng số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoTietThucDay
        {
            get { return _TongSoTietThucDay; }
            set { SetPropertyValue("TongSoTietThucDay", ref _TongSoTietThucDay, value); }
        }


        private decimal _GioQuyDoi;
        [ModelDefault("Caption", "Số tiết quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal GioQuyDoi
        {
            get { return _GioQuyDoi; }
            set { SetPropertyValue("GioQuyDoi", ref _GioQuyDoi, value); }
        }


        private decimal _HeSoChucDanhNhanVien;
        [ModelDefault("Caption", "HS chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoChucDanhNhanVien
        {
            get { return _HeSoChucDanhNhanVien; }
            set { SetPropertyValue("HeSoChucDanhNhanVien", ref _HeSoChucDanhNhanVien, value); }
        }


        private decimal _HeSoDiaDiem;
        [ModelDefault("Caption", "HS địa điểm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoDiaDiem
        {
            get { return _HeSoDiaDiem; }
            set { SetPropertyValue("HeSoDiaDiem", ref _HeSoDiaDiem, value); }
        }



        private decimal _HeSoLopDong;
        [ModelDefault("Caption", "HS lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLopDong
        {
            get { return _HeSoLopDong; }
            set { SetPropertyValue("HeSoLopDong", ref _HeSoLopDong, value); }
        }



        private decimal _HeSoCuNhanTN_CLC;
        [ModelDefault("Caption", "HS CNTN và CLC")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCuNhanTN_CLC
        {
            get { return _HeSoCuNhanTN_CLC; }
            set { SetPropertyValue("HeSoCuNhanTN_CLC", ref _HeSoCuNhanTN_CLC, value); }
        }


        private decimal _HeSoKhac;
        [ModelDefault("Caption", "HS Khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoKhac
        {
            get { return _HeSoKhac; }
            set { SetPropertyValue("HeSoKhac", ref _HeSoKhac, value); }
        }


        private decimal _TongHeSo;
        [ModelDefault("Caption", "Tổng hệ só")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongHeSo
        {
            get { return _TongHeSo; }
            set { SetPropertyValue("TongHeSo", ref _TongHeSo, value); }

        }




          public dsQuanLyNV_ThanhToanThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}