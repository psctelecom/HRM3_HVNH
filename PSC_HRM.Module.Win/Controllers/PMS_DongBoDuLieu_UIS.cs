using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using System.Data;

namespace PSC_HRM.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class PMS_DongBoDuLieu_UIS : ViewController<ListView>
    {
        public PMS_DongBoDuLieu_UIS()
        {
            InitializeComponent();
            RegisterActions(components);
            //TargetViewId = "CoSoGiangDay_ListView;BacDaoTao_ListView";
        }

        //void PMS_DongBoDuLieu_UIS_Activated(object sender, System.EventArgs e)
        //{

        //}
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            ListView _listView = View as ListView;
            if (TruongConfig.MaTruong == "UFM")
            {
                if (_listView.Id == "CoSoGiangDay_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_CoSoGiangDay", CommandType.StoredProcedure);
                else
                    if (_listView.Id == "BacDaoTao_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_BacDaoTao", CommandType.StoredProcedure);
                else
                        if (_listView.Id == "HeDaoTao_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_HeDaoTao", CommandType.StoredProcedure);
            }
            else
                if (TruongConfig.MaTruong == "HUFLIT")
            {
                if (_listView.Id == "NamHoc_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoNamHoc_HocKyTheo_UIS", CommandType.StoredProcedure);
                if (_listView.Id == "HinhThucThi_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoHinhThucThi_UIS", CommandType.StoredProcedure);
            }
            else
                if (TruongConfig.MaTruong == "NEU")
            {
                if (_listView.Id == "Bac_HeDaoTao_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_BacHeDaoTao", CommandType.StoredProcedure);
                else
                    if (_listView.Id == "BacDaoTao_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_BacDaoTao", CommandType.StoredProcedure);
                else
                        if (_listView.Id == "HeDaoTao_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_HeDaoTao", CommandType.StoredProcedure);
            }
            else
                if (TruongConfig.MaTruong == "VHU")
            {
                if (_listView.Id == "NamHoc_ListView")
                    DataProvider.ExecuteNonQuery("spd_PMS_DongBo_NamHocHocKy_UIS", CommandType.StoredProcedure);
            }
        }
    }
}
