using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhGiaHanDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhGiaHanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhGiaHanDaoTao>();
            QuyetDinhGiaHanDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhGiaHanDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhGiaHanDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
