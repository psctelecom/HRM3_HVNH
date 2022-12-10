using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoMoiHopDongController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_TaoHopDong chonHopDong;
        private QuanLyHopDong quanLyHopDong;

        public HopDong_TaoMoiHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoMoiHopDongController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            chonHopDong = obs.CreateObject<HopDong_TaoHopDong>();
            e.View = Application.CreateDetailView(obs, chonHopDong);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            HopDong_NhanVien hopDong;
            switch (chonHopDong.LoaiHopDong)
            {
                case TaoHopDongEnum.HopDongLamViec:
                    hopDong = obs.CreateObject<HopDong_LamViec>();
                    break;
                case TaoHopDongEnum.HopDongHeSo:
                    hopDong = obs.CreateObject<HopDong_LaoDong>();
                    break;
                case TaoHopDongEnum.HopDongKhoan:
                    hopDong = obs.CreateObject<HopDong_Khoan>();
                    break;
                default:
                    hopDong = obs.CreateObject<HopDong_LamViec>();
                    break;
            }
            hopDong.QuanLyHopDong = obs.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hopDong);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }

        //reload listview after save object in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = true;
        }
    }
}
