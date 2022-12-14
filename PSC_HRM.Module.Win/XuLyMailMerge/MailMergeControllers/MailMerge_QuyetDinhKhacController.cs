using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhKhacController : ViewController
    {
        public MailMerge_QuyetDinhKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhKhac>();
            QuyetDinhKhac qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhKhac)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhKhac>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
