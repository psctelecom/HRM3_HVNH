using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;
using System.Data;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Danh sách chức năng")]
    [ImageName("Nav_HeThong")]
    [DefaultProperty("Caption")]
    [Appearance("LaThuMuc", TargetItems = "AppObject;LoaiView;ThuMucQuanLy", Visibility = ViewItemVisibility.Hide, Criteria = "LaThuMuc")]
    [Appearance("KhongLaThuMuc", TargetItems = "PhanHe", Visibility = ViewItemVisibility.Hide, Criteria = "!LaThuMuc")]
    [Appearance("LoaiView", TargetItems = "LoaiCustom", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiView != 3")]
    public class AppMenu : BaseObject
    {
        private ThongTinTruong _Truong;

        [ModelDefault("Caption", "Trường")]
        [NonPersistent]
        [Browsable(false)]
        public ThongTinTruong Truong
        {
            get
            {
                return _Truong;
            }
            set
            {
                SetPropertyValue("Truong", ref _Truong, value);
            }
        }
        private decimal _SoThuTu;
        private string _TenChucNang;
        private PhanHeEnum _PhanHe;
        private AppObject _AppObject;
        private LoaiViewEnum _LoaiView;
        private LoaiCustomEnum _LoaiCustom;
        private LoaiNavigation _LoaiNavigation;
        private bool _LaThuMuc;
        private string _HinhAnh;
        private AppMenu _ThuMucQuanLy;
        private bool _SuDung;

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "n3")]
        [ModelDefault("EditMask", "n3")]
        public decimal SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ModelDefault("Caption", "Tên chức năng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public String TenChucNang
        {
            get
            {
                return _TenChucNang;
            }
            set
            {
                SetPropertyValue("TenChucNang", ref _TenChucNang, value);
                if (!IsLoading)
                {
                    if (TenChucNang != string.Empty)
                        if (AppObject == null)
                        {
                            AppObject = Session.FindObject<AppObject>(CriteriaOperator.Parse("Caption =?", TenChucNang));
                        }
                }
            }
        }

        [ModelDefault("Caption", "Là thư mục")]
        [ImmediatePostData]
        public bool LaThuMuc
        {
            get
            {
                return _LaThuMuc;
            }
            set
            {
                SetPropertyValue("LaThuMuc", ref _LaThuMuc, value);
                if (LaThuMuc)
                {
                    AppObject = null;
                    ThuMucQuanLy = null;
                    HinhAnh = "BO_Folder";
                }
                else
                    HinhAnh = "BO_List";
            }
        }

        [ModelDefault("Caption", "Đối tượng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!LaThuMuc")]
        [ImmediatePostData]
        public AppObject AppObject
        {
            get
            {
                return _AppObject;
            }
            set
            {
                SetPropertyValue("AppObject", ref _AppObject, value);
                if (!IsLoading)
                {
                    if (AppObject != null && TenChucNang == string.Empty)
                        TenChucNang = AppObject.Caption;
                    //else
                    //    TenChucNang = "";
                }
            }
        }

        [ModelDefault("Caption", "Loại đối tượng")]
        public LoaiViewEnum LoaiView
        {
            get
            {
                return _LoaiView;
            }
            set
            {
                SetPropertyValue("LoaiView", ref _LoaiView, value);
            }
        }

        [ModelDefault("Caption", "Loại custom")]
        public LoaiCustomEnum LoaiCustom
        {
            get
            {
                return _LoaiCustom;
            }
            set
            {
                SetPropertyValue("LoaiCustom", ref _LoaiCustom, value);
            }
        }

        [ModelDefault("Caption", "Loại phần mềm")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public LoaiNavigation LoaiNavigation
        {
            get
            {
                return _LoaiNavigation;
            }
            set
            {
                SetPropertyValue("LoaiNavigation", ref _LoaiNavigation, value);
            }
        }

        [ModelDefault("Caption", "Thư mục quản lý")]
        [DataSourceProperty("AppMenuParentList")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!LaThuMuc")]
        public AppMenu ThuMucQuanLy
        {
            get
            {
                return _ThuMucQuanLy;
            }
            set
            {
                SetPropertyValue("ThuMucQuanLy", ref _ThuMucQuanLy, value);
                if (ThuMucQuanLy != null)
                {
                    PhanHe = ThuMucQuanLy.PhanHe;
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Phân hệ")]
        public PhanHeEnum PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        [ModelDefault("Caption", "Hình ảnh")]
        public String HinhAnh
        {
            get
            {
                return _HinhAnh;
            }
            set
            {
                SetPropertyValue("HinhAnh", ref _HinhAnh, value);
            }
        }

        [ModelDefault("Caption", "Sử dụng")]
        public bool SuDung
        {
            get
            {
                return _SuDung;
            }
            set
            {
                SetPropertyValue("SuDung", ref _SuDung, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Tên chức năng")]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public String Caption
        {
            get
            {
                string tenPhanHe = string.Empty;
                if (PhanHe == PhanHeEnum.NhanSu)
                {
                    tenPhanHe = "Nhân sự";
                }
                else if (PhanHe == PhanHeEnum.TaiChinh)
                {
                    tenPhanHe = "Tài chính";
                }
                else if (PhanHe == PhanHeEnum.DanhMuc)
                {
                    tenPhanHe = "Danh mục";
                }
                else if (PhanHe == PhanHeEnum.HeThong)
                {
                    tenPhanHe = "Hệ thống";
                }
                else if (PhanHe == PhanHeEnum.PMS)
                {
                    tenPhanHe = "PMS";
                }
                else
                {
                    tenPhanHe = "Bàn làm việc";
                }
                return String.Format("{0} - {1}", TenChucNang, tenPhanHe);
            }

        }

        public AppMenu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            Truong = HamDungChung.ThongTinTruong(Session);
            LoaiNavigation = LoaiNavigation.Win;
            LoaiView = LoaiViewEnum.ListView;
            PhanHe = PhanHeEnum.NhanSu;
            SuDung = true;
            HinhAnh = "BO_List";
            //
            UpdateAppMenuParentList();
        }
        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    //
        //    UpdateAppMenuParentList();
        //}
        [Browsable(false)]
        public XPCollection<AppMenu> AppMenuParentList { get; set; }

        public void UpdateAppMenuParentList()
        {
            if (AppMenuParentList == null)
                AppMenuParentList = new XPCollection<AppMenu>(Session);
            //
            if (AppMenuParentList != null)
                AppMenuParentList.Criteria = CriteriaOperator.Parse("LaThuMuc");
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            try
            {
                DataProvider.ExecuteNonQuery("spd_HRM_CapNhatSoThuTu_AppMenu", CommandType.StoredProcedure);
            }
            catch (Exception)
            { }
        }
    }
}