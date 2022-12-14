using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ChamCong
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Bảng chấm công ngoài giờ")]
    [ImageName("BO_ChamCong")]
    [Appearance("BangChamCongNgoaiGio", TargetItems = "*", Enabled = false,
        Criteria = "KyTinhLuong is not null and KyTinhLuong.KhoaSo")]
    public class BangChamCongNgoaiGio : BaseObject, IThongTinTruong
    {
        private ThongTinTruong _ThongTinTruong;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;

        [ImmediatePostData]
        [DataSourceCriteria("!KhoaSo")]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangChamCongNgoaiGio-ListChiTietChamCongNgoaiGio")]
        public XPCollection<ChiTietChamCongNgoaiGio> ListChiTietChamCongNgoaiGio
        {
            get
            {
                return GetCollection<ChiTietChamCongNgoaiGio>("ListChiTietChamCongNgoaiGio");
            }
        }

        public BangChamCongNgoaiGio(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
            KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=? and !KhoaSo", NgayLap, NgayLap));
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }

}
