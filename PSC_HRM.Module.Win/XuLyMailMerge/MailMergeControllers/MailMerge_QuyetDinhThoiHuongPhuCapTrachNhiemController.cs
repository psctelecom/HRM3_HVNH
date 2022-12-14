using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhThoiHuongPhuCapTrachNhiemController : ViewController
    {
        public MailMerge_QuyetDinhThoiHuongPhuCapTrachNhiemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhThoiHuongPhuCapTrachNhiem>();
            QuyetDinhThoiHuongPhuCapTrachNhiem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhThoiHuongPhuCapTrachNhiem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhThoiHuongPhuCapTrachNhiem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
