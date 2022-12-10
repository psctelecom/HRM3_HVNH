using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Khai báo hệ số khối lượng giảng dạy")]
    public class KhaiBao_LaiHeSoKhoiLuongGiangDay : BaseObject
    {
       
        [ModelDefault("Caption", "Chi tiết ")]
        public XPCollection<ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay> listKetKhai
        {
            get;
            set;
        }
        public KhaiBao_LaiHeSoKhoiLuongGiangDay(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public void LoadData(Guid KhoiLuongGiangDay)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KhoiLuongGiangDay", KhoiLuongGiangDay);

            DataTable dt = DataProvider.GetDataTable("spd_PMS_DuLieuKhoiLuongGiangDay", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay ds = new ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay(Session);
                    ds.MaGV = item["MaGV"].ToString();
                    ds.HoTen = item["HoTen"].ToString();
                    ds.LopHocPhan = item["LopHocPhan"].ToString();
                    ds.LopSinhVien = item["LopSinhVien"].ToString();
                    ds.MaHP = item["MaHP"].ToString();
                    ds.TenHocPhan = item["TenHocPhan"].ToString();
                    ds.HeSoBacDaoTao = Convert.ToDecimal(item["HeSoBacDaoTao"].ToString());
                    ds.HeSoLopDong = Convert.ToDecimal(item["HeSoLopDong"].ToString());
                    ds.HeSoNgoaiGio = Convert.ToDecimal(item["HeSoNgoaiGio"].ToString());
                    ds.HeSoNgonNgu = Convert.ToDecimal(item["HeSoNgonNgu"].ToString());
                    ds.HeSoTinhChi = Convert.ToDecimal(item["HeSoTinhChi"].ToString());
                    ds.OidChiTiet = Guid.Parse(item["Oid"].ToString());
                    ds.BacHeDaoTao = item["BacHeDaoTao"].ToString();
                    listKetKhai.Add(ds);
                }
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listKetKhai = new XPCollection<ChiTietKhaiBaoLaiHeSo_KhoiLuongGiangDay>(Session, false);
        }
    }
}