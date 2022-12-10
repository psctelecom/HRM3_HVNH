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
    [ModelDefault("Caption", "Danh sách thời khóa biểu")]
    public class dsChiTietTKB_KhoaDuLieu : BaseObject
    {
        private bool _Chon;
        private Guid _OidTKB_KhoiLuongGiangDay;
        private Guid _OidTKB_ChiTietKhoiLuongGiangDay;
        //
        private string _NhanVien;
        private string _BoPhan;
        private string _TenMonHoc;
        private string _MaLopHocPhan;
        private string _LopHocPhan;
        private string _TenLopSV;
        private string _KhoaDaoTao;
        

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
        [ModelDefault("Caption", "Mã lớp HP")]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }
        [ModelDefault("Caption", "Khóa đào tạo")]
        public string KhoaDaoTao
        {
            get { return _KhoaDaoTao; }
            set { SetPropertyValue("KhoaDaoTao", ref _KhoaDaoTao, value); }
        }
        public dsChiTietTKB_KhoaDuLieu(Session session)
            : base(session)
        { }
    }
}
