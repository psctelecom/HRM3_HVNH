using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuyDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhHuyDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuyDaoTao>();
            QuyetDinhHuyDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuyDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuyDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
