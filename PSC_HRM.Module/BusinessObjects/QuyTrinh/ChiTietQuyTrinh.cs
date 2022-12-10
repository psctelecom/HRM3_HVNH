using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuyTrinh
{
    [DefaultProperty("TenChiTietQuyTrinh")]
    [ModelDefault("Caption", "Chi tiết quy trình")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyTrinh;SoThuTu")]
    public class ChiTietQuyTrinh : BaseObject
    {
        // Fields...
        private QuyTrinh _QuyTrinh;
        private string _TenChiTietQuyTrinh;
        private int _SoThuTu;

        [Browsable(false)]
        [Association("QuyTrinh-ListChiTietQuyTrinh")]
        [ModelDefault("Caption", "Quy trình")]
        public QuyTrinh QuyTrinh
        {
            get
            {
                return _QuyTrinh;
            }
            set
            {
                SetPropertyValue("QuyTrinh", ref _QuyTrinh, value);
                if (!IsLoading && value != null)
                {
                    SoThuTu = value.ListChiTietQuyTrinh.Count + 1;
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Số thứ tự")]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên chi tiết quy trình")]
        public string TenChiTietQuyTrinh
        {
            get
            {
                return _TenChiTietQuyTrinh;
            }
            set
            {
                SetPropertyValue("TenChiTietQuyTrinh", ref _TenChiTietQuyTrinh, value);
            }
        }

        public ChiTietQuyTrinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();            
        }
    }
}
