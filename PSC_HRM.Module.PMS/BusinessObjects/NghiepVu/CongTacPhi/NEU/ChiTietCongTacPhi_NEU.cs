using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.PMS.NghiepVu.NEU.VHVL;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{

    [ModelDefault("Caption", "Chi tiết công tác phí_NEU")]
    [Appearance("ChiTietCongTacPhi_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    public class ChiTietCongTacPhi_NEU : BaseObject
    {

        private QuanLyCongTacPhi _QuanLyCongTacPhi;

        private NhanVien _NhanVien;
        private DiaDiemGiangDayLienKet _DiaDiemGiangDayLienKet;
        private string _MaLopHocPhan;
        private string _LopHocPhan;
        private string _GhiChu;
        private bool _Khoa;
        private Guid _OidThoiKhoaBieu;
        private decimal _ThanhTien;

        [ModelDefault("Caption", "QuanLyCongTacPhi")]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi_NEU")]
        [Browsable(false)]
        public QuanLyCongTacPhi QuanLyCongTacPhi
        {
            get { return _QuanLyCongTacPhi; }
            set { SetPropertyValue("QuanLyCongTacPhi", ref _QuanLyCongTacPhi, value); }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Mã lớp học phần")]
        [Size(-1)]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Địa điểm")]
        public DiaDiemGiangDayLienKet DiaDiemGiangDayLienKet
        {
            get { return _DiaDiemGiangDayLienKet; }
            set { SetPropertyValue("DiaDiemGiangDayLienKet", ref _DiaDiemGiangDayLienKet, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set
            {
                SetPropertyValue("Khoa", ref _Khoa, value);
                if (!IsLoading)
                    TinhTien();
            }
        }
        [ModelDefault("Caption", "Đã thanh toán")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public Guid OidThoiKhoaBieu
        {
            get { return _OidThoiKhoaBieu; }
            set { SetPropertyValue("OidThoiKhoaBieu", ref _OidThoiKhoaBieu, value); }
        }
        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }
        public ChiTietCongTacPhi_NEU(Session session)
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
        public void TinhTien()
        {
            if (Khoa & DiaDiemGiangDayLienKet != null)
                ThanhTien = DiaDiemGiangDayLienKet.DonGia ;
        }
    }
}