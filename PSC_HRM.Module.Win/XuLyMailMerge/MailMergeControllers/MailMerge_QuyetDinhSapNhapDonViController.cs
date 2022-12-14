using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhSapNhapDonViController : ViewController
    {
        public MailMerge_QuyetDinhSapNhapDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhSapNhapDonVi>();
            QuyetDinhSapNhapDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhSapNhapDonVi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhSapNhapDonVi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
