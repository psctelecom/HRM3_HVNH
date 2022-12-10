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
using PSC_HRM.Module;
using System.Data.SqlClient;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_LayDanhSachNhanVienController : ViewController
    {
        public DanhGia_LayDanhSachNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_LayDanhSachNhanVienController");
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if ((View.CurrentObject as DanhGiaCanBoCuoiNam).Oid == Guid.Empty)
            {
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
            //
            DanhGiaCanBoCuoiNam danhGia = View.CurrentObject as DanhGiaCanBoCuoiNam;
            //
            if (danhGia != null)
            {
                using (DialogUtil.AutoWait())
                {
                    // Lấy danh sách bộ phận ghép thành chuỗi
                    StringBuilder sb = new StringBuilder();
                    foreach (Guid item in HamDungChung.GetCriteriaBoPhan().ToArray())
                    {
                        sb.Append(item.ToString() + ",");
                    }
                    //
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@OidDanhGiaCanBo", danhGia.Oid);
                    param[1] = new SqlParameter("@BoPhanList", sb.ToString());
                    //
                    DataProvider.ExecuteNonQuery("dbo.spd_DanhGia_LayDanhSachNhanVienDanhGiaCuoiNam", System.Data.CommandType.StoredProcedure, param);

                    View.ObjectSpace.Refresh();
                }
                DialogUtil.ShowInfo("Lấy danh sách cán bộ thành công!");
            }
            else
            { DialogUtil.ShowError("Chưa chọn năm đánh giá."); }
        }

        private void DanhGia_DanhSachDanhGiaLan1Controller_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhGiaCanBoCuoiNam>() &&
                                              HamDungChung.IsWriteGranted<ChiTietDanhGiaCanBoCuoiNamLan1>();
        }

    }
}
