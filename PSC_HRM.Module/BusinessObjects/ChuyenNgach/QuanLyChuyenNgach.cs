using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ChuyenNgach
{
    [DefaultClassOptions]
    [DefaultProperty("NamHienThi")]
    [ImageName("BO_ChuyenNgach")]
    [ModelDefault("Caption", "Quản lý chuyển ngạch")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyChuyenNgach", DefaultContexts.Save, "ThongTinTruong;Nam")]
    public class QuanLyChuyenNgach : BaoMatBaseObject
    {
        // Fields...
        private int _Nam;

        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("Caption", "Năm")]
        [RuleUniqueValue(DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (IsLoading)
                {
                    NamHienThi = Convert.ToString(_Nam);
                }
            }
        }

        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Năm")]
        public string NamHienThi { get; set; }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách đề nghị chuyển ngạch")]
        [Association("QuanLyChuyenNgach-ListDeNghiChuyenNgach")]
        public XPCollection<DeNghiChuyenNgach> ListDeNghiChuyenNgach
        {
            get
            {
                return GetCollection<DeNghiChuyenNgach>("ListDeNghiChuyenNgach");
            }
        }

        public QuanLyChuyenNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = HamDungChung.GetServerTime().Year;
        }
    }

}
