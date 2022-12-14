using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhKyLuatController : ViewController
    {
        public MailMerge_QuyetDinhKyLuatController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhKyLuat>();
            QuyetDinhKyLuat qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhKyLuat)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhKyLuat>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
