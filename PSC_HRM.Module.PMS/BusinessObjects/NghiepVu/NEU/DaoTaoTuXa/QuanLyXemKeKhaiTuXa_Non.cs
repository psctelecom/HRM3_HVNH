using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa
{
    [ModelDefault("Caption","Quản lý xem kê khai từ xa")]
    [NonPersistent]
    public class QuanLyXemKeKhaiTuXa_Non : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "DS Chi tiết kê khai")]
        public XPCollection<ChiTietXemKeKhaiTuXa_Non> ListChiTietKeKhai
        {
            get;set;
        }

        public QuanLyXemKeKhaiTuXa_Non(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            //
            // Place here your initialization code.
        }

        public void LoadData()
        {
            if(NamHoc != null && HocKy != null)
            {
                if (ListChiTietKeKhai != null)
                    ListChiTietKeKhai.Reload();
                else
                    ListChiTietKeKhai = new XPCollection<ChiTietXemKeKhaiTuXa_Non>(Session, false);

                SqlParameter[] pDongBo = new SqlParameter[2];
                pDongBo[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                pDongBo[1] = new SqlParameter("@HocKy", HocKy.Oid);
                DataTable table = DataProvider.GetDataTable("spd_pms_KekhaiTuXa_LayDuLieu", CommandType.StoredProcedure, pDongBo);
                if(table != null)
                {
                    foreach(DataRow item in table.Rows)
                    {
                        ChiTietXemKeKhaiTuXa_Non ct = new ChiTietXemKeKhaiTuXa_Non(Session);
                        ct.BoPhan = item["BoPhan"].ToString();
                        ct.NhanVien = item["NhanVien"].ToString();
                        ct.TenMonHoc = item["TenMonHoc"].ToString();
                        ct.LopMonHoc = item["LopMonHoc"].ToString();
                        ct.BoMonQuanLy = item["BoMonQuanLy"].ToString();
                        ct.OidChiTiet = item["OidChiTiet"].ToString();
                        ct.BacDaoTao = item["BacDaoTao"].ToString();
                        ct.HeDaoTao = item["HeDaoTao"].ToString();
                        ct.SoBaiKiemTra = Convert.ToInt32(item["SoBaiKiemTra"].ToString());
                        ct.SoBaiTieuLuan = Convert.ToInt32(item["SoBaiTieuLuan"].ToString());
                        ct.SoTraLoiCauHoiTrenHeThongHocTap = Convert.ToInt32(item["SoTraLoiCauHoiTrenHeThongHocTap"].ToString());
                        ct.SoTruyCapLopHoc = Convert.ToInt32(item["SoTruyCapLopHoc"].ToString());
                        ct.LoaiHuongDan = item["LoaiHuongDan"].ToString();
                        ct.SoLuongHuongDan = Convert.ToInt32(item["SoLuongHuongDan"].ToString());
                        ct.XacNhan = Convert.ToBoolean(item["XacNhan"].ToString());
                        ListChiTietKeKhai.Add(ct);
                    }
                }
            }

        }
    }

}