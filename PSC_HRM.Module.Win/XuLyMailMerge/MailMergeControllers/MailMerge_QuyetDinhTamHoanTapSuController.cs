using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhTamHoanTapSuController : ViewController
    {
        public MailMerge_QuyetDinhTamHoanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhTamHoanTapSu>();
            QuyetDinhTamHoanTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhTamHoanTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhTamHoanTapSu>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
