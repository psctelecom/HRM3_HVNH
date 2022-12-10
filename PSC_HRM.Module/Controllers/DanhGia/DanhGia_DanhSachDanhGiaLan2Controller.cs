using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_DanhSachDanhGiaLan2Controller : ViewController
    {
        public DanhGia_DanhSachDanhGiaLan2Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_DanhSachDanhGiaLan2Controller");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
            ChiTietDanhGiaCanBoCuoiNamLan2 chiTietLan1 = View.CurrentObject as ChiTietDanhGiaCanBoCuoiNamLan2;
            if (chiTietLan1 != null)
            {
                using (DialogUtil.AutoWait())
                {
                    View.ObjectSpace.CommitChanges();

                    // Lấy danh sách bộ phận ghép thành chuỗi
                    StringBuilder sb = new StringBuilder();
                    foreach (Guid item in HamDungChung.GetCriteriaBoPhan().ToArray())
                    {
                        sb.Append(item.ToString() + ",");
                    }

                    //Chạy store đánh giá theo điều kiện dưới store
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@DanhGiaCanBoCuoiNam", chiTietLan1.DanhGiaCanBoCuoiNam.Oid);
                    param[1] = new SqlParameter("@BoPhanList", sb.ToString());
                    param[2] = new SqlParameter("@Nam", HamDungChung.GetServerTime().Year);

                    DataProvider.ExecuteNonQuery("dbo.spd_DanhGia_DanhGiaCanBoCuoiNamLan2", System.Data.CommandType.StoredProcedure, param);

                    View.ObjectSpace.Refresh();
                }
            }
        }

        private void DanhGia_DanhSachDanhGiaLan2Controller_Activated(object sender, EventArgs e)
        {
            simpleAction2.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhGiaCanBoCuoiNam>() &&
                                              HamDungChung.IsWriteGranted<ChiTietDanhGiaCanBoCuoiNamLan2>();
        }
    }
}
