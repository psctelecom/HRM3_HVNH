using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhCongNhanHocHamController : ViewController
    {
        public MailMerge_QuyetDinhCongNhanHocHamController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhCongNhanHocHam>();
            QuyetDinhCongNhanHocHam qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhCongNhanHocHam)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhCongNhanHocHam>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
