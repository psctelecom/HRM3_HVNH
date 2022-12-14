using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChiaTachDonViController : ViewController
    {
        public MailMerge_QuyetDinhChiaTachDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChiaTachDonVi>();
            QuyetDinhChiaTachDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChiaTachDonVi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChiaTachDonVi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
