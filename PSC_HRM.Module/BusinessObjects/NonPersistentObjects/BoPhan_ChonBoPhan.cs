using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn đơn vị")]
    public class BoPhan_ChonBoPhan : BaseObject
    {
        // Fields...

        private bool _ChonTatCa;


        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
        [ModelDefault("AllowEdit", "True")]
        public bool ChonTatCa
        {
            get
            {
                return _ChonTatCa;
            }
            set
            {
                SetPropertyValue("ChonTatCa", ref _ChonTatCa, value);
                if (!IsLoading)
                    CheckBoPhan();
            }
        }

        [ModelDefault("Caption", "Danh sách bộ phận")]
        [ModelDefault("AllowEdit", "True")]
        public XPCollection<BoPhan_ChonBoPhanItem> ListBoPhan { get; set; }

        public BoPhan_ChonBoPhan(Session session)
            : base(session)
        { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListBoPhan = new XPCollection<BoPhan_ChonBoPhanItem>(Session, false);
        }

        private void CheckBoPhan()
        {
            foreach (BoPhan_ChonBoPhanItem item in ListBoPhan)
            {
                if (item.Chon != ChonTatCa)
                    item.Chon = ChonTatCa;
            }
        }
    }

}
