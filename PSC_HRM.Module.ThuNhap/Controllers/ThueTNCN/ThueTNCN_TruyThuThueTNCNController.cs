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
    public partial class ThueTNCN_TruyThuThueTNCNController : ViewController
    {
        private IObjectSpace obs;
        private ChiTietQuanLyTruyThuThueTNCN bang;
        private CopyTruyThuThueTNCN source;

        public ThueTNCN_TruyThuThueTNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThueTNCN_TruyThuThueTNCNController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangDinhMucNopThueTNCN>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //View.ObjectSpace./

           bang = View.CurrentObject as ChiTietQuanLyTruyThuThueTNCN;
            if (bang != null)
            {
                obs = Application.CreateObjectSpace();
                source = obs.CreateObject<CopyTruyThuThueTNCN>();
                e.View = Application.CreateDetailView(obs, source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Source", source.ChiTietQuanLyTruyThuThueTNCN.Oid);
                param[1] = new SqlParameter("@Target", bang.Oid);

                DataProvider.ExecuteNonQuery("spd_ThueTNCN_TruyThueTNCN", System.Data.CommandType.StoredProcedure, param);

                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
