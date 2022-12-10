using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách chi tiết khối lượng giảng dạy")]
    public class dsChiTietKhoiLuong_NhanVien : BaseObject
    {
        private Guid _OidKhoiLuongGiangDay;
        private bool _Chon; 
        private string _BoPhan;
        private string _NhanVien;
        private string _MaQuanLy;
        private string _BacDaoTao;
        private string _MaHocPhan;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private decimal _HeSo_ChucDanhMonHoc;

        //
       
        [ModelDefault("Caption", "Oid KhoiLuongGiangDay")]
        [Browsable(false)]
        public Guid OidKhoiLuongGiangDay
        {
            get { return _OidKhoiLuongGiangDay; }
            set { SetPropertyValue("OidKhoiLuongGiangDay", ref _OidKhoiLuongGiangDay, value); }
        }
        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Họ tên")]
        public string NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Mã học phần")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Mã lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_ChucDanhMonHoc
        {
            get { return _HeSo_ChucDanhMonHoc; }
            set { SetPropertyValue("HeSo_ChucDanhMonHoc", ref _HeSo_ChucDanhMonHoc, value); }
        }


         public dsChiTietKhoiLuong_NhanVien(Session session)
            : base(session)
        { }
    }
}
