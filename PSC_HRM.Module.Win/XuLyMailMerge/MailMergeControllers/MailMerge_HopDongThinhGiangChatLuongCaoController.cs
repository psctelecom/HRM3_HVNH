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
    public partial class MailMerge_HopDongThinhGiangChatLuongCaoController : ViewController
    {
        public MailMerge_HopDongThinhGiangChatLuongCaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_ThinhGiangChatLuongCao>();
            HopDong_ThinhGiangChatLuongCao hopDongThinhGiangChatLuongCao;
            foreach (object item in View.SelectedObjects)
            {
                hopDongThinhGiangChatLuongCao = (HopDong_ThinhGiangChatLuongCao)item;
                if (hopDongThinhGiangChatLuongCao != null)
                    list.Add(hopDongThinhGiangChatLuongCao);
            }
            SystemContainer.Resolver<MailMerge_HopDongThinhGiangChatLuongCao>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongThinhGiangChatLuongCaoController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_ThinhGiangChatLuongCao>();

        }
    }
}
