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
    public partial class MailMerge_HopDongKiemGiangController : ViewController
    {
        public MailMerge_HopDongKiemGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_KiemGiang>();
            HopDong_KiemGiang hopDongKiemGiang;
            foreach (object item in View.SelectedObjects)
            {
                hopDongKiemGiang = (HopDong_KiemGiang)item;
                if (hopDongKiemGiang != null)
                    list.Add(hopDongKiemGiang);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_KiemGiang>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_KiemGiang>();

        }
    }
}
