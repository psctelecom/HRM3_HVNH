using System;
using System.ComponentModel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{

    [ModelDefault("Caption", "Giảng viên tham gia hoạt động quản lý(Non)")]
    [DefaultProperty("Caption")]
    [NonPersistent]
    public class QuanLyHoatDongQuanLy_Non : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }

            set
            {
                _NamHoc = value;     
                if(value != null)
                {
                    HocKy = null;
                }          
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }

            set
            {
                _HocKy = value;
            }
        }

        [ModelDefault("Caption", "Danh sách chi tiết")]
        public XPCollection<ChiTietQuanLyHoatDongQuanLy_Non> DanhSach
        {
            get;
            set;
        }

        public QuanLyHoatDongQuanLy_Non(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        public void LoadData()
        {
            if (NamHoc != null)
            {
                if (DanhSach != null)
                    DanhSach.Reload();
                else
                    DanhSach = new XPCollection<ChiTietQuanLyHoatDongQuanLy_Non>(Session, false);

                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@HocKy", HocKy != null ? HocKy.Oid : Guid.Empty);
                param[2] = new SqlParameter("@BoPhan", HamDungChung.GetPhanQuyenBoPhan());
                SqlCommand cmd = DataProvider.GetCommand("spd_HoatDongQuanLyKhac_LayDuLieuTheoPhanQuyen", CommandType.StoredProcedure, param);
                DataSet dataset = DataProvider.GetDataSet(cmd);
                if (dataset != null)
                {
                    DataTable dt = dataset.Tables[0];
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        ChiTietQuanLyHoatDongQuanLy_Non ds = new ChiTietQuanLyHoatDongQuanLy_Non(Session);
                        ds.OidKey = Guid.Parse(itemRow["Oid"].ToString());
                        ds.NhanVien = Session.GetObjectByKey<NhanVien>(itemRow["NhanVien"]);
                        ds.HoatDongQuanLy = Session.GetObjectByKey<HoatDongQuanLy>(itemRow["HoatDongQuanLy"]); ;
                        ds.NoiDungCongViec = itemRow["NoiDungCongViec"].ToString();
                        ds.NgayThucHien = Convert.ToDateTime(itemRow["NgayThucHien"].ToString());
                        ds.TongThoiGiang = Convert.ToDecimal(itemRow["TongThoiGiang"].ToString());
                        ds.HeSoQuyDoi = Convert.ToDecimal(itemRow["HeSoQuyDoi"].ToString());
                        ds.SoTietQuyDoi = Convert.ToDecimal(itemRow["SoTietQuyDoi"].ToString());
                        ds.GhiChu = itemRow["GhiChu"].ToString();
                        ds.XacNhan = Convert.ToBoolean(itemRow["XacNhan"].ToString());
                        DanhSach.Add(ds);

                    }
                }
            }
        }
    }

}