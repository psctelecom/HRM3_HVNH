using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChuyenTruongDaoTaoController : ViewController
    {
        public MailMerge_QuyetDinhChuyenTruongDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenTruongDaoTao>();
            QuyetDinhChuyenTruongDaoTao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChuyenTruongDaoTao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenTruongDaoTao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
