using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_ChungTu")]
    [ModelDefault("Caption", "Chi tiết tạm giữ lương nhân viên")]
    [NonPersistent]
    public class ChiTietTamGiuLuongNhanVien : BaseObject
    {
        private bool _Chon;
        private ChungTu _ChungTu;
        private KyTinhLuong _KyTinhLuong;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool _ChiLaiLuong;
        private decimal _ThueTNCN;
        private decimal _ThucNhan;
        private TrangThaiChiLuongEnum _TrangThai;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Chứng từ")]
        public ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }

        [ModelDefault("Caption", "Kỳ tính lương")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }
        [ModelDefault("Caption", "Thuế TNCN")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
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
        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiChiLuongEnum TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }
        public ChiTietTamGiuLuongNhanVien(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
       
    }
}
