using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuongPhuCapTrachNhiemController : ViewController
    {
        public MailMerge_QuyetDinhHuongPhuCapTrachNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuongPhuCapTrachNhiem>();
            QuyetDinhHuongPhuCapTrachNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuongPhuCapTrachNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuongPhuCapTrachNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
