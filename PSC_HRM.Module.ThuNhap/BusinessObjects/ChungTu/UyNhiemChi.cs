using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ImageName("BO_HoaDon")]
    [ModelDefault("Caption", "Ủy nhiệm chi")]
    [Appearance("UyNhiemChi.KhoaSo", TargetItems = "*", Enabled = false,
        Criteria = "KyTinhLuong is not null and KyTinhLuong.KhoaSo")]
    public class UyNhiemChi : BaseObject
    {
        private KyTinhLuong _KyTinhLuong;
        //private string _MauSo;
        private string _So;
        private DateTime _NgayLap;
        private ThongTinTruong _DonViTraTien;
        private string _MaDVQHNS;
        private string _DiaChi;
        private string _TaiKhoan;
        private NganHang _KhoBac;
        private string _DonViNhanTien;
        private string _MaDVQHNS1;
        private string _DiaChi1;
        private string _TaiKhoan1;
        private NganHang _KhoBac1;
        private string _NoiDungThanhToan;
        private decimal _SoTien;
        private string _SoTienBangChu;

        //[ModelDefault("Caption", "Mẫu số")]
        //public string MauSo
        //{
        //    get
        //    {
        //        return _MauSo;
        //    }
        //    set
        //    {
        //        SetPropertyValue("MauSo", ref _MauSo, value);
        //    }
        //}

        [ModelDefault("Caption", "Số")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string So
        {
            get
            {
                return _So;
            }
            set
            {
                SetPropertyValue("So", ref _So, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading && value != DateTime.MinValue)
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", value));
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("Số: {So} lập {NgayLap:'ngày' dd 'tháng' MM 'năm' yyyy}", this);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị trả tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinTruong DonViTraTien
        {
            get
            {
                return _DonViTraTien;
            }
            set
            {
                SetPropertyValue("DonViTraTien", ref _DonViTraTien, value);
                if (!IsLoading && value != null)
                {
                    DonViNhanTien = value.TenBoPhan;
                    MaDVQHNS = MaDVQHNS1 = value.MaQuanLy;
                    DiaChi = DiaChi1 = value.DiaChi.FullDiaChi;
                    foreach (TaiKhoanNganHang item in value.ListTaiKhoanNganHang)
                    {
                        if (item.TaiKhoanChinh)
                        {
                            TaiKhoan = TaiKhoan1 = item.SoTaiKhoan;
                            KhoBac = KhoBac1 = item.NganHang;
                            break;
                        }
                    }

                }
            }
        }

        [ModelDefault("Caption", "Mã ĐVQHNS")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaDVQHNS
        {
            get
            {
                return _MaDVQHNS;
            }
            set
            {
                SetPropertyValue("MaDVQHNS", ref _MaDVQHNS, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DiaChi
        {
            get
            {
                return _DiaChi;
            }
            set
            {
                SetPropertyValue("DiaChi", ref _DiaChi, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TaiKhoan
        {
            get
            {
                return _TaiKhoan;
            }
            set
            {
                SetPropertyValue("TaiKhoan", ref _TaiKhoan, value);
            }
        }

        [ModelDefault("Caption", "Tại kho bạc Nhà nước (NH)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang KhoBac
        {
            get
            {
                return _KhoBac;
            }
            set
            {
                SetPropertyValue("KhoBac", ref _KhoBac, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị nhận tiền")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DonViNhanTien
        {
            get
            {
                return _DonViNhanTien;
            }
            set
            {
                SetPropertyValue("DonViNhanTien", ref _DonViNhanTien, value);
            }
        }

        [ModelDefault("Caption", "Mã ĐVQHNS 1")]
        public string MaDVQHNS1
        {
            get
            {
                return _MaDVQHNS1;
            }
            set
            {
                SetPropertyValue("MaDVQHNS1", ref _MaDVQHNS1, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ 1")]
        public string DiaChi1
        {
            get
            {
                return _DiaChi1;
            }
            set
            {
                SetPropertyValue("DiaChi1", ref _DiaChi1, value);
            }
        }

        [ModelDefault("Caption", "Tài khoản 1")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string TaiKhoan1
        {
            get
            {
                return _TaiKhoan1;
            }
            set
            {
                SetPropertyValue("TaiKhoan1", ref _TaiKhoan1, value);
            }
        }

        [ModelDefault("Caption", "Tại Kho bạc Nhà nước (NH)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NganHang KhoBac1
        {
            get
            {
                return _KhoBac1;
            }
            set
            {
                SetPropertyValue("KhoBac1", ref _KhoBac1, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Nội dung thanh toán")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string NoiDungThanhToan
        {
            get
            {
                return _NoiDungThanhToan;
            }
            set
            {
                SetPropertyValue("NoiDungThanhToan", ref _NoiDungThanhToan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
                if (!IsLoading && value > 0)
                    SoTienBangChu = HamDungChung.DocTien(value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _SoTienBangChu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _SoTienBangChu, value);
            }
        }

        public UyNhiemChi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DonViTraTien = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
        }
    }

}
