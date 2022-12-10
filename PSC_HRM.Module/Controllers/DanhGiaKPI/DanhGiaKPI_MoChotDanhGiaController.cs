using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhGiaKPI;
using System.Text;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGiaKPI_MoChotDanhGiaController : ViewController
    {
        public DanhGiaKPI_MoChotDanhGiaController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DanhGiaKPI_MoChotDanhGiaController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.CurrentUser().MoKhoaSoLuong;
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
                    DataProvider.ExecuteNonQuery("dbo.spd_DanhGiaKPI_MoChotDanhGiaKPILan3", System.Data.CommandType.StoredProcedure, param);

                    View.ObjectSpace.Refresh();
                }
                DialogUtil.ShowInfo("Mở chốt đánh giá thành công!");
            }
            else
            { DialogUtil.ShowError("Chưa có thông tin."); }
        }
    }
}
