using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS
{
    [ModelDefault("Caption", "Thông tin chung PMS")]

    [DefaultProperty("ThongTin")]
    public class ThongTinChungPMS : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private KyTinhPMS _KyTinhPMS;

        [ModelDefault("Caption", "Trường")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")] 
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if(!IsLoading)
                {
                    HocKy = null;
                    updateHocKyList(); 
                    updateKyPMS();
                }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        //[RuleUniqueValue(DefaultContexts.Save, TargetCriteria = "ThongTinTruong.TenVietTat='HUFLIT'")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if(value != null)
                {
                    AfterLoadDotTinhChanged();
                }
            }
        }
        [ModelDefault("Caption", "Kỳ tính PMS")]
        [DataSourceProperty("KyTinhPMSList")]
        [VisibleInListView(false)]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Kỳ PMS List")]
        public XPCollection<KyTinhPMS> KyTinhPMSList
        {
            get;
            set;
        }
        void updateKyPMS()
        {
            if (NamHoc != null)
            {
                KyTinhPMSList = new XPCollection<KyTinhPMS>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
            }
            else
                KyTinhPMSList = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            OnChanged("KyTinhPMSList");
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            if (HocKyList != null)
                HocKyList.Reload();
            else
                HocKyList = new XPCollection<HocKy>(Session);
            if (NamHoc != null)
            {
                HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
                SortingCollection sortHK = new SortingCollection();
                sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
                HocKyList.Sorting = sortHK;
                //OnChanged("HocKyList");
            }
        }

        
        public ThongTinChungPMS(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.

            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (NamHoc != null)
                updateHocKyList();
        }
        protected virtual void AfterLoadDotTinhChanged()
        {
        }
    }

}