using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuongDanTapSuController : ViewController
    {
        public MailMerge_QuyetDinhHuongDanTapSuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuongDanTapSu>();
            QuyetDinhHuongDanTapSu qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuongDanTapSu)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuongDanTapSu>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
