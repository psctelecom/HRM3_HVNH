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
    [ModelDefault("Caption", "Tổ chức Đoàn")]
    [DefaultProperty("TenToChucDoan")]
    [RuleCombinationOfPropertiesIsUnique("ToChucDoan.Unique", DefaultContexts.Save, "MaQuanLy;TenToChucDoan")]
    public class ToChucDoan : BaseObject, ITreeNode, ITreeNodeImageProvider
    {
        public ToChucDoan(Session session) : base(session) { }

        // Fields...
        private PhanLoaiToChucDoan _PhanLoai;
        private ToChucDoan _ToChucDoanCha;
        private string _TenToChucDoan;
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

        [ModelDefault("Caption", "Tên tổ chức Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenToChucDoan
        {
            get
            {
                return _TenToChucDoan;
            }
            set
            {
                SetPropertyValue("TenToChucDoan", ref _TenToChucDoan, value);
            }
        }

        [ModelDefault("Caption", "Phân loại tổ chức Đoàn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanLoaiToChucDoan PhanLoai
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

        [ModelDefault("Caption", "Trực thuộc tổ chức Đoàn")]
        [Association("ToChucDoan-ToChucDoanCon")]
        public ToChucDoan ToChucDoanCha
        {
            get
            {
                return _ToChucDoanCha;
            }
            set
            {
                SetPropertyValue("ToChucDoanCha", ref _ToChucDoanCha, value);
            }
        }

        [ModelDefault("Caption", "Các tổ chức Đoàn trực thuộc")]
        [Association("ToChucDoan-ToChucDoanCon")]
        public XPCollection<ToChucDoan> ToChucDoanCon
        {
            get
            {
                return GetCollection<ToChucDoan>("ToChucDoanCon");
            }
        }

        [Browsable(false)]
        [Association("ToChucDoan-DoanVienList")]
        public XPCollection<DoanVien> DoanVienList
        {
            get
            {
                return GetCollection<DoanVien>("DoanVienList");
            }
        }



        IBindingList ITreeNode.Children
        {
            get { return ToChucDoanCon; }
        }

        string ITreeNode.Name
        {
            get { return TenToChucDoan; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return ToChucDoanCha; }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }
    }

}
