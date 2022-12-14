using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhDiNuocNgoaiController : ViewController
    {
        public MailMerge_QuyetDinhDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhDiNuocNgoai>();
            QuyetDinhDiNuocNgoai qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhDiNuocNgoai)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhDiNuocNgoai>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
