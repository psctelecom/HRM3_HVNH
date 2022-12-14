using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_PhuLucHopDongKhoanController : ViewController
    {
        public MailMerge_PhuLucHopDongKhoanController()
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
                if (hopDongKhoan != null
                    && hopDongKhoan.HopDongKhoan != null)
                    list.Add(hopDongKhoan);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_Khoan>>>("PhuLucHopDongKhoan").Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_Khoan>();

        }
    }
}
