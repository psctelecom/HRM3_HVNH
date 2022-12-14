using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ListHopDongController : ViewController
    {
        public MailMerge_ListHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var listHDCBC = new List<HopDong_ChamBaoCao>();
            var listHDTG = new List<HopDong_ThinhGiang>();

            foreach (object item in View.SelectedObjects)
            {
                if (item is HopDong_ChamBaoCao)
                    listHDCBC.Add(item as HopDong_ChamBaoCao);
                if (item is HopDong_ThinhGiang)
                    listHDTG.Add(item as HopDong_ThinhGiang);
            }
            if (listHDCBC.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_ChamBaoCao>>>().Merge(Application.CreateObjectSpace(), listHDCBC);
            if (listHDTG.Count > 0)
                SystemContainer.Resolver<IMailMerge<IList<HopDong_ThinhGiang>>>().Merge(Application.CreateObjectSpace(), listHDTG);
        }

        private void MailMerge_HopDongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsCreateGranted<HopDong_ChamBaoCao>();
        }
    }
}
