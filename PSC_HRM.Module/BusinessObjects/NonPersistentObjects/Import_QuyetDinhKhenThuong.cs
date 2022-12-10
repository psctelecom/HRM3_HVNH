using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.Validation;
using System.ComponentModel;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn quyết định khen thưởng")]
    public class Import_QuyetDinhKhenThuong : BaseObject
    {
        // Fields...
        private decimal _SoTienChiuThue;
        private QuyetDinhKhenThuong _QuyetDinhKhenThuong;
        private decimal _SoTienThuong;

        [ImmediatePostData]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Số tiền thưởng")]
        public decimal SoTienThuong
        {
            get
            {
                return _SoTienThuong;
            }
            set
            {
                SetPropertyValue("SoTienThuong", ref _SoTienThuong, value);
                if (!IsLoading)
                    SoTienChiuThue = value;
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Số tiền chịu thuế")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }

        [DataSourceProperty("QDList")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định khen thưởng")]
        public QuyetDinhKhenThuong QuyetDinhKhenThuong
        {
            get
            {
                return _QuyetDinhKhenThuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhKhenThuong", ref _QuyetDinhKhenThuong, value);
            }
        }

        public Import_QuyetDinhKhenThuong(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            QDList = new XPCollection<QuyetDinhKhenThuong>(Session);
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhKhenThuong> QDList { get; set; }
    }

}
