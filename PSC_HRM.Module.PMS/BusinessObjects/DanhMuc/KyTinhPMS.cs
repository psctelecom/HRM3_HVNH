using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Kỳ tính PMS")]
    [DefaultProperty("TenKy")]
    //[RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "TongKet;NamHoc;Dot", "Đợt tổng kết đã tồn tại!")]
    public class KyTinhPMS : BaseObject
    {
        private NamHoc _NamHoc;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private int _Dot;
        private bool _TongKet;

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        [Browsable(false)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        [Browsable(false)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [ModelDefault("AllowEdit","False")]
        public int Dot
        {
            get { return _Dot; }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }
        [ModelDefault("Caption", "Đợt tổng kết cuối năm")]
        public bool TongKet
        { get { return _TongKet; }
            set { SetPropertyValue("TongKet", ref _TongKet, value); }
        }
        [ModelDefault("Caption", "Tên kỳ tính")]
        public string TenKy
        {
            get
            {
                return String.Format("Đợt {0} {1} {2}", Dot.ToString(), TuNgay != DateTime.MinValue ? "- Từ " + TuNgay.Date.ToString("dd/MM/yyyy") : "", DenNgay != DateTime.MinValue ? " đến " + DenNgay.Date.ToString("dd/MM/yyyy") : "");
            }
        }
        public KyTinhPMS(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            XPCollection<KyTinhPMS> listKyTinh = new XPCollection<KyTinhPMS>(Session);
            if (listKyTinh == null)
                Dot = 1;
            else
                Dot = listKyTinh.Count + 1;
        }
    }
}