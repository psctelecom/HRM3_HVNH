using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class ChiTietKhaiBao_KhoiLuongGiangDay : BaseObject
    {
        private bool _Chon;
        private Guid _OidNhanVien;
        private string _HoTen;
        private string _DK;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [Browsable(false)]
        public Guid OidNhanVien
        {
            get { return _OidNhanVien; }
            set { SetPropertyValue("OidNhanVien", ref _OidNhanVien, value); }
        }
        [ModelDefault("Caption", "Họ tên")]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }
        private decimal _ThamQuan;
        private decimal _DiHoc;
        private decimal _KiemNhiem;
        private decimal _ConNho;
        private decimal _NghienCuuKhoaHoc;
        private decimal _Khac;

        [ModelDefault("Caption", "Tham quan")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ThamQuan
        {
            get
            {
                return _ThamQuan;
            }
            set
            {
                SetPropertyValue("ThamQuan", ref _ThamQuan, value);
            }
        }

        [ModelDefault("Caption", "Đi học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DiHoc
        {
            get
            {
                return _DiHoc;
            }
            set
            {
                SetPropertyValue("DiHoc", ref _DiHoc, value);
            }
        }
        [ModelDefault("Caption", "Kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal KiemNhiem
        {
            get
            {
                return _KiemNhiem;
            }
            set
            {
                SetPropertyValue("KiemNhiem", ref _KiemNhiem, value);
            }
        }
        [ModelDefault("Caption", "Con nhỏ, phụ tá TN, ...")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ConNho
        {
            get
            {
                return _ConNho;
            }
            set
            {
                SetPropertyValue("ConNho", ref _ConNho, value);
            }
        }
        [ModelDefault("Caption", "Nghiên cứu KH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal NghienCuuKhoaHoc
        {
            get
            {
                return _NghienCuuKhoaHoc;
            }
            set
            {
                SetPropertyValue("NghienCuuKhoaHoc", ref _NghienCuuKhoaHoc, value);
            }
        }
        [ModelDefault("Caption", "Khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal Khac
        {
            get
            {
                return _Khac;
            }
            set
            {
                SetPropertyValue("Khac", ref _Khac, value);
            }
        }

        [ModelDefault("Caption", "Điều kiện")]
        [Browsable(false)]
        public string DK
        {
            get { return _DK; }
            set { SetPropertyValue("DK", ref _DK, value); }
        }

        public ChiTietKhaiBao_KhoiLuongGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            DK = TruongConfig.MaTruong;
        }
    }

}