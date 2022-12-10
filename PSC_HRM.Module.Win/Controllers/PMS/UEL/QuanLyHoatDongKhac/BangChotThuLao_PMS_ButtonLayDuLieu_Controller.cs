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
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BangChotThuLao_PMS_ButtonLayDuLieu_Controller : ViewController
    {
        public BangChotThuLao_PMS_ButtonLayDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyNV_ThanhToanThuLao_DetailView";
           
        }

        private void BangChotThuLao_PMS_ButtonLayDuLieu_Controller_ViewControlsCreated(object sender, EventArgs e)
        {
            IObjectSpace os1 = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)os1).Session;//Session được sử dụng để load và lưu lại  

            DetailView view = View as DetailView;

            QuanLyNV_ThanhToanThuLao qly = View.CurrentObject as QuanLyNV_ThanhToanThuLao;
            //
            if (view != null)
            {
                ViewItem item = ((DetailView)View).FindItem("btnLoadData") as ViewItem;//ControlViewItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Lấy dữ liệu";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            //Gọi hàm createCommand() bên class
                            qly.LoadData();
                            View.Refresh();
                        };
                    }
                }

            }
        }
        
      
    }

}
