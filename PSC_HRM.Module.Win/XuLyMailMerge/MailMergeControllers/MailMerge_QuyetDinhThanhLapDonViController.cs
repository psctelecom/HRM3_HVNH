using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapDonViController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapDonVi>();
            QuyetDinhThanhLapDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapDonVi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapDonVi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
