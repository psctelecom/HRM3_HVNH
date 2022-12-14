using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Quản lý truy thu thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyTruyThuThueTNCN.Unique", DefaultContexts.Save, "Nam")]
    public class QuanLyTruyThuThueTNCN : BaseObject
    {
        // Fields...
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [RuleUniqueValue(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách kỳ tính lương")]
        [Association("QuanLyTruyThuThueTNCN-ListChiTietQuanLyTruyThuThueTNCN")]
        public XPCollection<ChiTietQuanLyTruyThuThueTNCN> ListChiTietQuanLyTruyThuThueTNCN
        {
            get
            {
                return GetCollection<ChiTietQuanLyTruyThuThueTNCN>("ListChiTietQuanLyTruyThuThueTNCN");
            }
        }

        public QuanLyTruyThuThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
