using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using DevExpress.Persistent.Base;
namespace PSC_HRM.Module.PMS.NonPersistentObjects.NEU
{
    [NonPersistent]
    [ModelDefault("Caption", "Quản lý dữ liệu chưa thanh toán")]
    public class QuanLyDuLieuChuaThanhToan : BaseObject
    {
        private NamHoc _NamHoc;
        [Browsable(false)]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { _NamHoc = value; }
        }
        private HocKy _HocKy;
        [Browsable(false)]
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { _HocKy = value; }
        }
        private BoPhan _BoMonQuanLy;
        [ModelDefault("Caption", "Bộ môn quản lý")]
        public BoPhan BoMonQuanLy
        {
            get { return _BoMonQuanLy; }
            set
            {
                _BoMonQuanLy = value;
                if (!IsLoading && value != null)
                {
                    LoadDuLieu();
                }
            }
        }
        [ModelDefault("Caption", "Danh sách phân công")]
        public XPCollection<ChiTietDuLieuChuaThanhToan> LisDanhSach
        {
            get;
            set;
        }
        public QuanLyDuLieuChuaThanhToan(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public void LoadDuLieu()
        {
            if(LisDanhSach != null)
            {
                LisDanhSach.Reload();
            }
            else
            {
                LisDanhSach = new XPCollection<ChiTietDuLieuChuaThanhToan>(Session, false);
            }
            //Lấy danh sách số parameter để truyền dữ liệu 
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
            param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
            param[2] = new SqlParameter("@BoMonQuanLy", BoMonQuanLy != null ? BoMonQuanLy.Oid : Guid.Empty);

            DataTable dt = DataProvider.GetDataTable("spd_PMS_XemDuLieuChuaThanhToan", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ChiTietDuLieuChuaThanhToan ds = new ChiTietDuLieuChuaThanhToan(Session);
                    ds.Oid = new Guid(item["Oid"].ToString());
                    ds.MaMonHoc = item["MaMonHoc"].ToString();
                    ds.TenMonHoc = item["TenMonHoc"].ToString();
                    ds.MaLopHocPhan = item["MaLopHocPhan"].ToString();
                    ds.LopHocPhan = item["LopHocPhan"].ToString();
                    ds.MaLopSV = item["MaLopSV"].ToString();
                    ds.TenLopSV = item["TenLopSV"].ToString();
                    ds.SiSo = item["SiSo"].ToString();
                    ds.HeDaoTao = item["HeDaoTao"].ToString();
                    ds.BacDaoTao = item["BacDaoTao"].ToString();
                    ds.MaBoPhan = item["MaBoPhan"].ToString();
                    ds.MaGV = item["MaGV"].ToString();
                    ds.TenGV = item["TenGV"].ToString();
                    ds.SoTinChi = item["SoTinChi"].ToString();
                    ds.SoTietDungLop = item["SoTietDungLop"].ToString();
                    LisDanhSach.Add(ds);
                }
            }
        }
    }
}