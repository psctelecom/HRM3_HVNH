using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("SoSoBHXH")]
    [ModelDefault("Caption", "Tăng lao động")]
    public class BienDong_TangLaoDong : BienDong
    {
        // Fields...
        private decimal _PCK;
        private decimal _TNGD;
        private int _TNVK;
        private decimal _PCCV;
        private decimal _TienLuong;
        private bool _KhongThamGiaBHTN;
        private QuyenLoiHuongBHYT _QuyenLoiHuongBHYT;
        private BenhVien _NoiDangKyKhamChuaBenh;
        private DateTime _DenThang;
        private DateTime _TuThang;
        private string _SoTheBHYT;
        private string _SoSoBHXH;

        [ModelDefault("Caption", "Số sổ BHXH")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SoSoBHXH
        {
            get
            {
                return _SoSoBHXH;
            }
            set
            {
                SetPropertyValue("SoSoBHXH", ref _SoSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Số thẻ BHYT")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SoTheBHYT
        {
            get
            {
                return _SoTheBHYT;
            }
            set
            {
                SetPropertyValue("SoTheBHYT", ref _SoTheBHYT, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng, năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }

        [ModelDefault("Caption", "Đến tháng, năm")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [ModelDefault("Caption", "Nơi đăng ký khám chữa bệnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BenhVien NoiDangKyKhamChuaBenh
        {
            get
            {
                return _NoiDangKyKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("NoiDangKyKhamChuaBenh", ref _NoiDangKyKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT
        {
            get
            {
                return _QuyenLoiHuongBHYT;
            }
            set
            {
                SetPropertyValue("QuyenLoiHuongBHYT", ref _QuyenLoiHuongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN
        {
            get
            {
                return _KhongThamGiaBHTN;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHTN", ref _KhongThamGiaBHTN, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCCV
        {
            get
            {
                return _PCCV;
            }
            set
            {
                SetPropertyValue("PCCV", ref _PCCV, value);
            }
        }

        [ModelDefault("Caption", "% Vượt khung")]
        public int TNVK
        {
            get
            {
                return _TNVK;
            }
            set
            {
                SetPropertyValue("TNVK", ref _TNVK, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% Thâm niên")]
        public decimal TNGD
        {
            get
            {
                return _TNGD;
            }
            set
            {
                SetPropertyValue("TNGD", ref _TNGD, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PCK
        {
            get
            {
                return _PCK;
            }
            set
            {
                SetPropertyValue("PCK", ref _PCK, value);
            }
        }

        public BienDong_TangLaoDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiBienDong = "Tăng lao động";
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            if (ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai != ThongTinLuongEnum.LuongHeSo)
                TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan;
            else
                TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong ? ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong * 0.85m : ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            PCCV = ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu;
            TNVK = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            TNGD = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
            PCK = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //tạo quá trình tham gia BHXH
                QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, TuNgay));
                if (qtBHXH == null)
                {
                    qtBHXH = new QuaTrinhThamGiaBHXH(Session);
                }
                qtBHXH.HoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
                qtBHXH.TuNam = TuNgay;
                qtBHXH.DenNam = DenNgay;
                qtBHXH.KhongThamGiaBHTN = KhongThamGiaBHTN;
                qtBHXH.KhongThamGiaBHYT = String.IsNullOrEmpty(SoTheBHYT) ? true : false;
            }
        }

        protected override void OnDeleting()
        {
            QuaTrinhThamGiaBHXH qtBHXH = Session.FindObject<QuaTrinhThamGiaBHXH>(CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, TuNgay));
            if (qtBHXH != null)
            {
                Session.Delete(qtBHXH);
                Session.Save(qtBHXH);
            }

            base.OnDeleting();
        }
    }

}
