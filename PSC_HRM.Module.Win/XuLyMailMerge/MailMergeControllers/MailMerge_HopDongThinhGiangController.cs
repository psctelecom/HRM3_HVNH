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
    public partial class MailMerge_HopDongThinhGiangController : ViewController
    {
        public MailMerge_HopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_ThinhGiang>();
            HopDong_ThinhGiang hopDongThinhGiang;
            foreach (object item in View.SelectedObjects)
            {
                hopDongThinhGiang = (HopDong_ThinhGiang)item;
                if (hopDongThinhGiang != null)
                    list.Add(hopDongThinhGiang);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_ThinhGiang>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_ThinhGiang>();

        }
    }
}
