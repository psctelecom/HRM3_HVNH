using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Học phần - sau đại học")]
    [DefaultProperty("TenHocPhan")]
    public class HocPhan_SauDaiHoc : BaseObject
    {
        #region Thông tin chung
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
                if (!IsLoading)
                {
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
        [ModelDefault("Caption", "Kỳ tính PMS")]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            HocKyList = new XPCollection<HocKy>(Session);
            HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }
        #endregion

        private string _MaHocPhan_Chu;
        private int _MaHocPhan_So;
        private string _TenHocPhan;
        private decimal _SoTinChi;

        [ModelDefault("Caption", "Mã HP (Chữ)")]
        public string MaHocPhan_Chu
        {
            get { return _MaHocPhan_Chu; }
            set { SetPropertyValue("MaHocPhan_Chu", ref _MaHocPhan_Chu, value); }
        }
        [ModelDefault("Caption", "Mã HP (Số)")]
        public int MaHocPhan_So
        {
            get { return _MaHocPhan_So; }
            set { SetPropertyValue("MaHocPhan_So", ref _MaHocPhan_So, value); }
        }

        [ModelDefault("Caption", "Tên HP")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        public HocPhan_SauDaiHoc(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

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
    }

}