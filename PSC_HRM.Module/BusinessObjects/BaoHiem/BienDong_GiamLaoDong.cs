using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_BienDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Giảm lao động")]
    [Appearance("BienDong_GiamLaoDong", TargetItems = "TuThang;DenThang", Enabled = false, Criteria = "!KhongTraTheBHYT")]
    [Appearance("BienDong_GiamLaoDong.NghiViec", TargetItems = "DenThang", Enabled = false, Criteria = "LyDo = 0")]
    public class BienDong_GiamLaoDong : BienDong
    {
        // Fields...
        private decimal _TNGD;
        private int _TNVK;
        private decimal _PCCV;
        private decimal _TienLuong;
        private decimal _PCK;
        private DateTime _DenThang;
        private DateTime _TuThang;
        private bool _KhongTraTheBHYT;
        private LyDoNghiEnum _LyDo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Lý do")]
        public LyDoNghiEnum LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Không trả thẻ BHYT")]
        public bool KhongTraTheBHYT
        {
            get
            {
                return _KhongTraTheBHYT;
            }
            set
            {
                SetPropertyValue("KhongTraTheBHYT", ref _KhongTraTheBHYT, value);
            }
        }

        [ModelDefault("Caption", "Từ tháng, năm")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria="KhongTraTheBHYT")]
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
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "KhongTraTheBHYT")]
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

        public BienDong_GiamLaoDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiBienDong = "Giảm lao động";
            LyDo = LyDoNghiEnum.ThoiViec;
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            TienLuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
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
                //khi giảm lao động:
                //1. thay đổi trạng thái hồ sơ bảo hiểm
                //2. giảm bảo hiểm y tế
                //3. giảm bảo hiểm thất nghiệp
                HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
                if (hoSo != null)
                {                                        
                    //giảm bhyt
                    if (!string.IsNullOrEmpty(hoSo.SoTheBHYT))
                    {
                        BienDong_BHYT bhyt = Session.FindObject<BienDong_BHYT>(CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?", 
                            ThongTinNhanVien, TuNgay));
                        if (bhyt == null)
                        {
                            bhyt = new BienDong_BHYT(Session);
                            bhyt.QuanLyBienDong = QuanLyBienDong;
                            bhyt.BoPhan = BoPhan;
                            bhyt.ThongTinNhanVien = ThongTinNhanVien;
                            bhyt.TuNgay = TuNgay;
                            bhyt.PhanLoai = LoaiBienDongEnum.ThoaiTra;
                        }
                    }

                    // giảm bhtn
                    if (!hoSo.KhongThamGiaBHTN)
                    {
                        BienDong_BHTN bhtn = Session.FindObject<BienDong_BHTN>(CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?", 
                            ThongTinNhanVien, TuNgay));
                        if (bhtn == null)
                        {
                            bhtn = new BienDong_BHTN(Session);
                            bhtn.QuanLyBienDong = QuanLyBienDong;
                            bhtn.BoPhan = BoPhan;
                            bhtn.ThongTinNhanVien = ThongTinNhanVien;
                            bhtn.TuNgay = TuNgay;
                            bhtn.PhanLoai = LoaiBienDongEnum.ThoaiTra;
                        }
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            BienDong_BHYT bhyt = Session.FindObject<BienDong_BHYT>(CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?", ThongTinNhanVien, TuNgay));
            if (bhyt != null)
            {
                Session.Delete(bhyt);
                Session.Save(bhyt);
            }
            BienDong_BHTN bhtn = Session.FindObject<BienDong_BHTN>(CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?", ThongTinNhanVien, TuNgay));
            if (bhtn != null)
            {
                Session.Delete(bhtn);
                Session.Save(bhtn);
            }

            base.OnDeleting();
        }
    }

}
