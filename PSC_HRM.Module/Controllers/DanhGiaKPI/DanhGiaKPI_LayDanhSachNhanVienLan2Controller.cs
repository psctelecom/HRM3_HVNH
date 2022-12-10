using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGiaKPI;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGiaKPI_LayDanhSachNhanVienLan2Controller : ViewController
    {
        public DanhGiaKPI_LayDanhSachNhanVienLan2Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGiaKPI_LayDanhSachNhanVienLan2Controller");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if ((View.CurrentObject as QuanLyDanhGiaKPI).Oid == Guid.Empty)
            {
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
            //
            QuanLyDanhGiaKPI qldanhGia = View.CurrentObject as QuanLyDanhGiaKPI;
            //
            if (qldanhGia != null)
            {
                using (DialogUtil.AutoWait())
                {
                    // Lấy danh sách bộ phận ghép thành chuỗi
                    StringBuilder sb = new StringBuilder();
                    foreach (string item in HamDungChung.DanhSachBoPhanDuocPhanQuyen())
                    {
                        sb.Append(item + ";");
                    }
                    //
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@QuanLyDanhGiaKPI", qldanhGia.Oid);
                    param[1] = new SqlParameter("@BoPhanList", sb.ToString());
                    //
                    DataProvider.ExecuteNonQuery("dbo.spd_DanhGiaKPI_LayDanhSachNhanVienDanhGiaKPILan2", System.Data.CommandType.StoredProcedure, param);

                    View.ObjectSpace.Refresh();
                }
                DialogUtil.ShowInfo("Lấy danh sách cán bộ thành công!");
            }
            else
            { DialogUtil.ShowError("Chưa chọn năm đánh giá."); }
        }

        private void DanhGiaKPI_LayDanhSachNhanVienLan2Controller_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyDanhGiaKPI>() &&
                                              HamDungChung.IsWriteGranted<DanhGiaKPI.DanhGiaKPI>() &&
                                              HamDungChung.IsWriteGranted<ChiTietDanhGiaKPI>();
        }

    }
}
