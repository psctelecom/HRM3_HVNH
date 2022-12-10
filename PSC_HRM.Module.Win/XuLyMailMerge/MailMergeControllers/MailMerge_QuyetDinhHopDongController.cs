using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhHopDongController : ViewController
    {
        public MailMerge_QuyetDinhHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhHopDong>();
            QuyetDinhHopDong qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhHopDong)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhHopDong>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
