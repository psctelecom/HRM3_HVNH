using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.Win.Forms;

namespace PSC_HRM.Module.Win.Actions
{
    public partial class HRMHelpController : ViewController
    {
        public HRMHelpController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HRMHelpController_Activated(object sender, EventArgs e)
        {
            if (View.ObjectTypeInfo != null)
            {
                string type = View.ObjectTypeInfo.FullName;
                if (!string.IsNullOrEmpty(type))
                {
                    int index = type.LastIndexOf('.') + 1;
                    type = type.Substring(index);

                    System.IO.FileInfo file = new System.IO.FileInfo(String.Format(@"{0}\Help\{1}.mht", System.Windows.Forms.Application.StartupPath, type));
                    simpleAction1.Active["TruyCap"] = file.Exists;
                }
                else
                    simpleAction1.Active["TruyCap"] = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            frmHelper help = new frmHelper();
            help.XuLy(View.ObjectTypeInfo.Type, View.Caption);
        }
    }
}
