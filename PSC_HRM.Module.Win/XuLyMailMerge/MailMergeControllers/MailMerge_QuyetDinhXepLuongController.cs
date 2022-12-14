using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhXepLuongController : ViewController
    {
        public MailMerge_QuyetDinhXepLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhXepLuong>();
            QuyetDinhXepLuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhXepLuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhXepLuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
