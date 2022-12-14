using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhCongNhanDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhCongNhanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhCongNhanDaoTao>();
            QuyetDinhCongNhanDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhCongNhanDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhCongNhanDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
