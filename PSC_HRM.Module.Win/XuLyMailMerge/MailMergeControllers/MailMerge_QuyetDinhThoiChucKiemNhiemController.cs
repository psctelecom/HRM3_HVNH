using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThoiChucKiemNhiemController : ViewController
    {
        public MailMerge_QuyetDinhThoiChucKiemNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThoiChucKiemNhiem>();
            QuyetDinhThoiChucKiemNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThoiChucKiemNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThoiChucKiemNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
