using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.Win.XuLyMailMerge
{
    public partial class MailMerge_QuyetDinhNangPhuCapThamNienNhaGiaoController : ViewController
    {
        public MailMerge_QuyetDinhNangPhuCapThamNienNhaGiaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var list = new List<QuyetDinhNangPhuCapThamNienNhaGiao>();
            QuyetDinhNangPhuCapThamNienNhaGiao qd;
            foreach (object item in View.SelectedObjects)
            {
                qd = (QuyetDinhNangPhuCapThamNienNhaGiao)item;
                if (qd != null)
                    list.Add(qd);
            }
            SystemContainer.Resolver<IMailMerge<IList<QuyetDinhNangPhuCapThamNienNhaGiao>>>().Merge(Application.CreateObjectSpace(), list);
        }
    }
}
