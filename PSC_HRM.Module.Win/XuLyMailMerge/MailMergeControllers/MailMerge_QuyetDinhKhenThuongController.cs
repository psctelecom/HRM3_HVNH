using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhKhenThuongController : ViewController
    {
        public MailMerge_QuyetDinhKhenThuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhKhenThuong>();
            QuyetDinhKhenThuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhKhenThuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhKhenThuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}