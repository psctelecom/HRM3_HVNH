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
using System.Linq;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Chi tiết quản lý truy thu thuế TNCN")]
    [RuleCombinationOfPropertiesIsUnique("ChiTietQuanLyTruyThuThueTNCN.Unique", DefaultContexts.Save, "KyTinhLuong")]
    public class ChiTietQuanLyTruyThuThueTNCN : BaseObject
    {
        // Fields...
        private KyTinhLuong _KyTinhLuong;
        private QuanLyTruyThuThueTNCN _QuanLyTruyThuThueTNCN;
        private int _STT;

        [ModelDefault("Caption", "Kỳ tính lương")]
        [RuleUniqueValue(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Browsable(false)]
        [Association("QuanLyTruyThuThueTNCN-ListChiTietQuanLyTruyThuThueTNCN")]
        public QuanLyTruyThuThueTNCN QuanLyTruyThuThueTNCN
        {
            get
            {
                return _QuanLyTruyThuThueTNCN;
            }
            set
            {
                SetPropertyValue("QuanLyTruyThuThueTNCN", ref _QuanLyTruyThuThueTNCN, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "STT")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách truy thuế TNCN")]
        [Association("ChiTietQuanLyTruyThuThueTNCN-ListTruyThuThueTNCN")]
        public XPCollection<TruyThuThueTNCN> ListTruyThuThueTNCN
        {
            get
            {
                return GetCollection<TruyThuThueTNCN>("ListTruyThuThueTNCN");
            }
        }

        public ChiTietQuanLyTruyThuThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            XPCollection<ChiTietQuanLyTruyThuThueTNCN> list = new XPCollection<ChiTietQuanLyTruyThuThueTNCN>(Session);
            STT = 1;
            if (list.Count > 0)
                STT = (from d in list select d.STT).Max() + 1;
        }
    }

}
