using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNangPhuCapController : ViewController
    {
        public MailMerge_QuyetDinhNangPhuCapController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNangPhuCap>();
            QuyetDinhNangPhuCap qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNangPhuCap)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNangPhuCap>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
