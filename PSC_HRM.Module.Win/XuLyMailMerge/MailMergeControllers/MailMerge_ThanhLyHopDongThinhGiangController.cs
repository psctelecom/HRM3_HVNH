using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using DevExpress.ExpressApp.Security;
using System.Collections.Generic;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThanhLyHopDongThinhGiangController : ViewController
    {
        public MailMerge_ThanhLyHopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<ChiTietThanhLyHopDongThinhGiang>();
            ChiTietThanhLyHopDongThinhGiang qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (ChiTietThanhLyHopDongThinhGiang)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<ChiTietThanhLyHopDongThinhGiang>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_ThanhLyHopDongThinhGiangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<ChiTietThanhLyHopDongThinhGiang>();
        }
    }
}
