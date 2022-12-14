using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChuyenCongTacController : ViewController
    {
        public MailMerge_QuyetDinhChuyenCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenCongTac>();
            QuyetDinhChuyenCongTac qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChuyenCongTac)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenCongTac>>>("QuyetDinhChuyenCongTac").Merge(Application.CreateObjectSpace(), list);
        }
    }
}
