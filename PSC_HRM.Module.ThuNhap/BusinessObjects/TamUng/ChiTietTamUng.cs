using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.TamUng
{
    [ImageName("BO_BangLuong")]
    [DefaultProperty("NgayLap")]
    [ModelDefault("Caption", "Chi tiết tạm ứng")]
    public class ChiTietTamUng : BaseObject
    {
        private DateTime _NgayLap;
        private decimal _SoTien;
        private string _LyDo;
        private TamUng _TamUng;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng tạm ứng")]
        [Association("TamUng-ListChiTietTamUng")]
        public TamUng TamUng
        {
            get
            {
                return _TamUng;
            }
            set
            {
                SetPropertyValue("TamUng", ref _TamUng, value);
            }
        }

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
            }
        }

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

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
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

        public ChiTietTamUng(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            TamUng.XuLy();
        }
    }

}
