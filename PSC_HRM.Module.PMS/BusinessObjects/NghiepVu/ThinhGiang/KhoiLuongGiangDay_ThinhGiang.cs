using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Quản lý đào tạo (thỉnh giảng)")]
    [DefaultProperty("Caption")]
    [Appearance("Hide_HeSo", TargetItems = "KyTinhPMS", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    public class KhoiLuongGiangDay_ThinhGiang : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao_ThinhGiang _BangChotThuLao;
        private DotTinhPMS _DotTinh;

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao_ThinhGiang BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }

        [ModelDefault("Caption", "Đợt tính")]
        [DataSourceProperty("DotTinhPMSList")]
        [VisibleInListView(false)]
        public DotTinhPMS DotTinh
        {
            get { return _DotTinh; }
            set { SetPropertyValue("DotTinh", ref _DotTinh, value); }
        }
      
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoiLuongGiangDay_ThinhGiang")]
        [ModelDefault("Caption", "Chi tiết đào tạo")]
        public XPCollection<ChiTietKhoiLuongGiangDay_Moi> ListChiTietKhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay_Moi>("ListChiTietKhoiLuongGiangDay_ThinhGiang");
            }
        }
        
        [Browsable(false)]
        [ModelDefault("Caption", "Đợt PMS List")]
        public XPCollection<DotTinhPMS> DotTinhPMSList
        {
            get;
            set;
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
               return String.Format(" {0} {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", DotTinh != null ? " - Đợt " + DotTinh.Dot.ToString() : "");
            }
        }
        public KhoiLuongGiangDay_ThinhGiang(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DotTinhPMSList = new XPCollection<DotTinhPMS>(Session, false);
        }

        public void UpdateDotTinh()
        {
            if (HocKy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HocKy=?", HocKy.Oid);
                if (DotTinhPMSList != null)
                {
                    DotTinhPMSList.Reload();

                    XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);
                    foreach (DotTinhPMS item in DS_List)
                    {
                        DotTinhPMSList.Add(item);
                    }
                }
            }      
        }

        protected override void AfterLoadDotTinhChanged()
        {
            UpdateDotTinh();
        }
       
    }
}
