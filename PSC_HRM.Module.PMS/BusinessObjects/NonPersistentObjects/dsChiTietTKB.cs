using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách định mức chức vụ")]
    public class dsChiTietTKB : BaseObject
    {
        private bool _Chon;
        private Guid _OidTKB_KhoiLuongGiangDay;
        private Guid _OidTKB_ChiTietKhoiLuongGiangDay;
        //
        private string _NhanVien;
        private string _BoPhan;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private string _BoMonGiangDay;
        private string _KhoaDaoTao;
        private int _SoTietHeThong;
        private decimal _SoTietThucDay;
        private decimal _SoTietDungLop;
        private decimal _SoTietQuyDoi;
        private decimal _HeSoChucDanh;
        private decimal _HeSoLopDong;
        private decimal _HeSoDaoTao;
        private decimal _HeSoCoSo;
        private decimal _HeSoNgoaiGio;
        private decimal _HeSoTinChi;
        private decimal _HeSoChucVu;
        private decimal _HeSoTuXa;
        
        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "OidTKB_KhoiLuongGiangDay")]
        [Browsable(false)]
        public Guid OidTKB_KhoiLuongGiangDay
        {
            get { return _OidTKB_KhoiLuongGiangDay; }
            set { SetPropertyValue("OidTKB_KhoiLuongGiangDay", ref _OidTKB_KhoiLuongGiangDay, value); }
        }

        [ModelDefault("Caption", "OidTKB_ChiTietKhoiLuongGiangDay")]
        [Browsable(false)]
        public Guid OidTKB_ChiTietKhoiLuongGiangDay
        {
            get { return _OidTKB_ChiTietKhoiLuongGiangDay; }
            set { SetPropertyValue("OidTKB_ChiTietKhoiLuongGiangDay", ref _OidTKB_ChiTietKhoiLuongGiangDay, value); }
        }

        [ModelDefault("Caption", "Nhân Viên")]
        public string NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Bộ môn giảng dạy")]
        public string BoMonGiangDay
        {
            get { return _BoMonGiangDay; }
            set { SetPropertyValue("BoMonGiangDay", ref _BoMonGiangDay, value); }
        }

        [ModelDefault("Caption", "Khóa đào tạo")]
        public string KhoaDaoTao
        {
            get { return _KhoaDaoTao; }
            set { SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value); }
        }

        [ModelDefault("Caption", "Số tiết hệ thống")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public int SoTietHeThong
        {
            get { return _SoTietHeThong; }
            set { SetPropertyValue("SoTietHeThong", ref _SoTietHeThong, value); }
        }

        [ModelDefault("Caption", "Số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietThucDay
        {
            get { return _SoTietThucDay; }
            set { SetPropertyValue("SoTietThucDay", ref _SoTietThucDay, value); }
        }

        [ModelDefault("Caption", "Số tiết đứng lớp")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietDungLop
        {
            get { return _SoTietDungLop; }
            set { SetPropertyValue("SoTietDungLop", ref _SoTietDungLop, value); }
        }

        [ModelDefault("Caption", "Số tiết quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTietQuyDoi
        {
            get { return _SoTietQuyDoi; }
            set { SetPropertyValue("_SoTietQuyDoi", ref _SoTietQuyDoi, value); }
        }

        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoChucDanh
        {
            get { return _HeSoChucDanh; }
            set { SetPropertyValue("HeSoChucDanh", ref _HeSoChucDanh, value); }
        }

        [ModelDefault("Caption", "hệ số lớp đông")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLopDong
        {
            get { return _HeSoLopDong; }
            set { SetPropertyValue("HeSoLopDong", ref _HeSoLopDong, value); }
        }

        [ModelDefault("Caption", "Hệ số đào tạo")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoDaoTao
        {
            get { return _HeSoDaoTao; }
            set { SetPropertyValue("HeSoDaoTao", ref _HeSoDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ số cơ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCoSo
        {
            get { return _HeSoCoSo; }
            set { SetPropertyValue("HeSoCoSo", ref _HeSoCoSo, value); }
        }

        [ModelDefault("Caption", "Hệ số ngoài giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoNgoaiGio
        {
            get { return _HeSoNgoaiGio; }
            set { SetPropertyValue("HeSoNgoaiGio", ref _HeSoNgoaiGio, value); }
        }

        [ModelDefault("Caption", "Hệ số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoTinChi
        {
            get { return _HeSoTinChi; }
            set { SetPropertyValue("HeSoTinChi", ref _HeSoTinChi, value); }
        }

        [ModelDefault("Caption", "Hệ số chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoChucVu
        {
            get { return _HeSoChucVu; }
            set { SetPropertyValue("HeSoChucVu", ref _HeSoChucVu, value); }
        }
        [ModelDefault("Caption", "Hệ số từ xa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoTuXa
        {
            get { return _HeSoTuXa; }
            set { SetPropertyValue("_HeSoTuXa", ref _HeSoTuXa, value); }
        }


        public dsChiTietTKB(Session session)
            : base(session)
        { }
    }
}
