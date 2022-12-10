using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.ThuNhap.Luong;


namespace PSC_HRM.Module.ThuNhap.LuongWeb
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")] 
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng lương web")]
    [RuleCombinationOfPropertiesIsUnique("BangLuongNhanVienWeb.Unique", DefaultContexts.Save, "KyTinhLuong;NgayLap")]
    public class BangLuongNhanVienWeb : BaseObject
    {
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;

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
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading)
                {
                    KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang = ? and Nam = ? and !KhoaSo", NgayLap.Month, NgayLap.Year));
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangLuongNhanVienWeb-ListChiTietLuongNhanVienWeb")]
        public XPCollection<ChiTietLuongNhanVienWeb> ListChiTietLuongNhanVienWeb
        {
            get
            {
                return GetCollection<ChiTietLuongNhanVienWeb>("ListChiTietLuongNhanVienWeb");
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);
            //
            KyTinhLuongList.Criteria = CriteriaOperator.Parse("!KhoaSo");
        }

        public BangLuongNhanVienWeb(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateKyTinhLuongList();
            //
            NgayLap = HamDungChung.GetServerTime();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
        }
    }

}
