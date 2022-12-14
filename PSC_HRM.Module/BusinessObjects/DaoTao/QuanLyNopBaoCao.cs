using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DaoTao
{
    [DefaultClassOptions]
    [DefaultProperty("Nam")]
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Quản lý nộp báo cáo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;Nam")]
    public class QuanLyNopBaoCao : BaoMatBaseObject
    {
        // Fields...
        private int _Nam;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Aggregated]
        [ModelDefault("Caption", "Đăng ký nộp báo cáo")]
        [Association("QuanLyNopBaoCao-ListDangKyNopBaoCao")]
        public XPCollection<DangKyNopBaoCao> ListDangKyNopBaoCao
        {
            get
            {
                return GetCollection<DangKyNopBaoCao>("ListDangKyNopBaoCao");
            }
        }
        public QuanLyNopBaoCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Nam = HamDungChung.GetServerTime().Year;
        }
    }

}
