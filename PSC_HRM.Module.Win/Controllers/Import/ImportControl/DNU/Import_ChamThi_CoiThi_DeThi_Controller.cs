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
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
using DevExpress.Xpo;
using System.Windows.Forms;
using ERP.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.Win.Controllers.Import.ImportClass.DNU;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportControl.DNU
{
    public partial class Import_ChamThi_CoiThi_DeThi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        
        public Import_ChamThi_CoiThi_DeThi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyKhaoThi_DetailView";
        }

        void Import_ChamThi_CoiThi_DeThi_Controller_Activated(object sender, System.EventArgs e)
        {
            if (TruongConfig.MaTruong != "DNU")
                btn_Import_ChamThi_CoiThi_DeThi_Controller.Active["TruyCap"] = false;
        }
        private void btn_Import_ChamThi_CoiThi_DeThi_Controller_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            try
            {
                QuanLyKhaoThi OidQuanLy = View.CurrentObject as QuanLyKhaoThi;
                if (OidQuanLy != null)
                {
                    _obs = View.ObjectSpace;
                    if (e.SelectedChoiceActionItem.Caption == "Import Chấm Thi")
                    {
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_ChamThi.XuLy(_obs, OidQuanLy);
                        _obs.Refresh();
                    }
                    else if (e.SelectedChoiceActionItem.Caption == "Import Coi Thi")
                    {
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_CoiThi.XuLy(_obs, OidQuanLy);
                        _obs.Refresh();
                    }
                    else if (e.SelectedChoiceActionItem.Caption == "Import Đề Thi")
                    {
                        _obs = View.ObjectSpace;
                        View.ObjectSpace.CommitChanges();
                        Imp_DeThi.XuLy(_obs, OidQuanLy);
                        _obs.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        
    }
}
