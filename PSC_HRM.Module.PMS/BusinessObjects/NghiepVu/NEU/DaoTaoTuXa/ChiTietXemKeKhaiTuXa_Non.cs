using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa
{
    [ModelDefault("Caption", "Chi tiết kê khai NonPersistent")]
    [NonPersistent]
    public class ChiTietXemKeKhaiTuXa_Non : BaseObject
    {
        private bool _Chon;

        #region Khai báo Master
        private string _BoPhan;
        private string _NhanVien;
        private string _TenMonHoc;
        private string _LopMonHoc;
        private string _BoMonQuanLy;
        private string _OidChiTiet;
        private string _BacDaoTao;
        private string _HeDaoTao;
        #endregion

        #region ChiTiet

        private int _SoBaiKiemTra;
        private int _SoBaiTieuLuan;
        private int _SoTraLoiCauHoiTrenHeThongHocTap;
        private int _SoTruyCapLopHoc;
        private string _LoaiHuongDan;
        private int _SoLuongHuongDan;
        private bool _XacNhan;

        [ModelDefault("Caption", "Chọn xóa")]       
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }


        #endregion
        #region Master
        [ModelDefault("Caption","Đơn vị")]
        //[ModelDefault("AllowEdit","False")]
        public string BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        //[ModelDefault("AllowEdit", "False")]
        public string NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Tên môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp môn học")]
        //[ModelDefault("AllowEdit", "False")]
        public string LopMonHoc
        {
            get { return _LopMonHoc; }
            set { SetPropertyValue("LopMonHoc", ref _LopMonHoc, value); }
        }
        [ModelDefault("Caption", "Bộ môn quản lý")]
        //[ModelDefault("AllowEdit", "False")]
        public string BoMonQuanLy
        {
            get { return _BoMonQuanLy; }
            set { SetPropertyValue("BoMonQuanLy", ref _BoMonQuanLy, value); }
        }


        [ModelDefault("Caption", "Bậc đào tạo")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        public string HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }


        [Browsable(false)]
        public string OidChiTiet
        {
            get { return _OidChiTiet; }
            set { SetPropertyValue("OidChiTiet", ref _OidChiTiet, value); }
        }
        #endregion

        #region chitiet

        [ModelDefault("Caption","Số bài kiểm tra")]
        public int SoBaiKiemTra
        {
            get { return _SoBaiKiemTra; }
            set { SetPropertyValue("SoBaiKiemTra", ref _SoBaiKiemTra, value); }
        }

        [ModelDefault("Caption", "Số bài tiểu luận")]
        public int SoBaiTieuLuan
        {
            get { return _SoBaiTieuLuan; }
            set { SetPropertyValue("SoBaiTieuLuan", ref _SoBaiTieuLuan, value); }
        }

        [ModelDefault("Caption", "Số trả lời câu hỏi trên hệ thống học tập")]
        public int SoTraLoiCauHoiTrenHeThongHocTap
        {
            get { return _SoTraLoiCauHoiTrenHeThongHocTap; }
            set { SetPropertyValue("SoTraLoiCauHoiTrenHeThongHocTap", ref _SoTraLoiCauHoiTrenHeThongHocTap, value); }
        }
        [ModelDefault("Caption", "Chấm điểm câu trả lời của sinh viên trên diễn đàn")]
        public int SoTruyCapLopHoc
        {
            get { return _SoTruyCapLopHoc; }
            set { SetPropertyValue("SoTruyCapLopHoc", ref _SoTruyCapLopHoc, value); }
        }
        [ModelDefault("Caption", "Loại hướng dẫn")]
        public string LoaiHuongDan
        {
            get { return _LoaiHuongDan; }
            set { SetPropertyValue("LoaiHuongDan", ref _LoaiHuongDan, value); }
        }
        [ModelDefault("Caption", "Số lượng hướng dẫn")]
        public int SoLuongHuongDan
        {
            get { return _SoLuongHuongDan; }
            set { SetPropertyValue("SoLuongHuongDan", ref _SoLuongHuongDan, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }


        #endregion
        public ChiTietXemKeKhaiTuXa_Non(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}