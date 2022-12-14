using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using PSC_HRM.Module;
using System.Data.SqlClient;


namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [DefaultProperty("TenKy")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Kỳ tính lương")]
    [Appearance("KyTinhLuong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "Thang,Nam,BangChotThongTinTinhLuong")]
    public class KyTinhLuong : BaseObject, IThongTinTruong
    {
        private BangChotThongTinTinhLuong _BangChotThongTinTinhLuong;
        private ThongTinTruong _ThongTinTruong;
        private int _Thang;
        private int _Nam;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoNgay;
        private bool _KhoaSo;
        private ThongTinChung _ThongTinChung;
        private MocTinhThueTNCN _MocTinhThueTNCN;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tháng")]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    TuNgay = new DateTime(Nam, Thang, 1);
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    TuNgay = new DateTime(Nam, Thang, 1);
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Tên kỳ lương")]
        public string TenKy
        {
            get
            {
                return String.Format("Tháng {0:0#} năm {1:####}", Thang, Nam);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                //đưa giờ về đầu ngày
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartDay);
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading)
                {
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                //đưa giờ về cuối ngày
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.EndDay);
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                {
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session);
                }
            }
        }

        [ModelDefault("Caption", "Số ngày")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("EditMask", "n1")]
        [ModelDefault("DisplayFormat", "n1")]
        public decimal SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
                if (!IsLoading && BangChotThongTinTinhLuong != null)
                {
                    if(KhoaSo)
                    BangChotThongTinTinhLuong.KhoaSo = true;
                    else
                    BangChotThongTinTinhLuong.KhoaSo = false;
                }
          
            }
        }

        [ModelDefault("Caption", "Bảng chốt thông tin tính lương")]
        public BangChotThongTinTinhLuong BangChotThongTinTinhLuong
        {
            get
            {
                return _BangChotThongTinTinhLuong;
            }
            set
            {
                SetPropertyValue("BangChotThongTinTinhLuong", ref _BangChotThongTinTinhLuong, value);
            }
        }

        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("Caption", "Thông tin chung")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public ThongTinChung ThongTinChung
        {
            get
            {
                return _ThongTinChung;
            }
            set
            {
                SetPropertyValue("ThongTinChung", ref _ThongTinChung, value);
            }
        }

        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("Caption", "Mốc tính thuế TNCN")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocTinhThueTNCN MocTinhThueTNCN
        {
            get
            {
                return _MocTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("MocTinhThueTNCN", ref _MocTinhThueTNCN, value);
            }
        }

        [Browsable(false)]
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
            }
        }

        public KyTinhLuong(Session session) :
            base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);

            if (ThongTinTruong != null)
            {
                if (ThongTinTruong.ThongTinChung != null)
                    ThongTinChung = HamDungChung.Copy<ThongTinChung>(Session, ThongTinTruong.ThongTinChung);
                else
                    ThongTinChung = new ThongTinChung(Session);

                if (ThongTinTruong.MocTinhThueTNCN != null)
                    MocTinhThueTNCN = HamDungChung.Copy<MocTinhThueTNCN>(Session, ThongTinTruong.MocTinhThueTNCN);
                else
                    MocTinhThueTNCN = new MocTinhThueTNCN(Session);
            }
            else
            {
                ThongTinChung = new ThongTinChung(Session);

                MocTinhThueTNCN = new MocTinhThueTNCN(Session);
            }

            DateTime current = HamDungChung.GetServerTime();
            Thang = current.Month;
            Nam = current.Year;
            SoNgay = 22;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }

        public void SoNgayLamViecTrongThang(DateTime tuNgay, DateTime denNgay)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", tuNgay);
            param[1] = new SqlParameter("@DenNgay", denNgay);
            object obj = DataProvider.GetObject("spd_SoNgayLamViecTrongThang", System.Data.CommandType.StoredProcedure, param);
            SoNgay = ThongTinChung.SoNgayThang = Convert.ToDecimal(obj.ToString());

        }

    }

}
