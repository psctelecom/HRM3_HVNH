using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhBoNhiemKiemNhiemController : ViewController
    {
        public MailMerge_QuyetDinhBoNhiemKiemNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhBoNhiemKiemNhiem>();
            QuyetDinhBoNhiemKiemNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhBoNhiemKiemNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhBoNhiemKiemNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
