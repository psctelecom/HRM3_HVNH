using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NghiPhep
{
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Chi tiết nghỉ phép")]
    [Appearance("ChiTietNghiPhep", TargetItems = "*", Enabled = false, Criteria = "Khoa")]
    public class ChiTietNghiPhep : BaseObject
    {
        /*
        private bool _Khoa;
        private decimal _SoNgayQuaHan;
        private decimal _TongSoNgayNghiPhep;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private string _GhiChu;
        private ThongTinNghiPhep _ThongTinNghiPhep;

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin nghỉ phép")]
        [Association("ThongTinNghiPhep-ListChiTietNghiPhep")]
        public ThongTinNghiPhep ThongTinNghiPhep
        {
            get
            {
                return _ThongTinNghiPhep;
            }
            set
            {
                SetPropertyValue("ThongTinNghiPhep", ref _ThongTinNghiPhep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.StartDay);
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    TongSoNgayNghiPhep = TuNgay.TinhSoNgay(DenNgay, Session);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.SetTime(SetTimeEnum.EndDay);
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    TongSoNgayNghiPhep = TuNgay.TinhSoNgay(DenNgay, Session);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tổng số ngày nghỉ phép")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal TongSoNgayNghiPhep
        {
            get
            {
                return _TongSoNgayNghiPhep;
            }
            set
            {
                SetPropertyValue("TongSoNgayNghiPhep", ref _TongSoNgayNghiPhep, value);
                if (!IsLoading)
                {
                    decimal temp = (ThongTinNghiPhep.SoNgayPhepConLai > 0 ? ThongTinNghiPhep.SoNgayPhepConLai : 0) - value;
                    if (temp < 0)
                        SoNgayQuaHan = Math.Abs(temp);
                }
            }
        }

        [Browsable(false)]
        public decimal SoNgayQuaHan
        {
            get
            {
                return _SoNgayQuaHan;
            }
            set
            {
                SetPropertyValue("SoNgayQuaHan", ref _SoNgayQuaHan, value);
            }
        }

        [Browsable(false)]
        public bool Khoa
        {
            get
            {
                return _Khoa;
            }
            set
            {
                SetPropertyValue("Khoa", ref _Khoa, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietNghiPhep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && !Khoa)
            {
                Khoa = true;
                ThongTinNghiPhep.SoNgayPhepDaNghi += TongSoNgayNghiPhep;
            }
        }

        protected override void OnDeleting()
        {
            ThongTinNghiPhep.SoNgayPhepDaNghi -= TongSoNgayNghiPhep;

            base.OnDeleting();
        }
         */
    }

}
