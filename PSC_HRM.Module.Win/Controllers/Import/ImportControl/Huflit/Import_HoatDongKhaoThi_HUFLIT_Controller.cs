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
using PSC_HRM.Module.Win.Controllers.Import.ImportClass;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class Import_HoatDongKhaoThi_HUFLIT_Controller : ViewController
    {
        QuanLyKhaoThi _QuanLyKhaoThi;
        private IObjectSpace _obs;

        public Import_HoatDongKhaoThi_HUFLIT_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKhaoThi_DetailView";
        }



        private void btn_ImportDanhSachKhaoThi_HUFLIT_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            _QuanLyKhaoThi = View.CurrentObject as QuanLyKhaoThi;
            View.ObjectSpace.CommitChanges();
            Import_HoatDongKhaoThi_HUFLIT.XuLy(_obs, _QuanLyKhaoThi);
            _obs.Refresh();
        }
        void Import_HoatDongKhaoThi_HUFLIT_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "HUFLIT")
                btn_ImportDanhSachKhaoThi_HUFLIT.Active["TruyCap"] = false;
        }
    }
}
