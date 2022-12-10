using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace PSC_HRM.Module.HopDong
{
    [DefaultClassOptions]
    [ImageName("BO_Contract")]
    [DefaultProperty("NamHoc")]
    [ModelDefault("Caption", "Quản lý hợp đồng")]

    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc",TargetCriteria = "MaTruong != 'UEL'")]
   
    public class QuanLyHopDong : BaoMatBaseObject
    {
        private NamHoc _NamHoc;
        private int _Nam;

        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleUniqueValue("", DefaultContexts.Save,TargetCriteria = "MaTruong == 'UEL'")]
        [RuleRequiredField("", DefaultContexts.Save,TargetCriteria = "MaTruong == 'UEL'")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleUniqueValue("", DefaultContexts.Save,TargetCriteria = "MaTruong != 'UEL'")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách hợp đồng")]
        [Association("QuanLyHopDong-ListHopDong")]
        public XPCollection<HopDong_NhanVien> ListHopDong
        {
            get
            {
                return GetCollection<HopDong_NhanVien>("ListHopDong");
            }
        }

        private string MaTruong { get; set; }
        public QuanLyHopDong(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;
         
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);

            //
            MaTruong = TruongConfig.MaTruong;
        }
    }

}
