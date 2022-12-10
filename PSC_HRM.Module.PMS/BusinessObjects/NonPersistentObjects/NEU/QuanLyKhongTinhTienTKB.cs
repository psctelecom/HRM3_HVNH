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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.NEU
{
    [NonPersistent]
    [ModelDefault("Caption", "Quản lý tổng hợp không tính tiền")]
    public class QuanLyKhongTinhTienTKB : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set
            {
                _BacDaoTao = value;
            }
        }

        private HeDaoTao _HeDaoTao;
        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set
            {
                _HeDaoTao = value;
            }
        }
       
        private bool _KhongTinhTien;
        [ModelDefault("Caption", "Không tính tiền")]
        [ImmediatePostData]
        public bool KhongTinhTien
        {
            get { return _KhongTinhTien; }
            set
            {
                _KhongTinhTien = value;
            }
        }

        private NamHoc _NamHoc;
        [Browsable(false)]
        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                _NamHoc = value;

            }
        }

        private HocKy _HocKy;
        [Browsable(false)]
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                _HocKy = value;

            }
        }

        [ModelDefault("Caption", "Danh sách không tính tiền")]
        public XPCollection<ChiTietQuanLyKhongTinhTienTKB> LisDanhSach
        {
            get;
            set;
        }
        public QuanLyKhongTinhTienTKB(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public void LoadDuLieu()
        {
            if (HeDaoTao != null)
            {
                if (LisDanhSach != null)
                {
                    LisDanhSach.Reload();
                }
                else
                {
                    LisDanhSach = new XPCollection<ChiTietQuanLyKhongTinhTienTKB>(Session, false);
                }
                //Lấy danh sách số parameter để truyền dữ liệu 
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@HocKy", HocKy.Oid);
                param[2] = new SqlParameter("@HeDaoTao", HeDaoTao.Oid);
                param[3] = new SqlParameter("@BacDaoTao", BacDaoTao.Oid);


                DataTable dt = DataProvider.GetDataTable("spd_PMS_LayDuLieuKhongTinhTien", System.Data.CommandType.StoredProcedure, param);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ChiTietQuanLyKhongTinhTienTKB ds = new ChiTietQuanLyKhongTinhTienTKB(Session);
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
                        ds.KhongTinTien = Convert.ToBoolean(item["KhongTinhTien"]);
                        ds.Oid = Guid.Parse(item["Oid"].ToString());
                        ds.KhoaHoc = item["KhoaHoc"].ToString();
                        LisDanhSach.Add(ds);
                    }
                }
            }
        }
    }
}