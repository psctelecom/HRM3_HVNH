using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhBoNhiemChucVuDoanController : ViewController
    {
        public MailMerge_QuyetDinhBoNhiemChucVuDoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhBoNhiemChucVuDoan>();
            QuyetDinhBoNhiemChucVuDoan qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhBoNhiemChucVuDoan)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhBoNhiemChucVuDoan>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
