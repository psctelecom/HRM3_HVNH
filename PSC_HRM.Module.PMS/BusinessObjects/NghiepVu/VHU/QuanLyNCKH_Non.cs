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
    [ModelDefault("Caption", "Quản lý NCKH(Non)")]
    [NonPersistent]
    public class QuanLyNCKH_Non : BaseObject
    {
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
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

        [ModelDefault("Caption", "Danh sách chi tiết")]
        public XPCollection<ChiTietNCKH_Non> DanhSach
        {
            get;
            set;
        }
        public QuanLyNCKH_Non(Session session) : base(session) { }

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
                    DanhSach = new XPCollection<ChiTietNCKH_Non>(Session, false);


                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[1] = new SqlParameter("@BoPhan", HamDungChung.GetPhanQuyenBoPhan());
                SqlCommand cmd = DataProvider.GetCommand("spd_NghienCuuKhoaHoc_LayDuLieuTheoPhanQuyen", CommandType.StoredProcedure, param);
                DataSet dataset = DataProvider.GetDataSet(cmd);
                if (dataset != null)
                {
                    DataTable dt = dataset.Tables[0];
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        ChiTietNCKH_Non ds = new ChiTietNCKH_Non(Session);
                        ds.OidKey = Guid.Parse(itemRow["Oid"].ToString());
                        ds.NhanVien = Session.GetObjectByKey<NhanVien>(itemRow["NhanVien"]);
                        ds.DanhMucNCKH = Session.GetObjectByKey<DanhSachChiTietHDKhac>(itemRow["DanhMucNCKH"]);
                        ds.TenDeTai = itemRow["TenNCKH"].ToString() ; 
                        ds.SoTiet = Convert.ToDecimal(itemRow["SoTiet"].ToString());
                        ds.NgayNhap = Convert.ToDateTime(itemRow["NgayNhap"].ToString());
                        ds.GioQuyDoiNCKH = Convert.ToDecimal(itemRow["GioQuyDoiNCKH"].ToString());
                        ds.SoLuongTV = Convert.ToInt32(itemRow["SoLuongTV"].ToString());
                        ds.VaiTro = Session.GetObjectByKey<VaiTroNCKH>(itemRow["VaiTro"]);
                        ds.DaThanhToan = Convert.ToBoolean(itemRow["DuKien"].ToString());
                        ds.XacNhan = Convert.ToBoolean(itemRow["XacNhan"].ToString());
                        ds.TuChoi = Convert.ToBoolean(itemRow["TuChoi"].ToString());
                        ds.GhiChu = itemRow["GhiChu"].ToString();
                        DanhSach.Add(ds);

                    }
                }
            }
        }
    }

}
