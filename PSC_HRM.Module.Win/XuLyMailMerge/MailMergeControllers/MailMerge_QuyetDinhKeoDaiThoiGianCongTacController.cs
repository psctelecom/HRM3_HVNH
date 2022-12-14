using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhKeoDaiThoiGianCongTacController : ViewController
    {
        public MailMerge_QuyetDinhKeoDaiThoiGianCongTacController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhKeoDaiThoiGianCongTac>();
            QuyetDinhKeoDaiThoiGianCongTac qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhKeoDaiThoiGianCongTac)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhKeoDaiThoiGianCongTac>>>("QuyetDinhKeoDaiThoiGianCongTac").Merge(Application.CreateObjectSpace(), list);
        }
    }
}
