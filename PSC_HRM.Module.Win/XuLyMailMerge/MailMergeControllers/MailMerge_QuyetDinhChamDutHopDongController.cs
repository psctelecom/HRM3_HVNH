using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChamDutHopDongController : ViewController
    {
        public MailMerge_QuyetDinhChamDutHopDongController()
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
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChamDutHopDong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
