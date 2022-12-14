using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhBoNhiemNgachController : ViewController
    {
        public MailMerge_QuyetDinhBoNhiemNgachController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhBoNhiemNgach>();
            QuyetDinhBoNhiemNgach qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhBoNhiemNgach)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhBoNhiemNgach>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
