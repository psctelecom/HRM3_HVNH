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
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NghiepVu.HUFLIT
{

    [ModelDefault("Caption", "Danh sách môn học đơn giá thanh toán")]
    [NonPersistent]
    [DefaultProperty("Caption")]
    public class DanhSachDonGiaThanhToanMonHoc : BaseObject
    {
        #region Khai báo nhân viên
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        #endregion


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        //[ModelDefault("AllowEdit", "false")]
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
                if (value != null)
                {
                    HocKy = null;
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        //[ModelDefault("AllowEdit", "false")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if(!IsLoading && value != null)
                {
                    UpdateListNhanVien();
                    NhanVien = null;
                }
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        //[DataSourceProperty("ListNhanVien")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Danh sách chi tiết")]
        public XPCollection<ChiTietDanhSachDanhSachDonGiaThanhToan> ListDanhSach
        {
            get;
            set;
        }
        

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [NonPersistent]
        public XPCollection<NhanVien> ListNhanVien
        {
            get;
            set;
        }

        public DanhSachDonGiaThanhToanMonHoc(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //UpdateListNhanVien();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }


        public void UpdateListNhanVien()
        {
            if(BoPhan != null)
            {
                if (ListNhanVien != null)
                    ListNhanVien.Reload();
                else
                    ListNhanVien = new XPCollection<NhanVien>(Session, false);

                CriteriaOperator filter = CriteriaOperator.Parse("BoPhan = ?", BoPhan.Oid);
                XPCollection<NhanVien> ds = new XPCollection<NhanVien>(Session, filter);
                foreach(NhanVien item in ds)
                {
                    ListNhanVien.Add(item);
                }
            }
            else
            {
                ListNhanVien = new XPCollection<NhanVien>(Session);
            }
        }

        public void LoadData()
        {
            if (NamHoc != null)
            {
                if (ListDanhSach != null)
                    ListDanhSach.Reload();
                else
                    ListDanhSach = new XPCollection<ChiTietDanhSachDanhSachDonGiaThanhToan>(Session, false);


                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid :  Guid.Empty);
                param[2] = new SqlParameter("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                SqlCommand cmd = DataProvider.GetCommand("spd_PMS_HUFLIT_DanhSachDonGiaThanhToan", CommandType.StoredProcedure, param);
                DataSet dataset = DataProvider.GetDataSet(cmd);
                if (dataset != null)
                {
                    DataTable dt = dataset.Tables[0];
                    foreach(DataRow itemRow in dt.Rows)
                    {
                        ChiTietDanhSachDanhSachDonGiaThanhToan ds = new ChiTietDanhSachDanhSachDonGiaThanhToan(Session);
                        ds.NhanVien = Guid.Parse(itemRow["NhanVien"].ToString());
                        ds.TenNhanVien = itemRow["TenNhanVien"].ToString();
                        ds.LoaiMonHoc = Guid.Parse(itemRow["LoaiMonHoc"].ToString());
                        ds.TenLoaiMonHoc = itemRow["TenLoaiMonHoc"].ToString();
                        ds.BacDaoTao = Guid.Parse(itemRow["BacDaoTao"].ToString());
                        ds.TenBacDaoTao = itemRow["TenBacDaoTao"].ToString();
                        ds.HeDaoTao = Guid.Parse(itemRow["HeDaoTao"].ToString()); 
                        ds.TenHeDaoTao = itemRow["TenHeDaoTao"].ToString();
                        ds.MaMonhoc = itemRow["MaMonhoc"].ToString();
                        ds.TenMonHoc = itemRow["TenMonHoc"].ToString();
                        ds.LopHocPhan = itemRow["LopHocPhan"].ToString();
                        ds.LopSinhVien = itemRow["LopSinhVien"].ToString();
                        ds.DonGia = Convert.ToDecimal(itemRow["DonGia"]);
                        ListDanhSach.Add(ds);
                    }
                }
            }
        }
    }
}