using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuViec
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý thử việc")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinTruong;NamHoc;Dot")]
    //[Appearance("QuanLyDanhGia", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]
    public class QuanLyThuViec : BaoMatBaseObject
    {
        // Fields...
        private int _Dot;
        private NamHoc _NamHoc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                if (!IsLoading && value != null)
                {
                    object count = Session.Evaluate<QuanLyThuViec>(CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("NamHoc=?", value.Oid));
                    if (count != null)
                        Dot = (int)count + 1;
                    else
                        Dot = 1;
                }
            }
        }

        [ModelDefault("Caption", "Đợt")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public string Caption
        {
            get
            {
                if (NamHoc != null)
                    return string.Format("Năm học {0} đợt {1}", NamHoc.TenNamHoc, Dot);
                return "";
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách đề nghị xếp lương")]
        [Association("QuanLyThuViec-ListChiTietDeNghiXepLuong")]
        public XPCollection<ChiTietDeNghiXepLuong> ListChiTietDeNghiXepLuong
        {
            get
            {
                return GetCollection<ChiTietDeNghiXepLuong>("ListChiTietDeNghiXepLuong");
            }
        }

        public QuanLyThuViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }
}
