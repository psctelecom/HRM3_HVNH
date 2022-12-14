using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_HopDongChamBaoCaoController : ViewController
    {
        public MailMerge_HopDongChamBaoCaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<HopDong_ChamBaoCao>();
            HopDong_ChamBaoCao hopDongChamBaoCao;
            foreach (object item in View.SelectedObjects)
            {
                hopDongChamBaoCao = (HopDong_ChamBaoCao)item;
                if (hopDongChamBaoCao != null)
                    list.Add(hopDongChamBaoCao);
            }
            SystemContainer.Resolver<IMailMerge<IList<HopDong_ChamBaoCao>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_ChamBaoCao>();
        }
    }
}
