using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_ThemBoPhanController : ViewController
    {
        private BoPhan boPhan;
        private BoPhan_ThemBoPhan themBoPhan;
        private IObjectSpace obs;

        public BaoMat_ThemBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);

            popupWindowShowAction1.Id = "BaoMat_ThemBoPhanController";
            popupWindowShowAction1.Caption = "Thêm đơn vị";
            popupWindowShowAction1.ImageName = "Action_New";
            popupWindowShowAction1.TargetViewType = ViewType.Any;
            popupWindowShowAction1.TargetViewNesting = Nesting.Root;
            popupWindowShowAction1.TargetObjectType = typeof(BoPhan);
            HamDungChung.DebugTrace("BaoMat_ThemBoPhanController");
        }

        private void ThemBoPhanController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BoPhan>() ||
                HamDungChung.IsWriteGranted<ThongTinTruong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Bộ phận hiện hành
            View.ObjectSpace.CommitChanges();

            boPhan = View.CurrentObject as BoPhan;
            obs = Application.CreateObjectSpace();
            themBoPhan = obs.CreateObject<BoPhan_ThemBoPhan>();
            e.View = Application.CreateDetailView(obs, themBoPhan);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            BoPhan newBoPhan;
            if (themBoPhan.LoaiBoPhan == LoaiBoPhanEnum.Truong)
            {
                newBoPhan = obs.CreateObject<ThongTinTruong>();
            }
            else
            {
                newBoPhan = obs.CreateObject<BoPhan>();
            }

            
            if (boPhan != null)
                newBoPhan.BoPhanCha = obs.GetObjectByKey<BoPhan>(boPhan.Oid);
            newBoPhan.LoaiBoPhan = themBoPhan.LoaiBoPhan;

            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, newBoPhan);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }

        //reload listview after save BoPhan in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
