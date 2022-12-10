using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.CauHinh;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    //[ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS(UEL)")]

    public class CauHinhQuyDoiPMS_UEL : BaseObject
    {
        #region KhaiBao
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BacDaoTao _BacDaoTao;

        #region Công thức
        private string _DonGiaThuLaoGiangDay;
        private string _CongThucTinhThuLaoGiangDay;
        private string _CongThucTongHeSo;
        private string _CongThucTinhTietThucThanh;
        private string _CongThucTinhTietTroGiang;
        private string _CongThucDonGiaDieuChinh;
        private string _CongThucDonGiaQuyDoiTroGiang;
        private string _CongThucDonGiaQuyDoi;
        #endregion

        #endregion

        #region Thông tin
        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "False")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        //[VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                    updateHocKyList();
            }
        }
        
        [ModelDefault("Caption", "Học kỳ")]

        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
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
        
        
        [ModelDefault("Caption", "Bậc đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        [Browsable(false)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        #endregion

        #region Công thức
        private string ExpressionType
        {
            get
            {
                return "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_UEL";
            }
        }
        
        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính đơn giá tiết chuẩn")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string DonGiaThuLaoGiangDay
        {
            get
            {
                return _DonGiaThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("DonGiaThuLaoGiangDay", ref _DonGiaThuLaoGiangDay, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính thù lao giảng dạy")]
       // [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhThuLaoGiangDay
        {
            get
            {
                return _CongThucTinhThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("CongThucTinhThuLaoGiangDay", ref _CongThucTinhThuLaoGiangDay, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tổng hệ số")]
       // [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTongHeSo
        {
            get
            {
                return _CongThucTongHeSo;
            }
            set
            {
                SetPropertyValue("CongThucTongHeSo", ref _CongThucTongHeSo, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tiết thực hành")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTietThucThanh
        {
            get
            {
                return _CongThucTinhTietThucThanh;
            }
            set
            {
                SetPropertyValue("CongThucTinhTietThucThanh", ref _CongThucTinhTietThucThanh, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức tính tiết trợ giảng")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucTinhTietTroGiang
        {
            get
            {
                return _CongThucTinhTietTroGiang;
            }
            set
            {
                SetPropertyValue("CongThucTinhTietTroGiang", ref _CongThucTinhTietTroGiang, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức đơn giá điều chỉnh")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucDonGiaDieuChinh
        {
            get
            {
                return _CongThucDonGiaDieuChinh;
            }
            set
            {
                SetPropertyValue("CongThucDonGiaDieuChinh", ref _CongThucDonGiaDieuChinh, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Công thức đơn giá quy đổi trợ giảng")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucDonGiaQuyDoiTroGiang
        {
            get
            {
                return _CongThucDonGiaQuyDoiTroGiang;
            }
            set
            {
                SetPropertyValue("CongThucDonGiaQuyDoiTroGiang", ref _CongThucDonGiaQuyDoiTroGiang, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Công thức đơn giá quy đổi")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FormulaPMSEditor")]
        [VisibleInListView(false)]
        public string CongThucDonGiaQuyDoi
        {
            get
            {
                return _CongThucDonGiaQuyDoi;
            }
            set
            {
                SetPropertyValue("CongThucDonGiaQuyDoi", ref _CongThucDonGiaQuyDoi, value);
            }
        }

        public CauHinhQuyDoiPMS_UEL(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        protected override void OnSaving()
        {
            base.OnSaving();
        }
        #endregion
    }
}