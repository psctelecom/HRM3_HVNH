using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuyDiNuocNgoaiController : ViewController
    {
        public MailMerge_QuyetDinhHuyDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuyDiNuocNgoai>();
            QuyetDinhHuyDiNuocNgoai qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuyDiNuocNgoai)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuyDiNuocNgoai>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
