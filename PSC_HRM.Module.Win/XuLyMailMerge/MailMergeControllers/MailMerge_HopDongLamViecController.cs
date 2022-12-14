using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using System.Collections.Generic;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_HopDongLamViecController : ViewController
    {
        public MailMerge_HopDongLamViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_LamViec>();
            HopDong_LamViec hopDongLamViec;
            foreach (object item in View.SelectedObjects)
            {
                hopDongLamViec = (HopDong_LamViec)item;
                if (hopDongLamViec != null)
                    list.Add(hopDongLamViec);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_LamViec>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_LamViec>();

        }
    }
}
