using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.ChotThongTinTinhLuong;
using System.Data.SqlClient;
using DevExpress.Utils;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.Thue;
using PSC_HRM.Module.ThuNhap.NonPersistentObjects;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_CopyThueTNCNController : ViewController
    {
        private IObjectSpace obs;
        private BangDinhMucNopThueTNCN bang;
        private CopyBangDinhMucThueTNCN source;

        public ThueTNCN_CopyThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThueTNCN_CopyThueTNCNController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangDinhMucNopThueTNCN>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();

            bang = View.CurrentObject as BangDinhMucNopThueTNCN;
            if (bang != null)
            {
                obs = Application.CreateObjectSpace();
                source = obs.CreateObject<CopyBangDinhMucThueTNCN>();
                e.View = Application.CreateDetailView(obs, source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Source", source.BangDinhMucNopThueTNCN.Oid);
                param[1] = new SqlParameter("@Target", bang.Oid);

                DataProvider.ExecuteNonQuery("spd_TaiChinh_CopyBangDinhMucThueTNCN", System.Data.CommandType.StoredProcedure, param);

                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
