using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_ChungTu")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Chứng từ")]
    [Appearance("ChungTu.KhoaSo", TargetItems = "*", Enabled = false,Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo)")]
    [Appearance("HideKhac_QNU", TargetItems = "DienGiaiDH;DienGiaiPC", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'QNU'")]
    public class ChungTu : TruongBaseObject, IThongTinTruong
    {
        private UyNhiemChi _UyNhiemChi;
        private bool _TinhThueTNCN;
        private int _SoThuTu;
        private string _SoChungTu;
        private DateTime _TuNgay;
        private DateTime _NgayLap = HamDungChung.GetServerTime();
        private KyTinhLuong _KyTinhLuong;
        private string _DienGiai;
        private string _DienGiaiDH;
        private string _DienGiaiPC;
        private decimal _SoTien;
        private string _Sotienbangchu;
        private ThongTinTruong _ThongTinTruong;
        private string _LoaiChi;
        private int _UyNhiemChiERP;

        [ImmediatePostData]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số thứ tự")]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
                if (!IsLoading && value > 0 && NgayLap != DateTime.MinValue)
                {
                    if (this is ChuyenKhoanLuongNhanVien)
                    {
                        if(MaTruong == "UEL")
                            SoChungTu = string.Format("CV{0:0#}/{1:####}", value, NgayLap.Year);
                        else
                            SoChungTu = string.Format("CK{0:0#}/{1:####}", value, NgayLap.Year);
                    }
                    else
                    {
                        SoChungTu = string.Format("TM{0:0#}/{1:####}", value, NgayLap.Year);
                    }
                }
            }
        }

        [ModelDefault("Caption", "Số chứng từ")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoChungTu
        {
            get
            {
                return _SoChungTu;
            }
            set
            {
                SetPropertyValue("SoChungTu", ref _SoChungTu, value);
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
                SetPropertyValue("NgayLap", ref _NgayLap, value.SetTime(SetTimeEnum.EndDay));
                if (!IsLoading && value != DateTime.MinValue)
                {
                    //update ky tinh luong
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                    //
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));

                    //Tạo số thứ tự tự tăng
                    CriteriaOperator filter = CriteriaOperator.Parse("NgayLap>=? and NgayLap<=?",
                        value.SetTime(SetTimeEnum.StartYear), value.SetTime(SetTimeEnum.EndYear));
                    object obj = null;
                    if (this is ChiTMLuongNhanVien)
                    {
                        obj = Session.Evaluate<ChiTMLuongNhanVien>(CriteriaOperator.Parse("Max(SoThuTu)"), filter);
                        if (obj != null)
                            SoThuTu = (int)obj + 1;
                    }
                    else if (this is ChuyenKhoanLuongNhanVien)
                    {
                        obj = Session.Evaluate<ChuyenKhoanLuongNhanVien>(CriteriaOperator.Parse("Max(SoThuTu)"), filter);
                        if (obj != null)
                            SoThuTu = (int)obj + 1;
                    }
                    if (SoThuTu == 0)
                        SoThuTu = 1;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading && value != null)
                    DienGiai = string.Format("Chuyển khoản lương nhân viên tháng {0:0#}/{1:####}", value.Thang, value.Nam);
            }
        }

        //Chỉ dùng để xác định khoảng thời gian tính tiền để lập chứng từ
        [Browsable(false)]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Loại chi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.ComboBoxEditor")]
        public string LoaiChi
        {
            get
            {
                return _LoaiChi;
            }
            set
            {
                SetPropertyValue("LoaiChi", ref _LoaiChi, value);
                {
                    if (!IsLoading && MaTruong.Equals("IUH"))
                    {
                        if (!IsLoading && value.Equals(string.Format("{0}", LoaiChiEnum.LuongKy2)))
                        {
                            TinhThueTNCN = true;
                        }
                        else { TinhThueTNCN = false; }
                    }
                    if (!IsLoading && (MaTruong.Equals("UTE") || MaTruong.Equals("DLU")))
                    {
                        TinhThueTNCN = true; 
                    }
                }
            }
        }


        [ModelDefault("Caption", "Tính Thuế TNCN")]
        public bool TinhThueTNCN
        {
            get
            {
                return _TinhThueTNCN;
            }
            set
            {
                SetPropertyValue("TinhThueTNCN", ref _TinhThueTNCN, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }
        [ModelDefault("Caption", "Diễn giải DH")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiaiDH
        {
            get
            {
                return _DienGiaiDH;
            }
            set
            {
                SetPropertyValue("DienGiaiDH", ref _DienGiaiDH, value);
            }
        }
        [ModelDefault("Caption", "Diễn giải PC")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiaiPC
        {
            get
            {
                return _DienGiaiPC;
            }
            set
            {
                SetPropertyValue("DienGiaiPC", ref _DienGiaiPC, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [Size(300)]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _Sotienbangchu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _Sotienbangchu, value);
            }
        }

        //chỉ dùng để an toàn dữ liệu
        [Browsable(false)]
        public UyNhiemChi UyNhiemChi
        {
            get
            {
                return _UyNhiemChi;
            }
            set
            {
                SetPropertyValue("UyNhiemChi", ref _UyNhiemChi, value);
            }
        }

        ////chỉ dùng để an toàn dữ liệu       
        //public int UyNhiemChiERP
        //{
        //    get
        //    {
        //        return _UyNhiemChiERP;
        //    }
        //    set
        //    {
        //        SetPropertyValue("UyNhiemChiERP", ref _UyNhiemChiERP, value);
        //    }
        //}

        public ChungTu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            //
            UpdateKyTinhLuongList();
            //
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang=? and Nam=? and !KhoaSo", NgayLap.Month, NgayLap.Year));
            //
            NgayLap = HamDungChung.GetServerTime();
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading && value != null)
                    AfterThongTinTruongChanged();
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);
            //
            if (ThongTinTruong != null)
                KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo and Nam=?", ThongTinTruong, NgayLap.Year);
        }

        protected virtual void AfterThongTinTruongChanged()
        { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            if (string.IsNullOrEmpty(SoTienBangChu))
            {
                this.SoTienBangChu = HamDungChung.DocTien(Math.Round(SoTien, 0));
            }
        }
    }

}
