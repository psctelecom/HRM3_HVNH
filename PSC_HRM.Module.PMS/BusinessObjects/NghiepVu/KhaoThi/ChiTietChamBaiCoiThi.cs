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


namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{

    [ModelDefault("Caption", "Chi tiết chấm bài - coi thi")]
    public class ChiTietChamBaiCoiThi : BaseObject
    {
        #region key
        private QuanLyKhaoThi _QuanLyKhaoThi;
        [Association("QuanLyKhaoThi-ListChiTietChamBaiCoiThi")]
        [ModelDefault("Caption", "Quản lý chấm bài - coi thi")]
        [Browsable(false)]
        public QuanLyKhaoThi QuanLyKhaoThi
        {
            get
            {
                return _QuanLyKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanLyKhaoThi", ref _QuanLyKhaoThi, value);
            }
        }
        #endregion

        #region KhaiBao
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private decimal _SoBaiGiuaKy;
        private decimal _SoLuongDeThi;
        private decimal _SoCaCoiThi;
        private decimal _SoBaiChamTuLuan;
        private decimal _SoBaiChamVDTHTin;
        private decimal _SoBaiChamGDTCQP;
        private decimal _SoBaiChamTieuLuan;
        private decimal _TongGioQuyDoi;
        private string _TenHocKy;
        private BoPhan _DonViKeKhai;
        private CoSoGiangDay _CoSo;
        #endregion

        [ModelDefault("Caption","Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
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
        [ModelDefault("Caption", "Số bài chấm giữa kỳ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiGiuaKy
        {
            get { return _SoBaiGiuaKy; }
            set { SetPropertyValue("SoBaiGiuaKy", ref _SoBaiGiuaKy, value); }
        }

        [ModelDefault("Caption", "Số lượng đề thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoLuongDeThi
        {
            get { return _SoLuongDeThi; }
            set { SetPropertyValue("SoLuongDeThi", ref _SoLuongDeThi, value); }
        }

        [ModelDefault("Caption", "Số ca coi thi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoCaCoiThi
        {
            get { return _SoCaCoiThi; }
            set { SetPropertyValue("SoCaCoiThi", ref _SoCaCoiThi, value); }
        }
        [ModelDefault("Caption", "Số bài chấm tự luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiChamTuLuan
        {
            get { return _SoBaiChamTuLuan; }
            set { SetPropertyValue("SoBaiChamTuLuan", ref _SoBaiChamTuLuan, value); }
        }
        [ModelDefault("Caption", "Số bài chấm VĐ, TH Tin")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiChamVDTHTin
        {
            get { return _SoBaiChamVDTHTin; }
            set { SetPropertyValue("SoBaiChamVDTHTin", ref _SoBaiChamVDTHTin, value); }
        }
        [ModelDefault("Caption", "Số bài chấm GDTC-QP")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiChamGDTCQP
        {
            get { return _SoBaiChamGDTCQP; }
            set { SetPropertyValue("SoBaiChamGDTCQP", ref _SoBaiChamGDTCQP, value); }
        }
        [ModelDefault("Caption", "Số bài chấm tiểu luận")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBaiChamTieuLuan
        {
            get { return _SoBaiChamTieuLuan; }
            set { SetPropertyValue("SoBaiChamTieuLuan", ref _SoBaiChamTieuLuan, value); }
        }
        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }
        [ModelDefault("Caption", "Đơn vị kê khai")]
        public BoPhan DonViKeKhai
        {
            get { return _DonViKeKhai; }
            set { SetPropertyValue("DonViKeKhai", ref _DonViKeKhai, value); }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [ModelDefault("AllowEdit","False")]
        public string TenHocKy
        {
            get { return _TenHocKy; }
            set { SetPropertyValue("TenHocKy", ref _TenHocKy, value); }
        }

        public ChiTietChamBaiCoiThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
