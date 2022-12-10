using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
using PSC_HRM.Module.PMS.NghiepVu.GDTC_QP;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class OnLoad_BacDaoTao_Controller : ViewController
    {
        public OnLoad_BacDaoTao_Controller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //TargetViewId = "KhoiLuongGiangDay_DetailView;"
            //    + "QuanLySauDaiHoc_DetailView";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            switch (View.Id)
            {
                case "KhoiLuongGiangDay_DetailView":
                    {
                        KhoiLuongGiangDay KhoiLuong = View.CurrentObject as KhoiLuongGiangDay;
                        if (KhoiLuong != null)
                            KhoiLuong.LoadBacDaoTao();
                    } break;
                case "QuanLySauDaiHoc_DetailView":
                    {
                        QuanLySauDaiHoc QuanLySauDaiHoc = View.CurrentObject as QuanLySauDaiHoc;
                        if (QuanLySauDaiHoc != null)
                            QuanLySauDaiHoc.LoadBacDaoTao();
                    } break;
                case "QuanLyKhaoThi_DetailView":
                    {
                        QuanLyGDTC_QP QuanLyGDTC_QP = View.CurrentObject as QuanLyGDTC_QP;
                        if (QuanLyGDTC_QP != null)
                            QuanLyGDTC_QP.LoadBacDaoTao();
                    } break;

                case "QuanLyGDTC_QP_DetailView":
                    {
                        QuanLyKhaoThi QuanLyKhaoThi = View.CurrentObject as QuanLyKhaoThi;
                        if (QuanLyKhaoThi != null)
                            QuanLyKhaoThi.LoadBacDaoTao();
                    } break;

                case "QuanLyPhiGiaoVu_DetaiView":
                    {
                        QuanLyPhiGiaoVu QuanLyPhiGiaoVu = View.CurrentObject as QuanLyPhiGiaoVu;
                        if (QuanLyPhiGiaoVu != null)
                            QuanLyPhiGiaoVu.LoadBacDaoTao();
                    } break;

                default:
                    break;
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
