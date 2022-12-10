using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
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
    [ModelDefault("Caption", "Tổ chức Đảng")]
    [DefaultProperty("TenToChucDang")]
    public class ToChucDang : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        public ToChucDang(Session session) : base(session) { }

        // Fields...
        private PhanLoaiToChucDang _PhanLoaiToChucDang;
        private ToChucDang _ToChucDangCha;
        private string _TenToChucDang;
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

        [ModelDefault("Caption", "Tên tổ chức Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenToChucDang
        {
            get
            {
                return _TenToChucDang;
            }
            set
            {
                SetPropertyValue("TenToChucDang", ref _TenToChucDang, value);
            }
        }

        [ModelDefault("Caption", "Phân loại tổ chức Đảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanLoaiToChucDang PhanLoaiToChucDang
        {
            get
            {
                return _PhanLoaiToChucDang;
            }
            set
            {
                SetPropertyValue("PhanLoaiToChucDang", ref _PhanLoaiToChucDang, value);
            }
        }

        [ModelDefault("Caption", "Trực thuộc tổ chức Đảng")]
        [Association("ToChucDang-ToChucDangCon")]
        [VisibleInListView(false)]
        public ToChucDang ToChucDangCha
        {
            get
            {
                return _ToChucDangCha;
            }
            set
            {
                SetPropertyValue("ToChucDangCha", ref _ToChucDangCha, value);
            }
        }

        [ModelDefault("Caption", "Danh sách tổ chức Đảng trực thuộc")]
        [Association("ToChucDang-ToChucDangCon")]
        public XPCollection<ToChucDang> ToChucDangCon
        {
            get
            {
                return GetCollection<ToChucDang>("ToChucDangCon");
            }
        }

        [Browsable(false)]
        [Association("ToChucDang-DangVienList")]
        public XPCollection<DangVien> DangVienList
        {
            get
            {
                return GetCollection<DangVien>("DangVienList");
            }
        }

        IBindingList ITreeNode.Children
        {
            get { return ToChucDangCon; }
        }

        string ITreeNode.Name
        {
            get { return TenToChucDang; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return ToChucDangCha; }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
    }

}
