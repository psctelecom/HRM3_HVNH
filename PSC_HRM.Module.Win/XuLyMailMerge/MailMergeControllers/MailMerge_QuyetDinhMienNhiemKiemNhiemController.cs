using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhMienNhiemKiemNhiemController : ViewController
    {
        public MailMerge_QuyetDinhMienNhiemKiemNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhMienNhiemKiemNhiem>();
            QuyetDinhMienNhiemKiemNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhMienNhiemKiemNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhMienNhiemKiemNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
