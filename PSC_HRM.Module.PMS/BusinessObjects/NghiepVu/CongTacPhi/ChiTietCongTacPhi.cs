using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{

    [ModelDefault("Caption", "Chi tiết công tác phí")]
    [Appearance("ChiTietCongTacPhi_Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1")]
    public class ChiTietCongTacPhi : BaseObject
    {

        private QuanLyCongTacPhi _QuanLyCongTacPhi;

        private NhanVien _NhanVien;
        private CoSoGiangDay _CoSoGiangDay;
        private int _LuotDi;
        private string _LopHocPhan;
        private string _GhiChu;
        private bool _Khoa;
        private bool _DaThanhToan;
        private decimal _ThanhTien;

        [ModelDefault("Caption", "QuanLyCongTacPhi")]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi")]
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

        [ModelDefault("Caption", "Lớp")]
        [Size(-1)]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Cơ sở")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value); }
        }

        [ModelDefault("Caption", "Lượt đi")]
        public int LuotDi
        {
            get { return _LuotDi; }
            set { SetPropertyValue("LuotDi", ref _LuotDi, value); }
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
        [ModelDefault("AllowEdit", "False")]
        public bool DaThanhToan
        {
            get { return _DaThanhToan; }
            set { SetPropertyValue("DaThanhToan", ref _DaThanhToan, value); }
        }
        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("AllowEdit", "False")]
        public decimal ThanhTien
        {
            get { return _ThanhTien; }
            set { SetPropertyValue("ThanhTien", ref _ThanhTien, value); }
        }
        public ChiTietCongTacPhi(Session session)
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
            if (Khoa & CoSoGiangDay != null)
                ThanhTien = CoSoGiangDay.PhiDiChuyen * LuotDi;
        }
    }
}