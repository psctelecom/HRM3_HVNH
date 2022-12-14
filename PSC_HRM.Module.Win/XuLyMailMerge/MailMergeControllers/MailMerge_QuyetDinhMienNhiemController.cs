using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhMienNhiemController : ViewController
    {
        public MailMerge_QuyetDinhMienNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhMienNhiem>();
            QuyetDinhMienNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhMienNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhMienNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
