using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.Enum;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.ThanhToan
{
    [NonPersistent]
    [ModelDefault("Caption","Thanh toán đợt tổng kết")]
    public class dsChiTietThanhToanThuLaoBoSung : BaseObject
    {
        private Chon_ThanhToanBoSung _Key;
        [Browsable(false)]
        public Chon_ThanhToanBoSung Key
        {
            get { return _Key; }
            set { SetPropertyValue("Key", ref _Key, value); }
        }
        private bool _Chon;
        private Guid _OidChiTietBangChotThuLaoGiangDay;
        private string _DonVi;
        private string _HoTen;
        private string _HoatDong;
        private string _TenMonHoc;
        private string _LopHocPhan;
        private string _BacDaoTao;
        private decimal _TongGioA1;
        private decimal _TongGioA2;
        private decimal _TongGio;
        private decimal _SoTienThanhToan;
        private CongTruPMSEnum _CongTru = 0;
        private string _GhiChu;


        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [Browsable(false)]
        public Guid OidChiTietBangChotThuLaoGiangDay
        {
            get { return _OidChiTietBangChotThuLaoGiangDay; }
            set { SetPropertyValue("OidChiTietBangChotThuLaoGiangDay", ref _OidChiTietBangChotThuLaoGiangDay, value); }
        }
        [ModelDefault("Caption", "Đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public string DonVi
        {
            get { return _DonVi; }
            set { SetPropertyValue("DonVi", ref _DonVi, value); }
        }
        [ModelDefault("Caption", "Họ tên")]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }
        [ModelDefault("Caption", "Hoạt động")]
        [ModelDefault("AllowEdit","False")]
        public string HoatDong
        {
            get { return _HoatDong; }
            set { SetPropertyValue("HoatDong", ref _HoatDong, value); }
        }

        [ModelDefault("Caption", "Môn học")]
        [ModelDefault("AllowEdit", "False")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public string BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Tổng giờ A1")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA1
        {
            get { return _TongGioA1; }
            set
            {
                SetPropertyValue("TongGioA1", ref _TongGioA1, value); if (!IsLoading)
                {
                    TinhTongGio();
                }
            }
        }

        [ModelDefault("Caption", "Tổng giờ A2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioA2
        {
            get { return _TongGioA2; }
            set
            {
                SetPropertyValue("TongGioA2", ref _TongGioA2, value);
                if (!IsLoading)
                {
                    TinhTongGio();
                }
            }
        }

        [ModelDefault("Caption", "Tổng giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGio
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Cộng trừ")]
        public CongTruPMSEnum CongTru
        {
            get { return _CongTru; }
            set { SetPropertyValue("CongTru", ref _CongTru, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public dsChiTietThanhToanThuLaoBoSung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            GhiChu = "Thanh toán bổ sung";
        }
        void TinhTongGio()
        {
            TongGio = TongGioA1 + TongGioA2;
            if (TongGio != 0)
                Chon = true;
            else
                Chon = false;
            if (Key != null && Key.NamHoc != null)
            {
                CauHinhQuyDoiPMS CauHinh = Session.FindObject<CauHinhQuyDoiPMS>(CriteriaOperator.Parse("NamHoc =?", Key.NamHoc.Oid));
                if (CauHinh != null)
                    SoTienThanhToan = TongGio * CauHinh.DonGiaThanhToanVuotMuc;
            }
        }
    }

}