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
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;


namespace PSC_HRM.Module.PMS.DanhMuc
{

    [ModelDefault("Caption", "Hệ số chức danh (Nhân viên)")]
    [DefaultProperty("Caption")]
    [ModelDefault("IsCloneable", "True")]
    public class HeSoChucDanh_NhanVien : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        #region Check khởi tạo
        private bool _KT;
        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty("HeSoChucDanh_NhanVien.KT", DefaultContexts.Save, "Bảng hệ số chức danh nhân viên đã tồn tại!", SkipNullOrEmptyValues = false, UsedProperties = "NamHoc; HocKy")]
        public bool KT
        {
            get
            {
                return !_KT;
            }
            set
            {
                SetPropertyValue("KT", ref _KT, value);
            }
        }
        void Check()
        {
            #region Store
            if (NamHoc != null)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
                object obj = DataProvider.GetValueFromDatabase("spd_PMS_KiemTraBangHeSoChucDanhNhanVien", CommandType.StoredProcedure, param);
                if (Convert.ToInt32(obj.ToString()) != 0)
                {
                    KT = true;//không dc Save
                }
                else
                    KT = false;//Dc save
            }
            #endregion
        }
        #endregion
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                    if (NamHoc != null)
                    {
                        Check();
                        updateHocKyList();
                    }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [ImmediatePostData]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (!IsLoading)
                    if (NamHoc != null)
                    {
                        Check();
                        updateHocKyList();
                    }
            }
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }

        public void updateHocKyList()
        {
            HocKyList = new XPCollection<HocKy>(Session);
            if (NamHoc != null)
                HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }

        [Aggregated]
        [Association("HeSoChucDanh_NhanVien-ListDanhSachHeSoChucDanh_NhanVien")]
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<DanhSachHeSoChucDanh_NhanVien> ListDanhSachHeSoChucDanh_NhanVien
        {
            get
            {
                return GetCollection<DanhSachHeSoChucDanh_NhanVien>("ListDanhSachHeSoChucDanh_NhanVien");
            }
        }
        public HeSoChucDanh_NhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }
}