using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhTiepNhanDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhTiepNhanDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhTiepNhanDaoTao>();
            QuyetDinhTiepNhanDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhTiepNhanDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhTiepNhanDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
