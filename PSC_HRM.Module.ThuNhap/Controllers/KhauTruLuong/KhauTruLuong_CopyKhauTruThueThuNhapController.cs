using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using PSC_HRM.Module.ThuNhap;
using PSC_HRM.Module.ThuNhap.KhauTru;
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;
using PSC_HRM.Module.NonPersistentObjects;


namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class KhauTruLuong_CopyKhauTruThueThuNhapController : ViewController
    {
        private IObjectSpace obs;
        private BangKhauTruLuong bang;
        private CopyKhauTruThueThuNhap source;

        public KhauTruLuong_CopyKhauTruThueThuNhapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void KhauTruLuong_CopyKhauTruThueThuNhapController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("BUH"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangKhauTruLuong>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
            
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();

            bang = View.CurrentObject as BangKhauTruLuong;
            if (bang != null)
            {
                obs = Application.CreateObjectSpace();
                source = obs.CreateObject<CopyKhauTruThueThuNhap>();
                e.View = Application.CreateDetailView(obs, source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Source", source.BangKhauTruLuong.Oid);
                param[1] = new SqlParameter("@Target", bang.Oid);

                DataProvider.ExecuteNonQuery("spd_KhauTruLuong_CopyKhauTruThueThuNhap", System.Data.CommandType.StoredProcedure, param);

                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
