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

namespace PSC_HRM.Module.PMS.NonPersistentObjects.NEU
{
    [NonPersistent]
    [ModelDefault("Caption", "Quản lý tổng hợp phân công giảng dạy")]
    public class QuanLyXemPhanCongTongHop : BaseObject
    {
        [ModelDefault("Caption", "Danh sách phân công")]
        public XPCollection<ChiTietPhanCongTongHop> LisDanhSach
        {
            get;
            set;
        }
        public QuanLyXemPhanCongTongHop(Session session)
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
                LisDanhSach = new XPCollection<ChiTietPhanCongTongHop>(Session, false);
            }
            //Lấy danh sách số parameter để truyền dữ liệu 
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
            param[1] = new SqlParameter("@HocKy", HocKy.Oid);

            DataTable dt = DataProvider.GetDataTable("spd_PMS_XemDuLieuThoiKhoaBieuTongHop", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ChiTietPhanCongTongHop ds = new ChiTietPhanCongTongHop(Session);
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
                    ds.SoTietHeThong = item["SoTietHeThong"].ToString();
                    ds.TrangThai = item["TrangThai"].ToString();
                    ds.KhoaHoc = item["KhoaHoc"].ToString();
                    LisDanhSach.Add(ds);
                }
            }
        }
    }
}