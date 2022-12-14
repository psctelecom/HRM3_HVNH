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
    public partial class MailMerge_HopDongKhoanController : ViewController
    {
        public MailMerge_HopDongKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_Khoan>();
            HopDong_Khoan hopDongKhoan;
            foreach (object item in View.SelectedObjects)
            {
                hopDongKhoan = (HopDong_Khoan)item;
                if (hopDongKhoan != null)
                    list.Add(hopDongKhoan);
            }

            SystemContainer.Resolver<IMailMerge<IList<HopDong_Khoan>>>("HopDongKhoan").Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_Khoan>();

        }
    }
}
