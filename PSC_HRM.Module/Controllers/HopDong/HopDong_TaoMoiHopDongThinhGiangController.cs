using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;
using System.Data.SqlClient;
using PSC_HRM.Module.TaoMaQuanLy;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoMoiHopDongThinhGiangController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_TaoHopDongThinhGiang chonHopDongThinhGiang;
        private QuanLyHopDongThinhGiang quanLyHopDong;

        public HopDong_TaoMoiHopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoMoiHopDongThinhGiangController");
        }

        //reload listview after save object in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction2.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<HopDong.HopDong>() &&
                HamDungChung.IsWriteGranted<HopDong_ThinhGiang>() &&
                HamDungChung.IsWriteGranted<HopDong_ThinhGiangChatLuongCao>();
        }

        private void popupWindowShowAction2_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            quanLyHopDong = View.CurrentObject as QuanLyHopDongThinhGiang;
            chonHopDongThinhGiang = obs.CreateObject<HopDong_TaoHopDongThinhGiang>();
            e.View = Application.CreateDetailView(obs, chonHopDongThinhGiang);
        }

        private void popupWindowShowAction2_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            HopDong.HopDong hopDong;
            switch (chonHopDongThinhGiang.LoaiHopDong)
            {
                case TaoHopDongThinhGiangEnum.HopDongThinhGiang:
                    hopDong = obs.CreateObject<HopDong_ThinhGiang>();
                    break;
                case TaoHopDongThinhGiangEnum.HopDongThinhGiangChatLuongCao:
                    hopDong = obs.CreateObject<HopDong_ThinhGiangChatLuongCao>();
                    break;
                default:
                    hopDong = obs.CreateObject<HopDong_ThinhGiang>();
                    break;
            }
            hopDong.QuanLyHopDongThinhGiang = obs.GetObjectByKey<QuanLyHopDongThinhGiang>(quanLyHopDong.Oid);
            //Tạo số hợp đồng tự động
            if (hopDong.QuanLyHopDongThinhGiang != null)
            {
                 SqlParameter[] parameter = new SqlParameter[1];
                 parameter[0] = new SqlParameter("@QuanLyHopDongThinhGiang", hopDong.QuanLyHopDongThinhGiang.Oid);

                 hopDong.SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang, parameter);
            }  
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hopDong);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }
    }
}
