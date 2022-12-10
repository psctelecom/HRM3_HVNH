using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using System.Text;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DoanDang;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_ImportNhanVienController : ViewController
    {
        private HoSo_Import obj;
        private IObjectSpace obs;

        public HoSo_ImportNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSo_ImportController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ThongTinNhanVien>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            obj = obs.CreateObject<HoSo_Import>();
            e.View = Application.CreateDetailView(obs, obj);
        }

        private void popupWindowShowAction1_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            bool oke = true;

            if (!obj.TatCa)
            {
                if (obj.BoPhan == null)
                {
                    DialogUtil.ShowError("Chưa chọn bộ phận.");
                    return;
                }
                if (obj.BoPhan.ListBoPhanCon.Count > 0)
                {
                    DialogUtil.ShowError("Không thể import cán bộ vào bộ phận [" + obj.BoPhan.TenBoPhan + "]");
                    return;
                }
            }
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel 2003 file (*.xls)|*.xls;*.xlsx";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        if (TruongConfig.MaTruong == "VLU")
                        {
                            HoSo_ImportHoSo_VLU.Import(obj, obs, open.FileName, false);

                        }
                        else
                        {
                            HoSo_ImportHoSo.Import(obj, obs, open.FileName, false);
                        }
                       
                      
                    }
                    if (oke)
                    {
                        DialogUtil.ShowInfo("Import hồ sơ thành công!");
                    }
                }
            }
        }
    }
}
