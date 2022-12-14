using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhBoNhiemController : ViewController
    {
        public MailMerge_QuyetDinhBoNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhBoNhiem>();
            QuyetDinhBoNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhBoNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhBoNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
