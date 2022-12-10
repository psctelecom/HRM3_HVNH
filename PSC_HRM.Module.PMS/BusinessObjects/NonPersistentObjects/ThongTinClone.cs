using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption","Thông tin")]
    [Appearance("ThongTinClone_Hide", TargetItems = "HocKy", Visibility = ViewItemVisibility.Hide, Criteria = "An")]
    public class ThongTinClone : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _An;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                if (!IsLoading)
                    if (NamHoc != null)
                    {
                        HocKy = null;
                        updateHocKyList();
                    }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "ẨN")]
        [Browsable(false)]
        public bool An
        {
            get { return _An; }
            set { SetPropertyValue("An", ref _An, value); }
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            if (HocKyList != null)
                HocKyList.Reload();
            else
                HocKyList = new XPCollection<HocKy>(Session);

            HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }
        public ThongTinClone(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }

}