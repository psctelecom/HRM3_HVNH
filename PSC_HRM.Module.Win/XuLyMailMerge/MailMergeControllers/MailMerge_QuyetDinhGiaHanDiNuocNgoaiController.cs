using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaHanDiNuocNgoaiController : ViewController
    {
        public MailMerge_QuyetDinhGiaHanDiNuocNgoaiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaHanDiNuocNgoai>();
            QuyetDinhGiaHanDiNuocNgoai qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaHanDiNuocNgoai)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaHanDiNuocNgoai>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
