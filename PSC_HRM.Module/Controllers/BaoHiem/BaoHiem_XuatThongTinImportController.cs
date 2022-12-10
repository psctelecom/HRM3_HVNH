using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using PSC_HRM.Module.QuaTrinh;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoHiem_XuatThongTinImportController : ViewController
    {
        private IObjectSpace obs;
        private BaoHiem_ExportThongTin export;

        public BaoHiem_XuatThongTinImportController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoHiem_XuatThongTinImportController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            export = obs.CreateObject<BaoHiem_ExportThongTin>();
            e.View = Application.CreateDetailView(obs, export);
        }

        private void BaoHiem_XuatThongTinImportController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BaoHiem.HoSoBaoHiem>()
                && TruongConfig.MaTruong.Equals("BUH");
        }

        private void popupWindowShowAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                obs = Application.CreateObjectSpace();
                BaoHiem_XuatThongTinImportBHXH bangExport;
                bangExport = obs.CreateObject<BaoHiem_XuatThongTinImportBHXH>();
                if (export.NgayVaoCoQuan == DateTime.MinValue)
                    bangExport.NgayVaoCoQuan = "";
                else
                    bangExport.NgayVaoCoQuan = export.NgayVaoCoQuan.ToString("dd/MM/yyyy");

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, bangExport);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }
    }
}