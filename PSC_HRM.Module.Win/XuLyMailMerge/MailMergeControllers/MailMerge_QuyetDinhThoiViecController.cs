using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThoiViecController : ViewController
    {
        public MailMerge_QuyetDinhThoiViecController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThoiViec>();
            QuyetDinhThoiViec qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThoiViec)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThoiViec>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
