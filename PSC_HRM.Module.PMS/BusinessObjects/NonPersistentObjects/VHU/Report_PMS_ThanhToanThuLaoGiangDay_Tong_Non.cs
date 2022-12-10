using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non : BaseObject
    {
        private bool _Chon;
        private string _MaGV;
        private string _DonVi;
        private string _HoTen;
        private decimal _DonGia;
        private decimal _ThanhTien;
        private decimal _ThueTNCN;
        private decimal _ThucNhan;
        private string _MaSoThue;
        private string _SoTaiKhoan;
        private string _NganHang;
        private string _TenNamhoc;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption","Mã giảng viên")]
        [ModelDefault("AllowEdit", "False")]
        public string MaGV
        {
            get { return _MaGV; }
            set { SetPropertyValue("MaGV", ref _MaGV, value); }
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

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get
            {
                return _DonGia;
            }
            set
            {
                SetPropertyValue("DonGia", ref _DonGia, value);
            }
        }

        [ModelDefault("Caption", "Thành tiền")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThanhTien
        {
            get
            {
                return _ThanhTien;
            }
            set
            {
                SetPropertyValue("ThanhTien", ref _ThanhTien, value);
            }
        }

        [ModelDefault("Caption", "Thuế TNCN 10%")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThueTNCN
        {
            get
            {
                return _ThueTNCN;
            }
            set
            {
                SetPropertyValue("ThueTNCN", ref _ThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Thực nhận")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThucNhan
        {
            get
            {
                return _ThucNhan;
            }
            set
            {
                SetPropertyValue("ThucNhan", ref _ThucNhan, value);
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        [ModelDefault("AllowEdit", "False")]
        public string MaSoThue
        {
            get { return _MaSoThue; }
            set { SetPropertyValue("MaSoThue", ref _MaSoThue, value); }
        }

        [ModelDefault("Caption", "Số tài khoản")]
        [ModelDefault("AllowEdit", "False")]
        public string SoTaiKhoan
        {
            get { return _SoTaiKhoan; }
            set { SetPropertyValue("SoTaiKhoan", ref _SoTaiKhoan, value); }
        }

        [ModelDefault("Caption", "Ngân hàng")]
        [ModelDefault("AllowEdit", "False")]
        public string NganHang
        {
            get { return _NganHang; }
            set { SetPropertyValue("NganHang", ref _NganHang, value); }
        }

        [ModelDefault("Caption", "Tên Năm Học")]
        [Browsable(false)]
        [ModelDefault("AllowEdit", "False")]
        public string TenNamhoc
        {
            get { return _TenNamhoc; }
            set { SetPropertyValue("TenNamhoc", ref _TenNamhoc, value); }
        }
        public Report_PMS_ThanhToanThuLaoGiangDay_Tong_Non(Session session)
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
    }

}