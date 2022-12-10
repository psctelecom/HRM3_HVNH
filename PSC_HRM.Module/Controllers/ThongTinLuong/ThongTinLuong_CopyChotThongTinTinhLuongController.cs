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

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CopyChotThongTinTinhLuongController : ViewController
    {
        private IObjectSpace obs;
        private BangChotThongTinTinhLuong bang;
        private CopyChotThongTinTinhLuong source;

        public ThongTinLuong_CopyChotThongTinTinhLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ThongTinLuong_CopyChotThongTinTinhLuongController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangChotThongTinTinhLuong>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();

            bang = View.CurrentObject as BangChotThongTinTinhLuong;
            if (bang != null)
            {
                obs = Application.CreateObjectSpace();
                source = obs.CreateObject<CopyChotThongTinTinhLuong>();
                e.View = Application.CreateDetailView(obs, source);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@BangChotThongTinTinhLuong_ThangTruoc", source.BangChotThongTinTinhLuong.Oid);
                param[1] = new SqlParameter("@BangChotThongTinTinhLuong_ThangNay", bang.Oid);

                DataProvider.ExecuteNonQuery("spd_TaiChinh_CopyBangChotThongTinTinhLuong", System.Data.CommandType.StoredProcedure, param);

                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
    }
}
