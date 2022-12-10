using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DoanDang
{
    [DefaultClassOptions]
    [DefaultProperty("TenToChucDoanThe")]
    [ModelDefault("Caption", "Tổ chức Đoàn thể")]
    [RuleCombinationOfPropertiesIsUnique("ToChucDoanThe.Unique", DefaultContexts.Save, "MaQuanLy;TenToChucDoanThe")]
    public class ToChucDoanThe : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        public ToChucDoanThe(Session session) : base(session) { }

        // Fields...
        private PhanLoaiToChucDoanThe _PhanLoai;
        private ToChucDoanThe _ToChucDoanTheCha;
        private string _TenToChucDoanThe;
        private string _MaQuanLy;

        [ModelDefault("Caption", "Mã quản lý")]
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

        [ModelDefault("Caption", "Tên tổ chức Đoàn thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenToChucDoanThe
        {
            get
            {
                return _TenToChucDoanThe;
            }
            set
            {
                SetPropertyValue("TenToChucDoanThe", ref _TenToChucDoanThe, value);
            }
        }

        [ModelDefault("Caption", "Phân loại Đoàn thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanLoaiToChucDoanThe PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        [ModelDefault("Caption", "Trực thuộc Đoàn thể")]
        [Association("ToChucDoanThe-ToChucDoanTheCon")]
        public ToChucDoanThe ToChucDoanTheCha
        {
            get
            {
                return _ToChucDoanTheCha;
            }
            set
            {
                SetPropertyValue("ToChucDoanTheCha", ref _ToChucDoanTheCha, value);
            }
        }

        [ModelDefault("Caption", "Danh sách Đoàn thể trực thuộc")]
        [Association("ToChucDoanThe-ToChucDoanTheCon")]
        public XPCollection<ToChucDoanThe> ToChucDoanTheCon
        {
            get
            {
                return GetCollection<ToChucDoanThe>("ToChucDoanTheCon");
            }
        }

        [Browsable(false)]
        [Association("ToChucDoanThe-DoanTheList")]
        public XPCollection<DoanThe> DoanTheList
        {
            get
            {
                return GetCollection<DoanThe>("DoanTheList");
            }
        }

        IBindingList ITreeNode.Children
        {
            get { return ToChucDoanTheCon; }
        }

        string ITreeNode.Name
        {
            get { return TenToChucDoanThe; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return ToChucDoanTheCha; }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
    }

}
