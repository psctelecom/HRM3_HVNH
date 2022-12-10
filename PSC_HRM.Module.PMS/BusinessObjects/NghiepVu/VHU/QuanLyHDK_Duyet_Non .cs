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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{
    [ModelDefault("Caption", "Quản lý duyệt HDK(Non)")]
    [NonPersistent]
    public class QuanLyHDK_Duyet_Non : BaseObject
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
        public XPCollection<ChiTietKeKhaiHDK_VHU_Non> DanhSach
        {
            get;
            set;
        }
        public QuanLyHDK_Duyet_Non(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        public void LoadData()
        {
            if (NamHoc != null)
            {
                if (DanhSach != null)
                    DanhSach.Reload();
                else
                    DanhSach = new XPCollection<ChiTietKeKhaiHDK_VHU_Non>(Session, false);


                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@BoPhan", HamDungChung.GetPhanQuyenBoPhan());
                param[2] = new SqlParameter("@HocKy", HocKy == null? Guid.Empty : HocKy.Oid);
                SqlCommand cmd = DataProvider.GetCommand("spd_HoatDongKhac_LayDuLieuTheoPhanQuyen", CommandType.StoredProcedure, param);
                DataSet dataset = DataProvider.GetDataSet(cmd);
                if (dataset != null)
                {
                    DataTable dt = dataset.Tables[0];
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        ChiTietKeKhaiHDK_VHU_Non ds = new ChiTietKeKhaiHDK_VHU_Non(Session);
                        ds.OidKey = Guid.Parse(itemRow["Oid"].ToString());
                        ds.NhanVien = Session.GetObjectByKey<NhanVien>(itemRow["NhanVien"]);
                        ds.BoPhan = Session.GetObjectByKey<BoPhan>(itemRow["BoPhan"]); ;
                        ds.DanhMucHoatDongKhac = Session.GetObjectByKey<DanhMucHoatDongKhac>(itemRow["DanhMucHoatDongKhac"]); ;
                        ds.HocKy = Session.GetObjectByKey<HocKy>(itemRow["HocKy"]); ;
                        ds.SoLuong = Convert.ToDecimal(itemRow["SoLuong"].ToString());
                        ds.SoGioQuyDoi = Convert.ToDecimal(itemRow["SoGioQuyDoi"].ToString());
                        ds.GhiChu = itemRow["GhiChu"].ToString();
                        ds.XacNhan = Convert.ToBoolean(itemRow["XacNhan"].ToString());
                        DanhSach.Add(ds);

                    }
                }
            }
        }
    }

}
