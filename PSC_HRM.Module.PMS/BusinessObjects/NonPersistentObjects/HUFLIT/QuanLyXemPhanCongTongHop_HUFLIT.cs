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

namespace PSC_HRM.Module.PMS.NonPersistentObjects.HUFLIT
{
    [NonPersistent]
    [ModelDefault("Caption", "Quản lý tổng hợp phân công giảng dạy(1)")]
    public class QuanLyXemPhanCongTongHop_HUFLIT : BaseObject
    {
        [ModelDefault("Caption", "Danh sách phân công")]
        public XPCollection<ChiTietPhanCongTongHop_HUFLIT> LisDanhSach
        {
            get;
            set;
        }
        public QuanLyXemPhanCongTongHop_HUFLIT(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public void LoadDuLieu(NamHoc NamHoc, HocKy HocKy)
        {
            if(LisDanhSach != null)
            {
                LisDanhSach.Reload();
            }
            else
            {
                LisDanhSach = new XPCollection<ChiTietPhanCongTongHop_HUFLIT>(Session, false);
            }
            //Lấy danh sách số parameter để truyền dữ liệu 
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);

            DataTable dt = DataProvider.GetDataTable("spd_PMS_XemDuLieuThoiKhoaBieuTongHop", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ChiTietPhanCongTongHop_HUFLIT ds = new ChiTietPhanCongTongHop_HUFLIT(Session);
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
                    ds.SoTiet = item["SoTiet"].ToString();
                    ds.TrangThai = item["TrangThai"].ToString();
                    LisDanhSach.Add(ds);
                }
            }
        }
    }
}