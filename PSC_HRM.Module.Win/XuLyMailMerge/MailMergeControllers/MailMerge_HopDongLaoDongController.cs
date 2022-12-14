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
    public partial class MailMerge_HopDongLaoDongController : ViewController
    {
        public MailMerge_HopDongLaoDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_LaoDong>();
            HopDong_LaoDong hopDongLaoDong;
            foreach (object item in View.SelectedObjects)
            {
                hopDongLaoDong = (HopDong_LaoDong)item;
                if (hopDongLaoDong != null)
                    list.Add(hopDongLaoDong);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_LaoDong>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_LaoDong>();

        }
    }
}
