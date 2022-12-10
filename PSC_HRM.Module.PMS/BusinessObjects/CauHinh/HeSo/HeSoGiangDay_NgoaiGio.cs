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
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số giảng dạy - ngoài giờ")]
    [DefaultProperty("Caption")]
    [Appearance("Hide_HVNH", TargetItems = "TuTiet;DenTiet;HeSoHe;BacDaoTao;HeDaoTao"
               , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_UFM", TargetItems = "HeSoHe"
               , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'UFM'")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;GioGiangDay;HeSo_GiangDayNgoaiGio;Thu;HeDaoTao;BacDaoTao", "Hệ số đã tồn tại")]
    public class HeSoGiangDay_NgoaiGio : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoGiangDayNgoaiGio")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private DayOfWeek? _Thu;
        private int _TuTiet;
        private int _DenTiet;

        private decimal _HeSo_GiangDayNgoaiGio;
        private bool _HeSoHe;
        private GioGiangDayEnum _GioGiangDay;

        //
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;

        [ModelDefault("Caption", "Từ tiết")]
        public int TuTiet
        {
            get { return _TuTiet; }
            set { SetPropertyValue("TuTiet", ref _TuTiet, value); }
        }


        [ModelDefault("Caption", "Đến tiết")]
        public int DenTiet
        {
            get { return _DenTiet; }
            set { SetPropertyValue("DenTiet", ref _DenTiet, value); }
        }


        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSo_GiangDayNgoaiGio", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_GiangDayNgoaiGio
        {
            get { return _HeSo_GiangDayNgoaiGio; }
            set { SetPropertyValue("HeSo_GiangDayNgoaiGio", ref _HeSo_GiangDayNgoaiGio, value); }
        }

        [ModelDefault("Caption", "Hệ số hè")]
        public bool HeSoHe
        {
            get { return _HeSoHe; }
            set { SetPropertyValue("HeSoHe", ref _HeSoHe, value); }
        }
        [ModelDefault("Caption", "Loại giờ giảng dạy")]
        public GioGiangDayEnum GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue("GioGiangDay", ref _GioGiangDay, value); }
        }

        [ModelDefault("Caption", "Thứ")]
        public DayOfWeek? Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        public HeSoGiangDay_NgoaiGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuTiet = 1;
            DenTiet = 11;
            HeSo_GiangDayNgoaiGio = 1;
       }
    }

}
