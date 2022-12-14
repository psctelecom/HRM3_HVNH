using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNghiHuuController : ViewController
    {
        public MailMerge_QuyetDinhNghiHuuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNghiHuu>();
            QuyetDinhNghiHuu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNghiHuu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNghiHuu>>>("QuyetDinhNghiHuu").Merge(Application.CreateObjectSpace(), list);
        }
    }
}
