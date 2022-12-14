using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDiCongTacController : ViewController
    {
        public MailMerge_QuyetDinhDiCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDiCongTac>();
            QuyetDinhDiCongTac qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDiCongTac)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDiCongTac>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
