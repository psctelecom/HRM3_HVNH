using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDoiTenDonViController : ViewController
    {
        public MailMerge_QuyetDinhDoiTenDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDoiTenDonVi>();
            QuyetDinhDoiTenDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDoiTenDonVi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDoiTenDonVi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
