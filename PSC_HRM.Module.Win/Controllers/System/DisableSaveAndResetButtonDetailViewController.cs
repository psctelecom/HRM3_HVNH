using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace PSC_HRM.Module.Win
{
    public partial class DisableSaveAndResetButtonDetailViewController : ViewController<DetailView>
    {
        public DisableSaveAndResetButtonDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }


        private void DisableSaveAndResetButtonDetailViewController_ViewControlsCreated(object sender, System.EventArgs e)
        {
            DetailView detailView = View as DetailView;
            if (detailView != null)
            {

                #region 1. Ẩn nút save trên detail view
                ModificationsController modificationsController = Frame.GetController<ModificationsController>();
                if (modificationsController != null)
                {
                    if (detailView.Id == "QuanLyHoatDongQuanLy_Non_DetailView"
                        || detailView.Id == "QuanLyNCKH_Non_DetailView")
                    {
                        modificationsController.SaveAndCloseAction.Active["TruyCap"] = false;
                        modificationsController.SaveAction.Active["TruyCap"] = false;
                        if (detailView.Id == "ReportCustomView_DetailView")
                            modificationsController.CancelAction.Active["TruyCap"] = false;
                    }
                    else
                    {
                        modificationsController.SaveAndCloseAction.Active["TruyCap"] = true;
                        modificationsController.SaveAction.Active["TruyCap"] = true;
                    }
                }
                #endregion
                #region 1. Ẩn nút Refresh trên detail view
                RefreshController refreshController = Frame.GetController<RefreshController>();
                if (refreshController != null)
                {
                    #region if củ
                    if (detailView.Id == "QuanLyHoatDongQuanLy_Non_DetailView"
                        || detailView.Id == "QuanLyNCKH_Non_DetailView"
                        || detailView.Id == "QuanLyHDK_Nhap_Non_DetailView"
                        || detailView.Id == "QuanLyHDK_Duyet_Non_DetailView"
                        || detailView.Id == "PMS_ThanhToanThuLaoGiangDay_HopDong_NonBaoCao_DetailView"
                        || detailView.Id == "PMS_ThanhToanThuLaoGiangDay_Tong_NonBaoCao_DetailView"
                        || detailView.Id == "PMS_ThanhToanThuLaoGiangDay_ChiTiet_NonBaoCao_DetailView")
                    #endregion
                    {
                        refreshController.RefreshAction.Active["Visible"] = false;
                    }
                }
                #endregion
            }
        }

    }
}
