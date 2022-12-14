using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuyBoiDuongController : ViewController
    {
        public MailMerge_QuyetDinhHuyBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuyBoiDuong>();
            QuyetDinhHuyBoiDuong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuyBoiDuong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuyBoiDuong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
