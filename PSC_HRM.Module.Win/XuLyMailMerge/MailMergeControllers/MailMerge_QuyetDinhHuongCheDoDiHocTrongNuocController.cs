using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHuongCheDoDiHocTrongNuocController : ViewController
    {
        public MailMerge_QuyetDinhHuongCheDoDiHocTrongNuocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHuongCheDoDiHocTrongNuoc>();
            QuyetDinhHuongCheDoDiHocTrongNuoc qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHuongCheDoDiHocTrongNuoc)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHuongCheDoDiHocTrongNuoc>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
