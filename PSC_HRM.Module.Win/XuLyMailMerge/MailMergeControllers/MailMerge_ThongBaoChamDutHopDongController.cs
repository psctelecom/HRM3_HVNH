using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_ThongBaoChamDutHopDongController : ViewController
    {
        public MailMerge_ThongBaoChamDutHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChamDutHopDong>();
            QuyetDinhChamDutHopDong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChamDutHopDong)item;
                qd.IsThongBaoChamDutHopDong = true;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChamDutHopDong>>>().Merge(Application.CreateObjectSpace(), list);
        }

        private void MailMerge_ThongBaoChamDutHopDongController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("IUH"))
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
            {
                simpleAction1.Active["TruyCap"] = false;
            }
        }
    }
}
