using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoHiem;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_KetChuyenSoLieuController : ViewController
    {
        public BaoHiem_KetChuyenSoLieuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BaoHiem_KetChuyenSoLieuController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyBienDong>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            QuanLyBienDong quanLyBienDong = View.CurrentObject as QuanLyBienDong;
            if (quanLyBienDong != null && quanLyBienDong.ThoiGian != DateTime.MinValue)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@ThangHienTai", quanLyBienDong.ThoiGian);
                param[1] = new SqlParameter("@DotHienTai", quanLyBienDong.Dot);
                using (DataTable dt = DataProvider.GetDataTable("spd_BaoHiem_KetChuyenDauKy", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        quanLyBienDong.SoLaoDongBHXHKyTruoc = (int)dr["SoLaoDongBHXHKyNay"];
                        quanLyBienDong.SoLaoDongBHYTKyTruoc = (int)dr["SoLaoDongBHXHKyNay"];
                        quanLyBienDong.SoLaoDongBHTNKyTruoc = (int)dr["SoLaoDongBHXHKyNay"];
                        quanLyBienDong.QuyLuongBHXHKyTruoc = (decimal)dr["TongQuyLuongBHXHKyNay"];
                        quanLyBienDong.QuyLuongBHYTKyTruoc = (decimal)dr["TongQuyLuongBHYTKyNay"];
                        quanLyBienDong.QuyLuongBHTNKyTruoc = (decimal)dr["TongQuyLuongBHTNKyNay"];
                        quanLyBienDong.SoPhaiDongBHXHKyTruoc = (decimal)dr["SoPhaiDongBHXHKyNay"];
                        quanLyBienDong.SoPhaiDongBHYTKyTruoc = (decimal)dr["SoPhaiDongBHYTKyNay"];
                        quanLyBienDong.SoPhaiDongBHTNKyTruoc = (decimal)dr["SoPhaiDongBHTNKyNay"];
                    }
                }
            }
        }
    }
}
