using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThanhLapKhacController : ViewController
    {
        public MailMerge_QuyetDinhThanhLapKhacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThanhLapKhac>();
            QuyetDinhThanhLapKhac qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThanhLapKhac)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThanhLapKhac>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
