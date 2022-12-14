using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThoiChucVuDoanController : ViewController
    {
        public MailMerge_QuyetDinhThoiChucVuDoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThoiChucVuDoan>();
            QuyetDinhThoiChucVuDoan qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThoiChucVuDoan)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThoiChucVuDoan>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
