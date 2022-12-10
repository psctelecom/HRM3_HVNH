using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BoiDuong
{
    [DefaultClassOptions]
    [DefaultProperty("NamHoc")]
    [ImageName("BO_QuanLyBoiDuong")]
    [ModelDefault("Caption", "Quản lý bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc")]
    //[Appearance("QuanLyBoiDuong", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyBoiDuong : BaoMatBaseObject
    {
        // Fields...
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        //các khoa sẽ đăng ký ở đây
        [Aggregated]
        [ModelDefault("Caption", "Đăng ký bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListDangKyBoiDuong")]
        public XPCollection<DangKyBoiDuong> ListDangKyBoiDuong
        {
            get
            {
                return GetCollection<DangKyBoiDuong>("ListDangKyBoiDuong");
            }
        }

        //danh sách duyệt trình hiệu trưởng ký
        [Aggregated]
        [ModelDefault("Caption", "Duyệt cử đi bồi dưỡng")]
        [Association("QuanLyBoiDuong-ListDuyetDangKyBoiDuong")]
        public XPCollection<DuyetDangKyBoiDuong> ListDuyetDangKyBoiDuong
        {
            get
            {
                return GetCollection<DuyetDangKyBoiDuong>("ListDuyetDangKyBoiDuong");
            }
        }

        public QuanLyBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }

}
