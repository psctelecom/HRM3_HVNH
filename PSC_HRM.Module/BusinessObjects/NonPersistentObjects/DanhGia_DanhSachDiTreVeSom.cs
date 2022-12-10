using System;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ vi phạm")]
    public class DanhGia_DanhSachDiTreVeSom : BaseObject
    {
        // Fields...
        private bool _ChonTatCa;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
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
                    AfterCheckStateChanged();
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DanhGia_DiTreVeSom> ViPhamList { get; set; }

        public DanhGia_DanhSachDiTreVeSom(Session session) : base(session) 
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ViPhamList = new XPCollection<DanhGia_DiTreVeSom>(Session, false);
        }

        private void AfterCheckStateChanged()
        {
            foreach (DanhGia_DiTreVeSom item in ViPhamList)
            {
                if (item.Chon != ChonTatCa)
                    item.Chon = ChonTatCa;
            }
        }
    }

}
