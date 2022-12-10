using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Quản lý xem thời khóa biểu")]
    public class QuanLyXemTKBGV : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private BoPhan _DonVi;
        private NhanVien _NhanVien;


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
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
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Đơn Vị")]
        [ImmediatePostData]
        public BoPhan DonVi
        {
            get { return _DonVi; }
            set
            {
                SetPropertyValue("DonVi", ref _DonVi, value);
                if(!IsLoading && value != null)
                {
                    LoadNhanVien();
                }
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [DataSourceProperty("ListNhanVien")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [Browsable(false)]
        public XPCollection<NhanVien> ListNhanVien { get; set; }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<DanhSachThongTinThoiKhoaBieuGiangVien> ListDanhSach { get; set; }

        [Action(Caption = "Lấy thông tin", ImageName = "Action_Import", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void LayThongTinChiTiet()
        {
                LoadData();
        }

        public QuanLyXemTKBGV(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoadNhanVien();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            // Place here your initialization code.
        }

        private void LoadNhanVien()
        {
            if (ListNhanVien != null)
                ListNhanVien.Reload();
            else
                ListNhanVien = new XPCollection<NhanVien>(Session, false);

            if(DonVi != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("BoPhan = ?", DonVi.Oid);
                XPCollection<NhanVien> list = new XPCollection<NhanVien>(Session, filter);
                foreach(NhanVien item in list)
                {
                    ListNhanVien.Add(item);
                }
            }
            else
            {
                XPCollection<NhanVien> list = new XPCollection<NhanVien>(Session);
                foreach (NhanVien item in list)
                {
                    ListNhanVien.Add(item);
                }
            }
        }

        public void LoadData()
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
            param[2] = new SqlParameter("@DonVi", DonVi != null ? DonVi.Oid : Guid.Empty);
            param[3] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
            param[4] = new SqlParameter("@MaHocPhan", "");

            SqlCommand cmd = DataProvider.GetCommand("PMS_XemLichGiangDayCuaGV", CommandType.StoredProcedure, param);
            DataSet dataset = DataProvider.GetDataSet(cmd);
            if (dataset != null)
            {
                DataTable dt = dataset.Tables[0];
                if (ListDanhSach != null)
                    ListDanhSach.Reload();
                else
                    ListDanhSach = new XPCollection<DanhSachThongTinThoiKhoaBieuGiangVien>(Session, false);

                if (dt != null)
                {
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        DanhSachThongTinThoiKhoaBieuGiangVien ds = new DanhSachThongTinThoiKhoaBieuGiangVien(Session);
                        ds.LopHocPhan = itemRow["LopHocPhan"].ToString();
                        ds.MaHocPhan = itemRow["MaHocPhan"].ToString();
                        ds.TenHocPhan = itemRow["TenHocPhan"].ToString();
                        ds.LoaiHocPhan = itemRow["LoaiHocPhan"].ToString();
                        ds.TinChi = Convert.ToInt32(itemRow["TinChi"].ToString());
                        ds.SiSo = Convert.ToInt32(itemRow["SiSo"].ToString());
                        ds.MaGV = itemRow["MaGV"].ToString();
                        ds.TenGV = itemRow["TenGV"].ToString();
                        ds.LopSinhVien = itemRow["LopSinhVien"].ToString();
                        ds.LichGiang = itemRow["LichGiang"].ToString();
                        ListDanhSach.Add(ds);

                    }
                }
            }
        }
    }

}