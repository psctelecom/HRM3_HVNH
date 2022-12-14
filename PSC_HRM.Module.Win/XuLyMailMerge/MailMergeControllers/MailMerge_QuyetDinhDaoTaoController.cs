using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDaoTao>();
            QuyetDinhDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
