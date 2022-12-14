using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaHanTapSuController : ViewController
    {
        public MailMerge_QuyetDinhGiaHanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaHanTapSu>();
            QuyetDinhGiaHanTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaHanTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaHanTapSu>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
