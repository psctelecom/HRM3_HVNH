using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;
using System.Collections.Generic;
using PSC_HRM.Module;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThinhGiangController : ViewController
    {
        public MailMerge_QuyetDinhThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThinhGiang>();
            QuyetDinhThinhGiang qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThinhGiang)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThinhGiang>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
