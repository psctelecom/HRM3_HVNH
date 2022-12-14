using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNangThamNienTangThemController : ViewController
    {
        public MailMerge_QuyetDinhNangThamNienTangThemController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNangThamNienTangThem>();
            QuyetDinhNangThamNienTangThem qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNangThamNienTangThem)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNangThamNienTangThem>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
