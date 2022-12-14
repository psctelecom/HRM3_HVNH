using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaHanThinhGiangController : ViewController
    {
        public MailMerge_QuyetDinhGiaHanThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaHanThinhGiang>();
            QuyetDinhGiaHanThinhGiang qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaHanThinhGiang)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaHanThinhGiang>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
