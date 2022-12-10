using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao
{

    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Nhân viên giờ giảng")]
    [Appearance("Enabled_TongGio", TargetItems = "TongGio", Enabled = false)]

    [Appearance("Khoa_TamUng", TargetItems = "TamUng", Enabled = false, Criteria = "Khoa = 1 ")]
    public class NhanVien_GioGiang : BaseObject,IBoPhan
    {
        private QuanLyGioGiang _QuanLyGioGiang;
        private NhanVien _NhanVien;
        private BoPhan _BoPhan;
        //Trụ sở
        private decimal _GioNghiaVu;
        private decimal _TongGio;
        private decimal _TamUng;
        private decimal _TongGioSauTamUng;
        //Băc Ninh
        private decimal _GioNghiaVuBN;
        private decimal _TongGioBN;
        private decimal _TamUngBN;
        private decimal _TongGioSauTamUngBN;
        //Phú Yên
        private decimal _GioNghiaVuPY;
        private decimal _TongGioPY;
        private decimal _TamUngPY;
        private decimal _TongGioSauTamUngPY;
        //
        private decimal _ThanhTienTamUng;
        private decimal _ThanhTienConLai;   
        private bool _DaThanhToanTamUng;

        private bool _Khoa;
        private string _GhiChu;
        private Guid _OidChiTietThuLaoNhanVien;
        [Browsable(false)]
        public Guid OidChiTietThuLaoNhanVien
        {
            get { return _OidChiTietThuLaoNhanVien; }
            set { SetPropertyValue("OidChiTietThuLaoNhanVien", ref _OidChiTietThuLaoNhanVien, value); }
        }

        [Association("QuanLyGioGiang-ListNhanVien_GioGiang")]
        [ModelDefault("Caption", "Quản lý giờ giảng")]
        [Browsable(false)]
        public QuanLyGioGiang QuanLyGioGiang
        {
            get
            {
                return _QuanLyGioGiang;
            }
            set
            {
                SetPropertyValue("QuanLyGioGiang", ref _QuanLyGioGiang, value);
            }
        }
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format("{0}", NhanVien != null ? NhanVien.HoTen : "");
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        //Trụ sở

        [ModelDefault("Caption", "Tổng giờ trụ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal TongGio
        {
            get { return _TongGio; }
            set
            {
                SetPropertyValue("TongGio", ref _TongGio, value);
            }
        }
        [ModelDefault("Caption", "Tổng giờ (Sau tạm ứng) trụ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioSauTamUng
        {
            get { return _TongGioSauTamUng; }
            set
            {
                SetPropertyValue("TongGioSauTamUng", ref _TongGioSauTamUng, value);
            }
        }

        [ModelDefault("Caption", "Tạm ứng trụ sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        //[RuleRange("NhanVienGioGiang_TamUng", DefaultContexts.Save, 0, 100000, "Số giờ phải > 0!")]
        public decimal TamUng
        {
            get { return _TamUng; }
            set
            {
                SetPropertyValue("TamUng", ref _TamUng, value);
                if (!IsLoading)
                {
                    check();
                    if (TamUng != 0)
                        TongGioSauTamUng = TongGio - TamUng;
                    else
                        TongGioSauTamUng = TongGio;
                }
            }
        }

        [ModelDefault("Caption", "Định mức giờ chuẩn tru sở")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal GioNghiaVu
        {
            get { return _GioNghiaVu; }
            set
            {
                SetPropertyValue("GioNghiaVu", ref _GioNghiaVu, value);
            }
        }

        //Bắc Ninh

        [ModelDefault("Caption", "Tổng giờ bắc ninh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal TongGioBN
        {
            get { return _TongGioBN; }
            set
            {
                SetPropertyValue("TongGioBN", ref _TongGioBN, value);
            }
        }
        [ModelDefault("Caption", "Tổng giờ (Sau tạm ứng) bắc ninh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioSauTamUngBN
        {
            get { return _TongGioSauTamUngBN; }
            set
            {
                SetPropertyValue("TongGioSauTamUngBN", ref _TongGioSauTamUngBN, value);
            }
        }

        [ModelDefault("Caption", "Tạm ứng bắc ninh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        //[RuleRange("NhanVienGioGiang_TamUng", DefaultContexts.Save, 0, 100000, "Số giờ phải > 0!")]
        public decimal TamUngBN
        {
            get { return _TamUngBN; }
            set
            {
                SetPropertyValue("TamUngBN", ref _TamUngBN, value);
                if (!IsLoading)
                {
                    check1();
                    if (TamUng != 0)
                        TongGioSauTamUngBN = TongGioBN - TamUngBN;
                    else
                        TongGioSauTamUngBN = TongGioBN;
                }
            }
        }

        [ModelDefault("Caption", "Định mức giờ chuẩn bắc ninh")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal GioNghiaVuBN
        {
            get { return _GioNghiaVuBN; }
            set
            {
                SetPropertyValue("GioNghiaVuBN", ref _GioNghiaVuBN, value);
            }
        }

        //Phú Yên
        [ModelDefault("Caption", "Tổng giờ phú yên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal TongGioPY
        {
            get { return _TongGioPY; }
            set
            {
                SetPropertyValue("TongGioPY", ref _TongGioPY, value);
            }
        }
        [ModelDefault("Caption", "Tổng giờ (Sau tạm ứng) phú yên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        public decimal TongGioSauTamUngPY
        {
            get { return _TongGioSauTamUngPY; }
            set
            {
                SetPropertyValue("TongGioSauTamUngPY", ref _TongGioSauTamUngPY, value);
            }
        }

        [ModelDefault("Caption", "Tạm ứng phú yên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        //[RuleRange("NhanVienGioGiang_TamUng", DefaultContexts.Save, 0, 100000, "Số giờ phải > 0!")]
        public decimal TamUngPY
        {
            get { return _TamUngPY; }
            set
            {
                SetPropertyValue("TamUngPY", ref _TamUngPY, value);
                if (!IsLoading)
                {
                    check2();
                    if (TamUng != 0)
                        TongGioSauTamUngPY = TongGioPY - TamUngPY;
                    else
                        TongGioSauTamUngPY = TongGioPY;
                }
            }
        }

        [ModelDefault("Caption", "Định mức giờ chuẩn phú yên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public decimal GioNghiaVuPY
        {
            get { return _GioNghiaVuPY; }
            set
            {
                SetPropertyValue("GioNghiaVuPY", ref _GioNghiaVuPY, value);
            }
        }

        /// <summary>
        /// /
        /// </summary>


        [ModelDefault("Caption", "Thành tiền tạm ứng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit","false")]
        [Browsable(false)]
        [ImmediatePostData]
        public decimal ThanhTienTamUng
        {
            get { return _ThanhTienTamUng; }
            set
            {
                SetPropertyValue("ThanhTienTamUng", ref _ThanhTienTamUng, value);
            }
        }

        [ModelDefault("Caption", "Thành tiền còn lại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        [Browsable(false)]
        public decimal ThanhTienConLai
        {
            get { return _ThanhTienConLai; }
            set
            {
                SetPropertyValue("ThanhTienConLai", ref _ThanhTienConLai, value);
            }
        }
        
        [ModelDefault("Caption", "Ghi chú")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Đã thanh toán tạm ứng")]
        [ModelDefault("AllowEdit","False")]
        public bool DaThanhToanTamUng
        {
            get { return _DaThanhToanTamUng; }
            set { SetPropertyValue("DaThanhToanTamUng", ref _DaThanhToanTamUng, value); }
        }
        
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get{return _Khoa;}
            set{SetPropertyValue("Khoa",ref _Khoa,value);}
        }
        [Aggregated]
        [Association("NhanVien_GioGiang-ListChiTietGioGiang")]
        [ModelDefault("Caption", "Chi tiết giờ giảng")]
        public XPCollection<ChiTietGioGiang> ListChiTietGioGiang
        {
            get
            {
                return GetCollection<ChiTietGioGiang>("ListChiTietGioGiang");
            }
        }
        public NhanVien_GioGiang(Session session)
            : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            GhiChu = "Tạm ứng giờ giảng";
        }
        protected override void OnSaved()
        {
            base.OnSaved();
        }
        private bool _KT;
        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty("NhanVien_GioGiang.KT", DefaultContexts.Save, "Số giờ tạm ứng đã vượt định mức!", SkipNullOrEmptyValues = false)]
        [ModelDefault("Caption", "kiểm tra")]
        public bool KT
        {
            get
            {
                return !_KT;
            }
            set
            {
                SetPropertyValue("KT", ref _KT, value);
            }
        }
        void check()
        {
            if (GioNghiaVu > 0)
                if (TamUng <= TongGio - (GioNghiaVu / 2))
                    KT = false;
                else
                    KT = true;
            else
                KT = false;
        }
        void check1()
        {
            if (GioNghiaVuBN > 0)
                if (TamUngBN <= TongGioBN - (GioNghiaVuBN / 2))
                    KT = false;
                else
                    KT = true;
            else
                KT = false;
        }
        void check2()
        {
            if (GioNghiaVuPY > 0)
                if (TamUngPY <= TongGioPY - (GioNghiaVuPY / 2))
                    KT = false;
                else
                    KT = true;
            else
                KT = false;
        }

        public void XyLyTongGio()
        {
            if (ListChiTietGioGiang != null)
            {
                //CriteriaOperator fchitiet = CriteriaOperator.Parse("NhanVien_GioGiang = ?", this.Oid);
                XPCollection<ChiTietGioGiang> dschitiet = ListChiTietGioGiang;
                decimal Cong = 0;
                decimal Tru = 0;
                foreach (ChiTietGioGiang item in dschitiet)
                {
                    if (item.CongTru.GetHashCode() == 0)
                    {
                        Cong = Cong + item.SoGio;
                    }
                    else
                    {
                        Tru = Tru + item.SoGio;
                    }
                    TongGio = Cong - Tru;
                    TamUng = Tru;
                }
            }
            //TongGio = ListChiTietGioGiang.Where(item => item.CongTru.GetHashCode() == 0).Sum(item => item.SoGio) - ListChiTietGioGiang.Where(item => item.CongTru.GetHashCode() == 1).Sum(item => item.SoGio);
            OnChanged("TongGio");
            OnChanged("TamUng");
        }
        public void XyLyTongGio_Xoa(decimal GioXoa)
        {
            TongGio = TongGio + GioXoa;
            TamUng = TamUng - GioXoa;
            OnChanged("TongGio");
            OnChanged("TamUng");
        }
        public void XuLyThemKhongXoaDongBo(decimal GioXoa, bool DongBo, string GhiChu, Guid GuidOid)
        {
            ChiTietGioGiang ct = new ChiTietGioGiang(Session);
            ct.SoGio = GioXoa;
            ct.DongBo = DongBo;
            ct.GhiChu = GhiChu;
            ct.ThongTinBangChot = Session.GetObjectByKey<ThongTinBangChot>(GuidOid);
            ListChiTietGioGiang.Add(ct);
        }
    }

}