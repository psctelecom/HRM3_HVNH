using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhChuyenNgachController : ViewController
    {
        public MailMerge_QuyetDinhChuyenNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhChuyenNgach>();
            QuyetDinhChuyenNgach qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhChuyenNgach)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhChuyenNgach>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
