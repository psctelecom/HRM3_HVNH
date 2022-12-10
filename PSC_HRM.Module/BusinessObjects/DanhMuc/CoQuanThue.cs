using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Cơ quan thuế")]
    [DefaultProperty("TenCoQuanThue")]
    public class CoQuanThue : BaseObject, ITreeNode
    {
        private string _MaQuanLy;
        private string _TenCoQuanThue;
        private CoQuanThue _DonViChuQuan;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên cơ quan thuế")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenCoQuanThue
        {
            get
            {
                return _TenCoQuanThue;
            }
            set
            {
                SetPropertyValue("TenCoQuanThue", ref _TenCoQuanThue, value);
            }
        }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Cơ quan chủ quản")]
        [Association("CoQuanThue-DonViTrucThuoc")]
        public CoQuanThue DonViChuQuan
        {
            get
            {
                return _DonViChuQuan;
            }
            set
            {
                SetPropertyValue("DonViChuQuan", ref _DonViChuQuan, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị trực thuộc")]
        [Association("CoQuanThue-DonViTrucThuoc")]
        public XPCollection<CoQuanThue> DonViTrucThuoc
        {
            get
            {
                return GetCollection<CoQuanThue>("DonViTrucThuoc");
            }
        }

        public CoQuanThue(Session session) : base(session) { }

        IBindingList ITreeNode.Children
        {
            get { return DonViTrucThuoc; }
        }

        string ITreeNode.Name
        {
            get { return TenCoQuanThue; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return DonViChuQuan; }
        }
    }
}
