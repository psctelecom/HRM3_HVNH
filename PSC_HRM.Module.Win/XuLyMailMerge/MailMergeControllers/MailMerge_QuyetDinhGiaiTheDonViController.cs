using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaiTheDonViController : ViewController
    {
        public MailMerge_QuyetDinhGiaiTheDonViController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaiTheDonVi>();
            QuyetDinhGiaiTheDonVi qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaiTheDonVi)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaiTheDonVi>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
